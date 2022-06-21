using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp_follow : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private float timeLeft = 0.4f;
    private GameManager gameManager;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.onUnitSphere * 8f;
    }

    private void Update()
    {
        if (timeLeft <= 0) { transform.position = Vector3.MoveTowards(transform.position, player.position, 0.08f); rb.velocity = Vector2.zero; }
        else timeLeft -= Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundManager.SoundHit(soundManager.expSound);
            gameManager.expGet(0.2f);
            Lean.Pool.LeanPool.Despawn(this.gameObject);
        }
    }
}
