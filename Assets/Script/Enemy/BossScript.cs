using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Lean.Pool;

public class BossScript : MonoBehaviour
{
    private SpriteRenderer mainBossRender;
    public Sprite[] bossSprite;
    
    public int hpEnemy;
    public GameObject[] enemyPoint;

    public GameObject explosionPrefab;

    public Image bossHealth;

    [Header("Points")]
    public Transform Point_1;
    public Transform Point_2;

    public PlayerMove playerMove;
    
    public float fireRate = 1F;
    private float nextFire = 0.0F;

    public AudioClip damageSound;

    private int randBoss;
    public int randBullet;

    public GameManager gameManager;
    public SoundManager soundManager;
    
    public GameObject[] bossObject;
    public GameObject[] objectBoss;
    
    public GameObject expPrefab;

    private void Start()
    {
        mainBossRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        randBoss = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isBoss && gameManager.isPlay)
        {
            bossHealth.enabled = true;
            
            if (transform.position != Point_2.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, Point_2.position, 2f * Time.deltaTime);
            }

            if (randBoss == 0)
            {
                mainBossRender.sprite = bossSprite[0];
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    byte randSound = (byte) Random.Range(0, 3);
                    if (randSound == 1)
                    {
                        soundManager.SoundHit(soundManager.bossShoot);
                    }
                    for (int i = 0; i < enemyPoint.Length; i++)
                    {
                        float randomValue = Random.value;
                
                        if (playerMove.allBullet <= 10)
                        {
                            randBullet = Random.Range(0, objectBoss.Length);
                        }
                        else
                        {
                            randBullet = 0;
                        }
                
                        if (randomValue <= 0.8f && randomValue >= 0.1f)
                        {
                            if (randBullet == 0)
                            {
                                GameObject enemy = Lean.Pool.LeanPool.Spawn(objectBoss[0], enemyPoint[i].transform.position,
                                    Quaternion.identity);
                                Lean.Pool.LeanPool.Despawn(enemy, 5f);
                            }
                        }
                
                        if (playerMove.allBullet <= 10)
                        {
                            if (randomValue <= 0.2f && randomValue >= 0.1f)
                            {
                                if (randBullet == 1)
                                {
                                    GameObject enemy = Lean.Pool.LeanPool.Spawn(objectBoss[1], enemyPoint[i].transform.position,
                                        Quaternion.identity);
                                    Lean.Pool.LeanPool.Despawn(enemy, 5f);
                                }
                            }
                        }
                    }
                }
            }
            else if (randBoss == 1)
            {
                mainBossRender.sprite = bossSprite[1];
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    byte randSound = (byte) Random.Range(0, 3);
                    if (randSound == 1)
                    {
                        soundManager.SoundHit(soundManager.bossShoot);
                    }
                    for (int i = 0; i < enemyPoint.Length; i++)
                    {
                        float randomValue = Random.value;
                
                        if (playerMove.allBullet <= 10)
                        {
                            randBullet = Random.Range(0, objectBoss.Length);
                        }
                        else
                        {
                            randBullet = 0;
                        }
                
                        if (randomValue <= 0.8f && randomValue >= 0.1f)
                        {
                            if (randBullet == 0)
                            {
                                Damage(1);
                                GameObject enemy = Lean.Pool.LeanPool.Spawn(objectBoss[2], enemyPoint[i].transform.position,
                                    Quaternion.identity);
                                Lean.Pool.LeanPool.Despawn(enemy, 5f);
                            }
                        }
                
                        if (playerMove.allBullet <= 10)
                        {
                            if (randomValue <= 0.2f && randomValue >= 0.1f)
                            {
                                if (randBullet == 1)
                                {
                                    GameObject enemy = Lean.Pool.LeanPool.Spawn(objectBoss[1], enemyPoint[i].transform.position,
                                        Quaternion.identity);
                                    Lean.Pool.LeanPool.Despawn(enemy, 5f);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Damage(10);
            SoundManager.Instance.SoundHit(damageSound);
            transform.position += new Vector3(Random.Range(-0.2f, 0.2f),Random.Range(-0.2f, 0.2f), 0f);
            GameObject firePrefab = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            firePrefab.transform.localScale = new Vector3(3f, 3f, 3f);
            Destroy(other.gameObject);
            Destroy(firePrefab, 0.2f);
        }
    }

    public void Damage(int damage)
    {
        hpEnemy -= damage;
        bossHealth.fillAmount = hpEnemy / 100f;
        if (hpEnemy <= 0)
        {
            bossHealth.enabled = false;
            gameManager.levelCount++;
            gameManager.QuestBossKill();
            AchievementMeneger.achievementStatic.bossKillCount++;
            for (int i = 0; i < 5; i++)
            {
                GameObject exp = LeanPool.Spawn(expPrefab, transform.position, Quaternion.identity);
                exp.GetComponent<exp_follow>().player = gameManager.playerMove.transform;
            }
            transform.position = Point_1.position;
            gameManager.isBoss = false;
            gameManager.bossActive();
            gameObject.SetActive(false);
        }
    }
}
