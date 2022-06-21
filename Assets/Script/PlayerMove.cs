using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject bulletPrefab;

    public Transform[] teleportPoint;
    
    public int posInt;
    
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    public float dashInt;
    
    public float HP = 10;
    
    
    public int allBullet = 5;
    public int maxBullet = 10;

    public int playerDamage = 10;

    public bool isDead;
    public bool isSwap;
    
    public GameSystem_HUD gameSystem;
    
    public ScreenShake screenShake;

    public Animator animator;

    private Transform playerPos;
    private float now;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    public GameManager gameManager;
    public SoundManager soundManager;

    private void Start()
    {
        playerPos = GetComponent<Transform>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager.isPlay)
        {
            Swipe();

            if (posInt >= 0 || posInt <= 4)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(teleportPoint[posInt].position.x, transform.position.y), 0.4f);
            }

                if (HP <= 1 || gameManager.fuelCount <= 0)
                {
                    gameManager.isDiedPlayer();

                    isDead = true;
                }

            gameManager.fuelCount -= Time.deltaTime * gameManager.fuelSpeed;

            gameSystem.hpImage.fillAmount = HP / 10;

            gameSystem.scoreText.text = gameManager.score.ToString();
        }

        gameSystem.fuelImage.fillAmount = gameManager.fuelCount / 100;
    }

    public void Swipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
       
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            now = Vector3.Distance(firstPressPos, secondPressPos);
            
            currentSwipe.Normalize();
            
                //swipe upwards
                    if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && gameManager.isControlSwipe && now >= 30)
                    {
                        Shoot();
                    }
                    //swipe down
                    if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && now >= 30)
                    {
                        /*if (transform.position.y >= -4f)
                        {
                            transform.position = new Vector3(transform.position.x,transform.position.y - dashInt,transform.position.z);
                        }*/

                        if(dashInt == 2)
                        {
                            StartCoroutine(slowPlayer());
                        }
                    }
                    //swipe left
                    if(currentSwipe.x < 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && now >= 20 && gameManager.isControlSwipe && posInt >= 1 && !isSwap)
                    {
                        isSwap = true;
                        
                        posInt--;

                        animator.SetBool("isSwipe", true);

                        gameManager.fuelCount -= gameManager.fuelSwipeCount;

                        soundManager.SoundHit(soundManager.swipeSound);
                    } 
                
                    //swipe right
                
                    if(currentSwipe.x > 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && now >= 20 && gameManager.isControlSwipe && posInt <= 3 && !isSwap)
                    {
                        isSwap = true;
                        
                        posInt++;

                        animator.SetBool("isSwipe", true);
                        
                        gameManager.fuelCount -= gameManager.fuelSwipeCount;

                        soundManager.SoundHit(soundManager.swipeSound);
                    }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwap = false;
        }
    }

    public void leftSwipe()
    {
        if (posInt >= 1 && !gameManager.isControlSwipe)
        {
            posInt--;

            animator.SetBool("isSwipe", true);

            gameManager.fuelCount -= gameManager.fuelSwipeCount;

            soundManager.SoundHit(soundManager.swipeSound);
        }
    }

    public void rightSwipe()
    {
        if (posInt <= 3 && !gameManager.isControlSwipe)
        {
            posInt++;

            animator.SetBool("isSwipe", true);
                        
            gameManager.fuelCount -= gameManager.fuelSwipeCount;

            soundManager.SoundHit(soundManager.swipeSound);
        }
    }

    public void shootButton()
    {
        if (allBullet > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (allBullet > 0)
            {
                allBullet--;
                    
                GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
                bullet.GetComponent<bulletDamage>().damage = playerDamage;
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * 20;

                animator.SetBool("isShoot", true);
                                
                Destroy(bullet, 1f);

                soundManager.SoundHit(soundManager.shootSound);
            }else if (allBullet <= 0)
            {
                soundManager.SoundHit(soundManager.emptyBullet);
                gameSystem.bulletText.color = Color.red;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage();
            Lean.Pool.LeanPool.Despawn(other.gameObject);
            screenShake.StartShake(0.15f,0.15f);
        }
    }

    public void Damage()
    {
        if (!gameManager.isInfinityHealth)
        {
            if (!gameSystem.isAddLife)
            {
                HP -= 1.66f;
                AchievementMeneger.achievementStatic.touchDamageCount++;
                soundManager.SoundHit(soundManager.playerDamageSound);
            }
            else if (gameSystem.isAddLife)
            {
                gameSystem.playerAddLifeObject.SetActive(false);
                gameSystem.isAddLife = false;
            }
        }
    }
    
    private IEnumerator slowPlayer()
    {
            Time.timeScale = 0.5f;
            soundManager.SoundHit(soundManager.slowMotionSound);
            yield return new WaitForSeconds(1f);
            Time.timeScale = 1f;
            StopCoroutine(slowPlayer());
    }
}
