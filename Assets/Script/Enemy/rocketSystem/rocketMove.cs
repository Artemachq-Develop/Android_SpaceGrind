using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketMove : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    public bool isMove;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (isMove && gameManager.levelCount > 8)
        {
            transform.position += new Vector3(0, -4, 0) * Time.deltaTime * gameManager.speedEnemy * 1.4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMove>().Damage();
            transform.position = new Vector3(transform.position.x - 10, transform.position.y + 10, transform.position.z);
            isMove = false;
        }
    }
}
