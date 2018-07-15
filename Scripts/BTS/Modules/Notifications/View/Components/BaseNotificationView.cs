using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class BaseNotificationView : MonoBehaviour {
        [SerializeField]
        private Text m_date;
        [SerializeField]
        private Avatar m_image;
        [SerializeField]
        private Text m_title;
        [SerializeField]
        private Text m_notificationText;


        protected void SetDate(int date) {
            m_date.text = GetNotificationDate(date);
        }

        protected void SetTitle(string title) {
            m_title.text = title;
        }

        protected void SetLeftImage(Sprite image) {
            m_image.SetAvatar(image);
        }

        public virtual void SetMiddleText(string text) {
            m_notificationText.text = text;
        }

        internal virtual void SetViewModel(NotificationViewModel obj) {

            SetDate(obj.Date);
        }

        private string GetNotificationDate(int timestamp) {
            DateTime date = TimeDateUtils.FromUnixTime(timestamp);
            int days = TimeDateUtils.GetDaysDifference(date);
            if (days == 0) {
                return "Today";
            }
            else if (days == 1) {
                return "Yesturday";
            }
            else {
                return date.ToShortDateString();
            }
        }
    }
}