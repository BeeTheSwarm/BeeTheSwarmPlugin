using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditPostController: BasePopupController<IEditPostView>, IEditPostViewListener, IEditPostController
{
    public EditPostController()
    {
    }
    private Action<EditMenuResponce> m_callback;
    public void OnDeleteClick()
    {
        Hide();
        SendResponce(EditMenuResponce.DeletePost);
    }

    private void SendResponce(EditMenuResponce responce)
    {
        if (m_callback != null)
        {
            m_callback.Invoke(responce);
            m_callback = null;
        } else
        {
            throw new NullReferenceException("Callback cannot be null");
        }
    }

    public void OnEditClick()
    {
        Hide();
        SendResponce(EditMenuResponce.EditPost);
    }

    public void OnOutOfViewClick()
    {
        Hide();
        SendResponce(EditMenuResponce.NoAction);
    }

    public void Show(Action<object> callback, object options)
    {
        
    }

    public void Show(Action<EditMenuResponce> callback, Vector3 position) {
        m_callback = callback;
        m_view.ShowAtPosition((Vector3)position);
    }
}
