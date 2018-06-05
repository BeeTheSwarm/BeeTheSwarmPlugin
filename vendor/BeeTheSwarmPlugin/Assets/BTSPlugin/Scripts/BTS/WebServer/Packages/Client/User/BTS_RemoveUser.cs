using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS
{

    internal class BTS_RemoveUser : BTS_BasePackage<NoDataResponse>
    {

        public const string PackId = "DeleteAccount";

        public string email = String.Empty;

        public BTS_RemoveUser(string email) : base(PackId)
        {
            this.email = email;
        }

        public override Dictionary<string, object> GenerateData()
        {
            Dictionary<string, object> OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("email", email);
            return OriginalJSON;
        }

        public override bool AuthenticationRequired
        {
            get
            {
                return false;
            }
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