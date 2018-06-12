using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class PopupsModel : IPopupsModel {
        public event Action PopupAdded = delegate { };
        private List<PopupItemModel> m_queue = new List<PopupItemModel>();

        public PopupItemModel GetNextPopup() {
            if (m_queue.Count > 0) {
                var popup = m_queue[0];
                return popup;
            }
            return null;
        }

        public void PopupShown(PopupItemModel item) {
            m_queue.Remove(item);
        }

        public void AddPopup(PopupItemModel item) {
            if (m_queue.Count > 0) {
                if (m_queue[m_queue.Count-1].Equals(item)) {
                    return;
                }
            }
            m_queue.Add(item);
            PopupAdded.Invoke();
        }
    }
}