using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS
{

    internal class SetupPushId : BTS_BasePackage<NoDataResponse>
    {
        public const string PackId = "SetupPushId";
        private string m_pushId;
        public SetupPushId(string id) : base(PackId)
        {
            m_pushId = id;
        }

        public override bool AuthenticationRequired
        {
            get
            {
                return true;
            }
        }

        public override Dictionary<string, object> GenerateData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("push_id", m_pushId);
            return data;
        }
    }

}