using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInfoPopupView : MonoBehaviour {
    [SerializeField]
    private Text m_level;
    [SerializeField]
    private Text m_bees;

    public void Setup(UserInfoPopupItemModel model) {
        m_level.text = model.Level.ToString();
        m_bees.text = model.Bees.ToString();
    }
}
