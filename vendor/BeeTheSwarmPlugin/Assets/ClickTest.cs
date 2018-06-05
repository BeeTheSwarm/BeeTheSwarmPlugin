using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTest : MonoBehaviour, IPointerClickHandler {

	private float m_time = 0;
	private int m_clicks = 0;
	private const int CLICKS_COUNT = 20;
	void Update () {
		if (m_clicks > 0) {
			m_time += Time.deltaTime;
			if (m_clicks == CLICKS_COUNT) {
				Debug.Log( CLICKS_COUNT + " clicks per " + m_time + " seconds " + (m_clicks / m_time) + " click per seconds" );
				m_clicks = 0;
				m_time = 0;
			}
		}		
	}

	public void OnPointerClick(PointerEventData eventData) {
		m_clicks++;
	}
}
