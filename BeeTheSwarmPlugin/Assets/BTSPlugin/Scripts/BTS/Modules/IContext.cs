using UnityEngine;
using System.Collections;
using System;

public interface IContext
{
    event Action OnInited;
    void Show();
    void Hide();
    T GetModel<T>() where T : IModel;
    T GetService<T>() where T : IService;
    T GetController<T>() where T : IScreenController;
    void StartCoroutine(IEnumerator courutine); 
    T CreateCommand<T>() where T: ICommand, new();
}
