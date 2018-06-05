using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{

    internal class BTS_BaseServerPackage
    {

        private string m_id;
        private BTS_Error m_error = null;
        private Dictionary<string, object> m_data = null;

        private string m_rawResponce;

        //--------------------------------------
        //  Initialization
        //--------------------------------------


        public BTS_BaseServerPackage(string rawResponce)
        {

            Debug.Log(rawResponce);
            m_rawResponce = rawResponce;
            Dictionary<string, object> dict = Json.Deserialize(rawResponce) as Dictionary<string, object>;
            m_id = (string)dict["method"];
            Dictionary<string, object> errorData = (Dictionary<string, object>)dict["error"];
            m_error = new BTS_Error();
            m_error.ParseJSON(errorData);
            if (m_error.Status)
            {
                Debug.Log(NetworkService.WEBSERVER_LOG_HEADER + "Package " + m_id + " Failed: " + m_error.Description);
            }
            else
            {
                if (dict["data"] is Dictionary<string, object>)
                {
                    m_data = (Dictionary<string, object>)dict["data"];
                }
                Debug.Log(NetworkService.WEBSERVER_LOG_HEADER + "Package " + m_id + " Received ");
            }
        }

        //--------------------------------------
        //  Public Methods
        //--------------------------------------

        public T GetDataField<T>(string key)
        {

            T value = default(T);

            if (m_data != null && m_data.ContainsKey(key))
            {
                object data = m_data[key];
                try
                {
                    value = (T)System.Convert.ChangeType(data, typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.Log("[GetDataField] ChangeType Exception: " + ex.Message);
                }
            }

            return value;
        }

        //--------------------------------------
        //  Get / Set
        //--------------------------------------


        public bool IsFailed
        {
            get
            {
                return Error.Status;
            }
        }

        public string RawResponce
        {
            get
            {
                return m_rawResponce;
            }
        }

        public BTS_Error Error
        {
            get
            {
                return m_error;
            }
        }

        public string Id
        {
            get
            {
                return m_id;
            }
        }

        public Dictionary<string, object> Data
        {
            get
            {
                return m_data;
            }
        }


    }
}
