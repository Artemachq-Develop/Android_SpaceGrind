using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public string gameId = "4043903", type = "video";
    public bool testMode = true;
    
    public int score;
    public int allScore;
    
    public int levelCount;

    public int lengthRoad;
    
    public int record;
    public int musicCheck = 1;
    public int goldCount;
    public float fuelCount = 100;
    public float fuelTimer = 1f;
    public float fuelSpeed = 2.6f;
    public float bulletSpeedReload;
    public int boxCount;
    public int hpBox = 5;
    public float fuelSwipeCount;
    public int woodCount = 0;
    public int rockCount = 0;

    public int bonusCount = 1;

    public int playerAddLife;

    public float expBonus = 0f;

    public int boxBonus = 0;

    public int bulletBonus;

    public GameObject bossObject;
    public GameObject enemyObject;

    [Range(1f,2f)]
    public float speedEnemy = 1f;

    public GameSystem_HUD gameSystem;
    public Menu_HUD hud;
    public PlayerMove playerMove;
    public AudioSource musicSource;
    public EnemySpawn enemySpawn;
    
    public bool isBoss;
    public bool isPlay;
    
    //oneGameAbility
    public bool isOneShootAsteroid;
    public bool isMoreMoney;

    public bool isInfinityHealth;

    public bool isControlSwipe;
    public byte controlCount;

    public SoundManager soundManager;
    private float timerSpeedEnemy = 20f;

    [Header("EXP")]
    public float expCount;
    public int lvlCount;

    [Space]

    [Header("Player Skin")]
    public Animator playerAnimator;
    public RuntimeAnimatorController[] playerAnimations;
    public byte skinNumber;
    public SkinChange skinChange;
    
    public LanguageMar languageMar;
    public short languageInt;
    
    public ScreenShake screenShake;
    
    private Save save = new Save();
    public DailyQuestSystem dailyQuestSystem;
    public GuildSystem_Save guildSystemSave;
    public AchievementMeneger achievementMeneger;
    
    public static GameManager Instance;
    
    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        
        /*Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;*/
        
        LoadFile();
        achievementMeneger.CheckAchievement();
        guildSystemSave.LoadGuild();
        

        score = 0;
        
        if (skinNumber == 0)
        {
            playerAnimator.runtimeAnimatorController = playerAnimations[0];
            skinChange.shopShipImage.sprite = skinChange.shipSprite[0];
        } else if (skinNumber == 1)
        {
            playerAnimator.runtimeAnimatorController = playerAnimations[1];
            skinChange.shopShipImage.sprite = skinChange.shipSprite[1];
        }
        else if (skinNumber == 2)
        {
            playerAnimator.runtimeAnimatorController = playerAnimations[2];
            skinChange.shopShipImage.sprite = skinChange.shipSprite[2];
        }

        if (controlCount == 0)
        {
            isControlSwipe = true;
            gameSystem.touchButtonsPanel.SetActive(false);
        } else if (controlCount == 1)
        {
            isControlSwipe = false;
            gameSystem.touchButtonsPanel.SetActive(true);
        }

        if (musicCheck == 0)
        {
            gameSystem.musicImage.sprite = gameSystem.musicSprite[1];
            musicSource.volume = 0f;
        }
        else if (musicCheck == 1)
        {
            gameSystem.musicImage.sprite = gameSystem.musicSprite[0];
            musicSource.volume = 0.008f;
        }
        
        if (PlayerPrefs.HasKey("Save"))
        {
            if (languageInt == 0)
            {
                languageMar.LoadLanguage(SystemLanguage.English);
            }
            else if (languageInt == 1)
            {
                languageMar.LoadLanguage(SystemLanguage.Russian);
            }
        }
        else if(!PlayerPrefs.HasKey("Save"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                languageMar.LoadLanguage(SystemLanguage.Russian);
                hud.rusLang_button();
            } else
            {
                languageMar.LoadLanguage(SystemLanguage.English);
                hud.enLang_button();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlay)
        {
                timerSpeedEnemy -= Time.deltaTime * bonusCount;
                if (timerSpeedEnemy <= 0)
                {
                    timerSpeedEnemy = 20f;
                    levelCount++;
                    if (speedEnemy <= 2)
                    {
                        speedEnemy += 0.1f;
                    }
                    if (enemySpawn.fireRate > 0.5f)
                    {
                        enemySpawn.fireRate -= 0.1f;
                    }
                    if (fuelSwipeCount < 0.5f)
                    {
                        fuelSwipeCount += 0.05f;
                    }
                }
            if (fuelCount <= 20)
            {
                screenShake.StartShake(0.1f, 0.1f);
            }
        }
    }
    
    public void SaveFile()
    {
            save.allScore_Save = allScore;
            save.fireRate_Save = playerMove.fireRate;
            save.dashInt_Save = playerMove.dashInt;
            save.fuelSpeed_Save = fuelSpeed;
            save.record_Save = record;
            save.allBullet_Save = playerMove.allBullet;
            save.maxBullet_Save = playerMove.maxBullet;
            save.bulletSpeedReload_Save = bulletSpeedReload;
            save.musicCheck_Save = musicCheck;
            save.goldCount_Save = goldCount;
            save.boxCount_Save = boxCount;
            save.woodCount_Save = woodCount;
            save.rockCount_Save = rockCount;
            save.expCount_Save = expCount;
            save.lvlCount_Save = lvlCount;
            save.language_Save = languageInt;
            save.skinNumber_Save = skinNumber;
            save.controlCount_Save = controlCount;

            save.playerDamage_Save = playerMove.playerDamage;
            save.playerAddLife_Save = playerAddLife;
            save.expBonus_Save = expBonus;
            save.boxBonus_Save = boxBonus;
            save.bulletBonus_Save = bulletBonus;

            //Сохранение
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
    }
    
    public void LoadFile()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            Debug.Log("Dont Exist Save File (Save)");
            playerMove.allBullet = 5;
        }
        else
        {
            save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));

            allScore = save.allScore_Save;
            playerMove.fireRate = save.fireRate_Save;
            playerMove.dashInt = save.dashInt_Save;
            fuelSpeed = save.fuelSpeed_Save;
            record = save.record_Save;
            playerMove.allBullet = save.allBullet_Save;
            playerMove.maxBullet = save.maxBullet_Save;
            bulletSpeedReload = save.bulletSpeedReload_Save;
            musicCheck = save.musicCheck_Save;
            goldCount = save.goldCount_Save;
            boxCount = save.boxCount_Save;
            woodCount = save.woodCount_Save;
            rockCount = save.rockCount_Save;
            expCount = save.expCount_Save;
            lvlCount = save.lvlCount_Save;
            languageInt = save.language_Save;
            skinNumber = save.skinNumber_Save;
            controlCount = save.controlCount_Save;
            
            playerMove.playerDamage = save.playerDamage_Save;
            playerAddLife = save.playerAddLife_Save;
            expBonus = save.expBonus_Save;
            boxBonus = save.boxBonus_Save;
            bulletBonus = save.bulletBonus_Save;
        }
        dailyQuestSystem.LoadQuest();
    }

    public void saveBuyButton()
    {
        
    }

    public void isDiedPlayer()
    {
        allScore += score;
                    
        if (score > record)
        {
            record = score;
        }
                    
        QuestPlayerDied();

        SaveFile();

        Time.timeScale = 0;
        
        isPlay = false;

        gameSystem.gameHUD.SetActive(false);

        gameSystem.endGameHud.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        SaveFile();
    }
    
    public void score_plus(int scoreCount)
    {
        if (isMoreMoney)
        {
            score += scoreCount + 1;
        }
        else
        {
            score += scoreCount;
        }
        
        soundManager.SoundHit(soundManager.scoreDamageSound[Random.Range(0, soundManager.scoreDamageSound.Length)]);
    }

    public void expGet(float exp)
    {
        expCount += (exp + expBonus);

        if (expCount >= 10)
        {
            expCount = 0;
            lvlCount++;
        }
    }

    public void goldGet()
    {
        goldCount++;
        soundManager.SoundHit(soundManager.goldSound);
    }

    public void bulletGet()
    {
        playerMove.allBullet += (5 + bulletBonus);
        gameSystem.bulletText.color = Color.white;
        soundManager.SoundHit(soundManager.slimeBulletSound[Random.Range(0, soundManager.slimeBulletSound.Length)]);
    }

    public void fuelGet()
    {
        gameSystem.fuelHUD.SetActive(true);
        gameSystem.isFuel = true;
    }

    public void brickGet()
    {
        playerMove.Damage();
        screenShake.StartShake(0.15f,0.15f);
    }

    public void heartGet()
    {
        playerMove.HP += 1.66f;
        soundManager.SoundHit(soundManager.healSound);
    }

    public void boxGet()
    {
        boxCount += (1 + boxBonus);
        soundManager.SoundHit(soundManager.woodSound);
    }

    public void bonusGet()
    {
        StartCoroutine(bonusTimer(4f));
    }

    public void bossActive()
    {
        if (isBoss)
        {
            bossObject.SetActive(true);
            enemyObject.SetActive(false);
        }
        else if (!isBoss)
        {
            bossObject.SetActive(false);
            enemyObject.SetActive(true);
        }
    }

    private IEnumerator bonusTimer(float waitTime)
    {
        bonusCount = 4;
        yield return new WaitForSeconds(waitTime);
        bonusCount = 1;
        StopCoroutine(bonusTimer(waitTime));
    }

    public IEnumerator infinityHealth(float waitTime)
    {
        isInfinityHealth = true;
        gameSystem.infinityHealthText.enabled = true;
        if (languageInt == 1)
        {
            gameSystem.infinityHealthText.text = "Бессмертие";
        }
        else if (languageInt == 0)
        {
            gameSystem.infinityHealthText.text = "Immortality";
        }
        playerMove.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(waitTime);
        isInfinityHealth = false;
        gameSystem.infinityHealthText.enabled = false;
        playerMove.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public int randomGiftCount()
    {
        int a;

        if (lvlCount >= 5)
        {
            float div = ((float)lvlCount / 10) * 2;
            Debug.Log(div);
            a =  (int)div * Random.Range(100, 200);
        }
        else
        {
            a = Random.Range(100, 400);
        }
        
        return a;
    }

    public void QuestEnemyScore()
    {
        if (dailyQuestSystem.requestCount1 < dailyQuestSystem.completeCount1)
        {
            dailyQuestSystem.requestCount1++;
            dailyQuestSystem.SaveQuest();
        }
    }
    
    public void QuestBossKill()
    {
        if (dailyQuestSystem.requestCount2 < dailyQuestSystem.completeCount2)
        {
            dailyQuestSystem.requestCount2++;
            dailyQuestSystem.SaveQuest();
        }
    }

    public void QuestPlayerDied()
    {
        if (dailyQuestSystem.requestCount3 < dailyQuestSystem.completeCount3)
        {
            dailyQuestSystem.requestCount3++;
            dailyQuestSystem.SaveQuest();
        }
    }
    
    public void ShowAd(byte adCount)
    {
        if (Advertisement.IsReady() && adCount == 0)
        {
            var showOptions = new ShowOptions();
            showOptions.resultCallback += ContinueGameCallBack;
            Advertisement.Show(type, showOptions);
        }
        else if (Advertisement.IsReady() && adCount == 1)
        {
            var showOptions = new ShowOptions();
            showOptions.resultCallback += BoxCountAddCallBack;
            Advertisement.Show(type, showOptions);
        }
        else if (Advertisement.IsReady() && adCount == 2)
        {
            var showOptions = new ShowOptions();
            showOptions.resultCallback += FreeMoneyCallBack;
            Advertisement.Show(type, showOptions);
        }
        else if (Advertisement.IsReady() && adCount == 3)
        {
            var showOptions = new ShowOptions();
            Advertisement.Show(type, showOptions);
        }
    }
    
    private void ContinueGameCallBack(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            gameSystem.ResetContinueGame();
        }else Debug.Log("No award given. Result was :: " + result);
    }
    
    private void BoxCountAddCallBack(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            boxCount++;
            if (boxCount > 0)
            {
                hud.boxDropImage.SetActive(true);
                hud.boxCountText.text = languageMar.GetTest(hud.boxCountText.gameObject.name) + '\n' + boxCount;
            }
        }else Debug.Log("No award given. Result was :: " + result);
    }
    
    private void FreeMoneyCallBack(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            allScore += Random.Range(100, 400);
            hud.freeAdMoneyPanel.SetActive(false);
        }else Debug.Log("No award given. Result was :: " + result);
    }
    
    public IEnumerator lengthRoadText_On(float waitTime)
    {
        gameSystem.lengthRoadText.text = "Пройдено с.л: " + (lengthRoad + Random.Range(100, 600));
        gameSystem.lengthRoadText.enabled = true;
        yield return new WaitForSeconds(waitTime);
        gameSystem.lengthRoadText.enabled = false;
        StopCoroutine(lengthRoadText_On(waitTime));
    }
}
