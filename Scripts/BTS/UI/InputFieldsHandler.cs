using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {

	
	public static class InputFieldsHandler {
		private static SlideableScreensContainer s_view;
		public static void SetView(SlideableScreensContainer slideableScreensContainer) {
			s_view = slideableScreensContainer;
		}

		public static void InputFieldStartsEdit(RectTransform inputField) {
			if (s_view != null) {
				s_view.Slide(GetScreenRect(inputField));
			}
		}
		
		public static void InputFieldEndEdit() {
			if (s_view != null) {
				s_view.Restore();
			}
		}
		
		private static Rect GetScreenRect(RectTransform transform) {
			Vector3[] rtCorners = new Vector3[4];
			transform.GetWorldCorners(rtCorners);
			Rect rtRect = new Rect(new Vector2(rtCorners[0].x, rtCorners[0].y), new Vector2(rtCorners[3].x - rtCorners[0].x, rtCorners[1].y - rtCorners[0].y));

			Canvas canvas = transform.GetComponentInParent<Canvas>();
			Vector3[] canvasCorners = new Vector3[4];
			canvas.GetComponent<RectTransform>().GetWorldCorners(canvasCorners);
			Rect cRect = new Rect(new Vector2(canvasCorners[0].x, canvasCorners[0].y), new Vector2(canvasCorners[3].x - canvasCorners[0].x, canvasCorners[1].y - canvasCorners[0].y));

			var screenWidth = Screen.width;
			var screenHeight = Screen.height;

			Vector2 size = new Vector2(1 / cRect.size.x * rtRect.size.x, 1 / cRect.size.y * rtRect.size.y);
			Rect rect = new Rect(((rtRect.x - cRect.x) / cRect.size.x), ((-cRect.y + rtRect.y) / cRect.size.y), size.x, size.y);
			return rect;
		}

		public static void Update(RectTransform getComponent) {
			if (s_view != null) {
            	s_view.UpdatePosition(getComponent, GetScreenRect(getComponent));
            }
		}
	}


}