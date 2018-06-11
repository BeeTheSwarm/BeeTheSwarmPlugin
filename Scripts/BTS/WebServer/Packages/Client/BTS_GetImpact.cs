using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_GetImpact : BTS_BasePackage<GetImpactResponce> {

        public const string PackId = "GetImpact";
        public BTS_GetImpact() : base(PackId) {

        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}