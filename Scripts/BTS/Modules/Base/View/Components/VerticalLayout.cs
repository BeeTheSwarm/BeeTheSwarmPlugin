using UnityEngine;
using System.Collections;
using System;

public class VerticalLayout : MonoBehaviour
{
    [SerializeField]
    private int fillStrategy = 0; 
    void Update()
    {
        if (fillStrategy == 0)
        {
            WrapChildren();
        } else
        {
            ArrangeChildren();
        }
    }

    private void ArrangeChildren()
    {
        float totalHeight = 0f;
        var childs = transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            var child = transform.GetChild(i);
            RectTransform rect = child.GetComponent<RectTransform>();
            if (rect!= null)
            {
                totalHeight += rect.sizeDelta.y;
            }
        }

    }

    private void WrapChildren()
    {
        float totalHeight = 0f;
        var childs = transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            var child = transform.GetChild(i);
            RectTransform rect = child.GetComponent<RectTransform>();
            if (rect == null)
            {
                rect = child.gameObject.AddComponent<RectTransform>();
            }
            rect.pivot = new Vector2(0, 1);
            rect.anchoredPosition = new Vector2(0, totalHeight);
            totalHeight += 100;
        }
    }
}
