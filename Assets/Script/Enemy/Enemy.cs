using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum type
    {
        Asteroid,
        Boss
    }

    public type typeEnemy;

    public GameObject explosionPrefab;
    public GameObject expPrefab;

    public GameObject asteroidParticle;

    public int HP_Enemy;
    private int HP_check;

    public int scoreCount;

    private GameObject player;
    private GameManager gameManager;
    public AudioClip damageSound;

    public bool isMove;

    void Start()
    {
        gameManager = GameManager.Instance;
        HP_check = HP_Enemy;
    }
    
    private void Update()
    {
        transform.position += new Vector3(0,-4, 0) * Time.deltaTime * gameManager.speedEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Bullet"))
            {
                if (typeEnemy == type.Asteroid)
                {
                    Damage(other.GetComponent<bulletDamage>().damage);

                    if (HP_Enemy <= 0)
                    {
                        int rand = Random.Range(1, 6);
                        if (rand == 1)
                        {
                            GameObject exp = LeanPool.Spawn(expPrefab, transform.position, Quaternion.identity);
                            exp.GetComponent<exp_follow>().player = gameManager.playerMove.transform;
                        }
                        
                        GameObject asteroidEffect = LeanPool.Spawn(asteroidParticle, transform.position, Quaternion.identity);
                        LeanPool.Despawn(asteroidEffect, 1f);
                        
                        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                        
                        gameManager.score_plus(scoreCount);
            
                        Destroy(explosion, 0.2f);
                        Lean.Pool.LeanPool.Despawn(this.gameObject);
                        
                        gameManager.QuestEnemyScore();
                        AchievementMeneger.achievementStatic.killCount++;

                        HP_Enemy = HP_check;
                    }
                } else if (typeEnemy == type.Boss)
                {
                    GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
                    Destroy(explosion, 0.2f);
                    Lean.Pool.LeanPool.Despawn(this.gameObject);
                }
                
                SoundManager.Instance.SoundHit(damageSound);
                
                Destroy(other.gameObject);

                //У босса патроны переделать, так как с них тоже спавнятся объекты невидимые
            }
    }

    public void Damage(int damage)
    {
        if (gameManager.isOneShootAsteroid)
        {
            HP_Enemy -= 100;
        }
        else
        {
            HP_Enemy -= damage;
        }
    }
}
