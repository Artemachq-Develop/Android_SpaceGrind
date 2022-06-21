using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawn : MonoBehaviour
{
    public GameObject[] spawnPoint;

    [Header("Road \n")]
    
    public GameObject[] roadObject;

    public GameObject roadPrefab;

    public Transform roadParentInstantiate;

    public float RoadfireRate = 1F;
    private float RoadnextFire = 0.0F;

    [Header("Grass \n")]

    public GameObject[] grassObject;

    public GameObject grassPrefab;

    public Transform grassParentInstantiate;

    public float grassfireRate = 1F;
    private float grassnextFire = 0.0F;

    [Header("Planets \n")]

    public GameObject[] planetsObject;

    public float planetsfireRate = 1F;
    private float planetsnextFire = 0.0F;

    [Header("Stars \n")]

    public GameObject[] starsObject;

    public float starsfireRate = 1F;
    private float starsnextFire = 0.0F;
    private bool isFirstStar = true;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject road = Instantiate(roadPrefab, spawnPoint[i].transform.position, Quaternion.identity);
            road.transform.SetParent(roadParentInstantiate);
            roadObject[i] = road;

            GameObject grass = Instantiate(grassPrefab, spawnPoint[i].transform.position, Quaternion.identity);
            grass.transform.SetParent(grassParentInstantiate);
            grassObject[i] = grass;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlay)
        {
            if (Time.time > RoadnextFire)
            {
                RoadnextFire = Time.time + RoadfireRate;

                for (int i = 0; i < roadObject.Length; i++)
                {
                    spawnPoint[i].transform.position = new Vector2(spawnPoint[i].transform.position.x, spawnPoint[i].transform.position.y + Random.Range(-0.4f, 0.4f));
                    roadObject[i].transform.position = spawnPoint[i].transform.position;
                }
            }

            if (Time.time > grassnextFire)
            {
                grassnextFire = Time.time + grassfireRate;

                for (int i = 0; i < grassObject.Length; i++)
                {
                    grassObject[i].transform.position = spawnPoint[i].transform.position;
                    grassObject[i].transform.position = new Vector2(grassObject[i].transform.position.x, grassObject[i].transform.position.y + Random.Range(-4f, 4f));
                }
            }

            if (Time.time > planetsnextFire)
            {
                planetsnextFire = Time.time + planetsfireRate;

                for (int i = 0; i < planetsObject.Length; i++)
                {
                    planetsObject[i].transform.position = spawnPoint[i].transform.position;
                    planetsObject[i].transform.position = new Vector2(planetsObject[i].transform.position.x, planetsObject[i].transform.position.y + Random.Range(-8f, 8f));
                }
            }

            if (Time.time > starsnextFire)
            {
                starsnextFire = Time.time + starsfireRate;

                isFirstStar = !isFirstStar;

                if (isFirstStar)
                {
                    starsObject[0].transform.position = spawnPoint[2].transform.position;
                }
                else if (!isFirstStar)
                {
                    starsObject[1].transform.position = spawnPoint[2].transform.position;
                }
            }
        }
    }
}