using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BTS
{
    public abstract class BTS_BasePackage<ResponceModel>:IBasePackage where ResponceModel: PackageResponse
    {
        private string m_id;
        private Int32 m_timeStamp;
        private BTS_Error m_error = null;
        private Dictionary<string, object> m_data = null;
        private ResponceModel m_responce = null;

        //--------------------------------------
        //  Initialization
        //--------------------------------------
        
        public BTS_BasePackage(string id)
        {
            m_id = id;
            m_timeStamp = TimeDateUtils.CurrentTimeStamp;
        }
        
        //--------------------------------------
        // Get / SET
        //--------------------------------------
        
        public string Id
        {
            get
            {
                return m_id;
            }
        }

        public Int32 TimeStamp
        {
            get
            {
                return m_timeStamp;
            }
        }

        public virtual Int32 Timeout
        {
            get
            {
                return 10;
            }
        }

        public virtual bool AuthenticationRequired
        {
            get
            {
                return true;
            }
        }
         
        public virtual Dictionary<string, object> GenerateData()
        {
            return new Dictionary<string, object>();
        }

        public IServerResponseHandler<ResponceModel> Handler { get; set; }
        public bool HasHandler { get { return Handler != null; } }

        public BTS_Error Error {
            get {
                return m_error;
            }
        }

        public void ParseResponse(string text) { 
            Dictionary<string, object> dict = Json.Deserialize(text) as Dictionary<string, object>;
            m_id = (string)dict["method"];
            Dictionary<string, object> errorData = (Dictionary<string, object>)dict["error"];
            m_error = new BTS_Error();
            m_error.ParseJSON(errorData);
            if (!m_error.Status)
            {
                if (dict["data"] is Dictionary<string, object>)
                {
                    m_data = (Dictionary<string, object>)dict["data"];
                    m_responce = Activator.CreateInstance<ResponceModel>();
                    m_responce.Parse(m_data);
                }
            }
        }
        
        public void HandleResponce() {
            if (m_error.Status) {
                Handler.OnError(m_error);
            } else {
                Handler.OnSuccess(m_responce);
            }
        }

    }
}