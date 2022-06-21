using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPoint;             //Точки спавна объектов (врагов, патроны, жизни и прочее)
    
    public GameObject enemyUpPrefab_1;             //Повышенные противнки
    
    public GameObject enemyUpPrefab_2;             //Повышенные противнки

    public PlayerMove playerMove;                //Скрипт обращения к скрипту игрока

    public GameObject[] objectNew;               //Сами объекты, которые будут спавняться

    public Transform enemyParentInstantiate;
    public Transform fuelParentInstantiate;

    public float fireRate = 1F;                  //Как часто появляются объекты
    private float nextFire = 0.0F;               //Обнуление таймера

    public int rand;                             //Рандомное число для шанса спавна объектов

    public BossScript bossScript;

    public GameManager gameManager;
    
    void Update()
    {
        spawnObject();
    }

    public void spawnObject()
    {
        if (Time.time > nextFire && !gameManager.isBoss && gameManager.isPlay)
        {
            gameManager.lengthRoad += 10;

            if (gameManager.lengthRoad % 1000 == 0 && gameManager.lengthRoad != 0)
            {
                StartCoroutine(gameManager.lengthRoadText_On(4f));
            }
            
            nextFire = Time.time + fireRate;
            for (int i = 0; i < enemyPoint.Length; i++)
            {
                float randomValue = Random.value;
                
                rand = Random.Range(0, objectNew.Length);
                
                if (randomValue <= 0.9f && randomValue >= 0.1f)
                {
                    if (rand >= 0 && rand <= 4)
                    {
                        int enemyRand = Random.Range(0, 2);
                        
                        //Instantiate Enemy
                        GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[enemyRand],enemyPoint[i].transform.position, Quaternion.identity);
                        /*enemy.transform.SetParent(enemyParentInstantiate);*/
                        //Instantiate Enemy
                        
                        if (enemyRand == 0)
                        {
                            enemy.transform.localRotation = Quaternion.Euler(enemy.transform.localRotation.x, enemy.transform.localRotation.y, enemy.transform.localRotation.z + Random.Range(-180, 180));
                        }
                        Lean.Pool.LeanPool.Despawn(enemy, 3f);
                    }
                }

                if (playerMove.HP <= 8)
                {
                    if (randomValue <= 0.05f && randomValue >= 0.01f)
                    {
                        if (rand >= 4 && rand <= 5)
                        {
                            
                            //Instantiate Heart
                            GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[3],enemyPoint[i].transform.position, Quaternion.identity);
                            //Instantiate Heart
                            
                            Lean.Pool.LeanPool.Despawn(enemy, 3f);
                        }
                    }
                }

                if (randomValue <= 0.01f)
                {
                    if (rand >= 5 && rand < 6)
                    {
                        int randDrop = Random.Range(0, 3);
                        int randBox = Random.Range(0, 2);
                        if (randDrop == 0 && randBox != 1)
                        {
                            
                            //Instantiate Gold
                            GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[4],enemyPoint[i].transform.position, Quaternion.identity);
                            //Instantiate Gold
                            
                            Lean.Pool.LeanPool.Despawn(enemy, 3f); 
                        }
                        else if (randDrop == 1 || randBox == 1)
                        {
                            //Instantiate BoxDrop
                            GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[7],enemyPoint[i].transform.position, Quaternion.identity);
                            //Instantiate BoxDrop
                            
                            Lean.Pool.LeanPool.Despawn(enemy, 3f); 
                        }
                        else if (randDrop == 2 && randBox != 1)
                        {
                            
                            //Instantiate Bonus
                            GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[8], enemyPoint[i].transform.position, Quaternion.identity);
                            //Instantiate Bonus
                            
                            Lean.Pool.LeanPool.Despawn(enemy, 3f);
                        }
                    }
                }

                if (gameManager.fuelCount <= 30)
                {
                    if (randomValue <= 0.2f)
                    {
                        if (rand >= 6)
                        {
                            
                            //Instantiate Fuel
                            GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[5],enemyPoint[i].transform.position, Quaternion.identity);
                            //Instantiate Fuel
                            
                            enemy.transform.SetParent(fuelParentInstantiate);
                            Lean.Pool.LeanPool.Despawn(enemy, 3f);
                        }
                    } 
                }
                else if (fuelParentInstantiate.childCount > 0)
                {
                    for (int a1 = 0; a1 < fuelParentInstantiate.childCount; a1++)
                    {
                        Lean.Pool.LeanPool.Despawn(fuelParentInstantiate.GetChild(a1).gameObject);
                    }
                }
                
                if (playerMove.allBullet <= 10)
                {
                    if (randomValue <= 0.6f && randomValue >= 0.2f)
                    {
                        if (rand >= 6 && rand < 7)
                        {
                            
                            //Instantiate BulletScore
                            GameObject enemy = Lean.Pool.LeanPool.Spawn(objectNew[6],enemyPoint[i].transform.position, Quaternion.identity);
                            //Instantiate BulletScore
                            
                            Lean.Pool.LeanPool.Despawn(enemy, 3f);
                        }
                    }
                }
            }

            if (gameManager.levelCount % 8 == 0 && gameManager.levelCount != 0)
            {
                bossScript.hpEnemy = 100;
                gameManager.isBoss = true;
                gameManager.bossActive();

                if (gameManager.levelCount == 8)
                {
                    objectNew[0] = enemyUpPrefab_1;
                } else if (gameManager.levelCount == 16)
                {
                    objectNew[0] = enemyUpPrefab_2;
                }
            }
            
            int i1 = Random.Range(0, 5);
        }
    }
}
