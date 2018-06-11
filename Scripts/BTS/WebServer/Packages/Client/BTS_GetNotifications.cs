using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS
{

    internal class BTS_GetNotifications : BTS_BasePackage<GetNotificationsResponse>
    {
        private const string PACK_ID = "GetNotifications";
         
        private int m_limit;
        private int m_offset;
        public BTS_GetNotifications(int offset, int limit) : base(PACK_ID)
        {
            m_offset = offset;
            m_limit = limit;
        }

        public override Dictionary<string, object> GenerateData()
        {
            Dictionary<string, object> requestFields = new Dictionary<string, object>();
            requestFields.Add("offset", m_offset);
            requestFields.Add("limit", m_limit); 
            return requestFields;
        }
        
        public override Int32 Timeout
        {
            get
            {
                return 5;
            }
        }
    }
}