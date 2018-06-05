using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    public class NotificationCampaignFundedView : BaseNotificationView {
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            SetMiddleText("Your campaign reached Level "+ obj.Value+"!");
        }
    }
}