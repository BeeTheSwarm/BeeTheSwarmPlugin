using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragEventsProxy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    public void OnDrag(PointerEventData eventData) {
        var handlers = GetComponentsInParent<IDragHandler>();
        foreach (var handler in handlers) {
            if (handler == this) {
                continue;
            }

            handler.OnDrag(eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        var handlers = GetComponentsInParent<IBeginDragHandler>();
        foreach (var handler in handlers) {
            if (handler == this) {
                continue;
            }

            handler.OnBeginDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        var handlers = GetComponentsInParent<IEndDragHandler>();
        foreach (var handler in handlers) {
            if (handler == this) {
                continue;
            }

            handler.OnEndDrag(eventData);
        }
    }
}