using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BTS.Base.DependencyInjection {
    public class DependencyContainer {
        private Dictionary<Type, IScreenController> m_controllersMap = new Dictionary<Type, IScreenController>();
        private Dictionary<Type, IPopupController> m_popupControllersMap = new Dictionary<Type, IPopupController>();
        private Dictionary<Type, IService> m_servicesMap = new Dictionary<Type, IService>();
        private Dictionary<Type, IModel> m_modelsMap = new Dictionary<Type, IModel>();
        private Dictionary<Type, Func<IViewController>> m_controllersDelegatesMap = new Dictionary<Type, Func<IViewController>>();
        
        

        private void CheckInjects(IInjectTarget target) {
            GetInjectableFields(target).ForEach(fieldInfo => {
                Type fieldType = fieldInfo.FieldType;
                if (fieldInfo.GetValue(target)==null) {
                    Debug.Log("No injectable candidate for " + fieldType.Name + " in " + target.GetType().Name);
                }
            });
        }


        public void AddService<T>(T service) where T : IService {
            m_servicesMap.Add(typeof(T), service);
        }

        public void AddModel<T>(T model) where T : IModel {
            m_modelsMap.Add(typeof(T), model);
        }

        public void AddControllerDelegate<T>(Func<IViewController> fabric) where T : IViewController {
            m_controllersDelegatesMap.Add(typeof(T), fabric);
        }

        public void AddController<T>(T controller, IView view) where T : IScreenController {
            controller.SetView(view);
            m_controllersMap.Add(typeof(T), controller);
        }

        public void AddPopupController<T>(T controller, IView view) where T : IPopupController {
            controller.SetView(view);
            m_popupControllersMap.Add(typeof(T), controller);
        }
        

        private void InjectFromMap<T>(IInjectTarget target, IDictionary<Type, T> sourceMap) {
            GetInjectableFields(target).ForEach(fieldInfo => {
                var fieldType = fieldInfo.FieldType;
                if (sourceMap.ContainsKey(fieldType)) {
                    fieldInfo.SetValue(target, sourceMap[fieldType]);
                }
            });
        }
        
        private void InjectControllerDelegates(IInjectTarget target) {
            GetInjectableFields(target).ForEach(fieldInfo => {
                Type fieldType = fieldInfo.FieldType;
                if (m_controllersDelegatesMap.ContainsKey(fieldType)) {
                    fieldInfo.SetValue(target, GetControllerDelegate(m_controllersDelegatesMap[fieldType]));
                }
            });
            
        }
        

        private IViewController GetControllerDelegate(Func<IViewController> factory) {
            var controllerDelegate = factory();

            InjectFromMap(controllerDelegate, m_modelsMap);
            InjectFromMap(controllerDelegate, m_servicesMap);
            InjectFromMap(controllerDelegate, m_controllersMap);
            InjectFromMap(controllerDelegate, m_popupControllersMap);
            CheckInjects(controllerDelegate);
            controllerDelegate.PostInject();
            return controllerDelegate;
        }
        
        private List<FieldInfo> GetInjectableFields(object target) {
            var result = new List<FieldInfo>();
            Type type = target.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (var i = 0; i < fields.Length; i++) {
                var taggedFields = fields[i].GetCustomAttributes(typeof(Inject), false);
                if (taggedFields.Length > 0) {
                    result.Add(fields[i]);
                }
            }
            return result;
        }
                
        public T GetModel<T>() where T : IModel {
            Type type = typeof(T);
            if (m_modelsMap.ContainsKey(type)) {
                return (T)m_modelsMap[type];
            }
            throw new ArgumentException("unknown model");
        }

        
        
        public void InjectToCommand(ICommand target) {
            InjectFromMap(target, m_modelsMap);
            InjectFromMap(target, m_servicesMap);
            target.PostInject();
        }
        
        public T GetService<T>() where T : IService {
            Type type = typeof(T);
            if (m_servicesMap.ContainsKey(type)) {
                return (T) m_servicesMap[type];
            }
            throw new ArgumentException("unknown service");
        }


        public T GetController<T>() where T : IScreenController {
            Type type = typeof(T);
            if (m_controllersMap.ContainsKey(type)) {
                return (T)m_controllersMap[type];
            }
            throw new ArgumentException("unknown controller");
        }

        public T CreateCommand<T>() where T : ICommand, new() {
            var command = Activator.CreateInstance<T>();
            InjectToCommand(command);
            return command;
        }
        
        public void InjectDependencies() {
            foreach (var controller in m_controllersMap.Values) {
                InjectFromMap(controller, m_modelsMap);
                InjectFromMap(controller, m_servicesMap);
                InjectFromMap(controller, m_controllersMap);
                InjectFromMap(controller, m_popupControllersMap);
                InjectControllerDelegates(controller);
                CheckInjects(controller);
                controller.PostInject();
            }
            foreach (var controller in m_popupControllersMap.Values) {
                InjectFromMap(controller, m_modelsMap);
                InjectFromMap(controller, m_servicesMap);
                InjectFromMap(controller, m_controllersMap);
                InjectFromMap(controller, m_popupControllersMap);
                CheckInjects(controller);
                controller.PostInject();
            }
            foreach (var service in m_servicesMap.Values) {
                InjectFromMap(service, m_modelsMap);
                InjectFromMap(service, m_servicesMap);
                CheckInjects(service);
                service.PostInject();
            }
        }
    }
}