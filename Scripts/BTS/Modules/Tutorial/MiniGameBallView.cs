using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiniGameBallView : MonoBehaviour {

	public event Action OnGameStarted = delegate {};
	public event Action OnScoreAdded = delegate {};
	public event Action OnGameOver = delegate {};

	private Rigidbody2D m_rigidbody;
	private EventTrigger m_eventTrigger;
	private Button m_button;

	public int GameTime { get; private set; }
	[SerializeField]
	private float m_gravityScale = 50f;
	[SerializeField]
	private float m_ballForce = 280f;

	private Vector3 m_ballStartPosition;
	
	public int Score { get; private set; }
	
	private Vector2 Force {
		get {
			return new Vector2 (0, m_ballForce);
		}
	}

	private void Awake() {
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_ballStartPosition = m_rigidbody.transform.position;
		m_eventTrigger = gameObject.AddComponent<EventTrigger>();
		EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
		pointerDownEntry.eventID = EventTriggerType.PointerDown;
		pointerDownEntry.callback.AddListener((data) => PointerDownHandler());
		m_eventTrigger.triggers.Add(pointerDownEntry);
	}

	private bool m_gameActive;
	private float m_ballTimeOut;
	private float m_gameTime = 0;
	private int m_clicks = 0;
	private void Update() {
		if (m_gameActive) {
			m_rigidbody.mass += 2f*Time.deltaTime;
			m_gameTime += Time.unscaledDeltaTime;
		}
	}

	private void SetDefaults() {
		m_clicks = 0;
		m_rigidbody.transform.position = m_ballStartPosition;
		m_gameActive = false;
		m_rigidbody.mass = 1;
		m_rigidbody.gravityScale = 0;
	}

	public void PointerDownHandler() {
		if (!m_gameActive) {
			StartGame();
		}
		else {
			PushBall();
		}
	}

	private void PushBall() {
		m_clicks++;
		AddPoints();
		m_rigidbody.AddForce(Force, ForceMode2D.Impulse);
	}

	private void StartGame() {
		m_gameTime = 0;
		m_gameActive = true;
		m_rigidbody.gravityScale = m_gravityScale;
		OnGameStarted();
	}

	private void AddPoints() {
		Score++;
		OnScoreAdded.Invoke();
	}

	private void OnCollisionEnter2D(Collision2D other) {

		if (other.collider.tag == "Platform") {

			m_eventTrigger.triggers.Clear();
			GameTime = Mathf.CeilToInt(m_gameTime);
			OnGameOver();
			SetDefaults();
		}
	}
}
