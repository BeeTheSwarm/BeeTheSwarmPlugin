using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestCollided : MonoBehaviour {

    public float raqem;
    
    SpriteRenderer m_thisSpriteRenderer;
    [SerializeField] Text m_countChestText;
    [SerializeField] ParticleSystem m_particleSystem;

    private bool canDestroy;

     void Start()
    {
        canDestroy = false;

        m_countChestText.text = "" + raqem;

        m_thisSpriteRenderer = this.GetComponent<SpriteRenderer>();

    }

     void Update()
    {

        if (canDestroy) {
            if (m_particleSystem.isStopped)
                Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        raqem = raqem - 1;
        m_countChestText.text = "" + raqem;

        if (raqem == 0)
        {
            m_countChestText.enabled = false;
            m_thisSpriteRenderer.enabled = false;
            m_particleSystem.Play();
            this.GetComponent<Collider2D>().enabled = false;

            canDestroy = true;
        }
    }

    public void CheckTouchChestBottom() {
        if (transform.position.y < -3.45f) {
            GameObject.Find("GameManager").GetComponent<GameManager>().SetGameOver();
            Destroy(this.gameObject);
        }

    }


}
