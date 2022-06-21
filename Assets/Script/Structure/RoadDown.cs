using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDown : MonoBehaviour
{
    public float speed;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (gameManager.isPlay)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
}
