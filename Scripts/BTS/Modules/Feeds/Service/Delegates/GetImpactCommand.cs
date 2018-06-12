using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetImpactCommand : BaseNetworkService<GetImpactResponce>, IGetImpactService {
        [Inject] private IFeedsModel m_model;


        public GetImpactCommand() { }

        public void Execute() {
            SendPackage(new BTS_GetImpact());
        }

        protected override void HandleSuccessResponse(GetImpactResponce data) {
            m_model.Impact = data.Impact;
            FireSuccessFinishEvent();
        }
    }
}