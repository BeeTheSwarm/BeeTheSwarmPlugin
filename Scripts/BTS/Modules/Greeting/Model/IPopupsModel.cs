using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{
    public interface IPopupsModel : IModel {
        event Action PopupAdded;
        PopupItemModel GetNextPopup();
        void AddPopup(PopupItemModel item);
        void PopupShown(PopupItemModel m_currentItem);
    }
}