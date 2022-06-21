using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemySpawn : MonoBehaviour
{
    public GameObject[] spawnPoint;

    public GameObject rocketObject;

    public float rocketfireRate = 1F;
    private float rocketnextFire = 0.0F;

    public GameManager gameManager;

    private rocketMove rocketMove;
    private GameObject rocket;

    private void Start()
    {
        rocketMove = rocketObject.GetComponent<rocketMove>();
    }

    private void Update()
    {
        if (gameManager.isPlay && gameManager.levelCount > 8)
        {
            int randomPoint;

            randomPoint = Random.Range(0, 4);

            if (Time.time > rocketnextFire)
            {
                rocketnextFire = Time.time + rocketfireRate;

                rocketObject.transform.position = spawnPoint[randomPoint].transform.position;

                rocketMove.isMove = true;
            }
        }
    }
}
