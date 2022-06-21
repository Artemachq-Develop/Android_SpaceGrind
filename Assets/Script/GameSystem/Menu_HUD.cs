using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Menu_HUD : MonoBehaviour
{
    [Header("GameObject \n")]
    public GameObject menuHUD;
    public GameObject gameHUD;
    public GameObject shopHUD;
    public GameObject deleteHUD;
    public GameObject panelShop;
    public GameObject missionPanel;
    public GameObject boxDropPanel;
    public GameObject boxDropImage;
    public GameObject boxDropNotificationPopupObject;
    public GameObject settingsPanel;
    public GameObject earthUpgrade;
    public GameObject shopSkillPanel;
    public GameObject shopSkinPanel;
    public GameObject achievementHUD;
    public GameObject questHUD;
    public GameObject oneGameAbilityHUD;
    public GameObject guildDiscriptionPanel;
    public GameObject freeAdMoneyPanel;
    
    [Header("Text \n")]
    public Text nameText;
    public Text priceText;
    public Text recordText;
    public Text shopScoreText;
    public Text needLevelText;
    public Text allScoreText;
    public Text OneGameAbilityScoreText;
    public Text boxCountText;
    public Text boxDropObjectWoodCountText;
    public Text boxDropObjectRockCountText;
    public Text goldCoinText;
    public Text expText;
    public Text guildNameText;
    public Text guildLevelText;
    public Text guildDiscriptionText;
    public Text guildPriceText;
    public Text guildWoodText;
    public Text guildRockText;
    
    [Header("Image \n")]
    public Image upgradeLevel;
    public Image boxDropButtonImage;
    public Image earthUpgradeImage;
    public Image expImage;

    [Header("Sprite \n")]

    public Sprite[] boxDropSprite;

    public Sprite[] earthUpgradeSprite;
    public int earthUpgradeLevelCount;

    public Sprite moneySprite;

    public Sprite defaultBulletSprite;
    
    [Header("Other \n")]
    public Transform boxDropNotificationPopupTransform;
    
    public Camera cameraMain;

    public PlayerMove playerMove;

    public Animator playerAnimator;

    public Animator popupAnimator;

    public GameSystem_HUD gameSystem;

    public StructureSpawn structureSpawn;

    [Header("Managers")]

    public GameManager gameManager;
    public SoundManager soundManager;
    public LanguageMar languageMar;
    public GuildSystem_Save guildSystemSave;
    public ShopSystem_Save shopSystemSave;
    public SkinChange skinChange;
    
    // Start is called before the first frame update
    void Start()
    {
        recordText.text = languageMar.GetTest(recordText.gameObject.name) + gameManager.record;

        byte randomFree;
        randomFree = (byte)Random.Range(0, 14);

        freeAdMoneyPanel.SetActive(randomFree == 0 ? true : false);

        if (gameManager.score > gameManager.record)
        {
            gameManager.record = gameManager.score;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        shopScoreText.text = languageMar.GetTest(allScoreText.gameObject.name) + gameManager.allScore;

        goldCoinText.text = languageMar.GetTest(goldCoinText.gameObject.name) + gameManager.goldCount.ToString();

        allScoreText.text = languageMar.GetTest(allScoreText.gameObject.name) + formatNum.FormatNum((float)gameManager.allScore);

        if (gameManager.allScore < 0)
        {
            gameManager.allScore = 0;
        }

        expText.text = gameManager.lvlCount.ToString();

        expImage.fillAmount = gameManager.expCount / 10f;
    }

    public void playGame()
    {
        enabled = false;
        gameSystem.enabled = true;

        playerMove.enabled = true;
        playerMove.GetComponent<ScaleShip>().enabled = true;
        gameManager.isPlay = true;
        playerAnimator.enabled = true;

        structureSpawn.enabled = true;
        
        menuHUD.SetActive(false);
        gameHUD.SetActive(true);

        Time.timeScale = 1;
        
        gameManager.musicSource.Play();

        if (gameManager.playerAddLife == 1)
        {
            gameSystem.playerAddLifeObject.SetActive(true);
            gameSystem.isAddLife = true;
        }
        else if (gameManager.playerAddLife == 0)
        {
            gameSystem.playerAddLifeObject.SetActive(false);
            gameSystem.isAddLife = false; 
        }
        
        gameManager.StartCoroutine("infinityHealth", 3f);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void isDeleteOpen()
    {
        deleteHUD.SetActive(true);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void isDeleteClose()
    {
        deleteHUD.SetActive(false);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void deleteSave()
    {
        PlayerPrefs.DeleteAll();
        isDeleteClose();
        updateDeleteSave();
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void endGameButton()
    {
        AchievementMeneger.SaveAchievement();
        SceneManager.LoadScene(2);
        gameManager.score = 0;
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void boxDropOn()
    {
        boxDropPanel.SetActive(true);

        boxCountText.text = languageMar.GetTest(boxCountText.gameObject.name) + '\n' + gameManager.boxCount;

        boxDropObjectWoodCountText.text = gameManager.woodCount.ToString();
        boxDropObjectRockCountText.text = gameManager.rockCount.ToString();

        if (gameManager.boxCount > 0)
        {
            boxDropImage.SetActive(true);
        }
        else
        {
            boxDropImage.SetActive(false);
        }
        
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void boxDropOff()
    {
        boxDropPanel.SetActive(false);
        soundManager.SoundHit(soundManager.buttonSound);
    }
    
    public void boxDropButton()
    {
        if (gameManager.boxCount > 0)
        {
            gameManager.hpBox--;
            soundManager.SoundHit(soundManager.boxDropClickSound);
            if (gameManager.hpBox <= 0)
            {
                boxDropButtonImage.sprite = boxDropSprite[0];
                
                popupAnimator.SetBool("isActive", true);
                
                popupAnimator.gameObject.GetComponent<DropBoxObject>().AttackSpawn();

                soundManager.SoundHit(soundManager.boxDropCrackSound);
                gameManager.hpBox = 5;
                gameManager.boxCount--;

                boxCountText.text = languageMar.GetTest(boxCountText.gameObject.name) + '\n' + gameManager.boxCount;

                boxDropObjectWoodCountText.text = gameManager.woodCount.ToString();
                boxDropObjectRockCountText.text = gameManager.rockCount.ToString();
                
                if (gameManager.boxCount <= 0)
                {
                    boxDropImage.SetActive(false);
                }
                
                gameManager.SaveFile();
            }

            if (gameManager.hpBox == 4){boxDropButtonImage.sprite = boxDropSprite[1];}
            else if (gameManager.hpBox == 3){boxDropButtonImage.sprite = boxDropSprite[2];}
            else if (gameManager.hpBox == 2){boxDropButtonImage.sprite = boxDropSprite[3];}
            else if (gameManager.hpBox == 1){boxDropButtonImage.sprite = boxDropSprite[4];}
        }
    }

    public void BoxDropBuyAd()
    {
        gameManager.ShowAd(1);
    }

    public void FreeMoneyAdButton()
    {
        gameManager.ShowAd(2);
    }

    public void settingsMenuOn()
    {
        settingsPanel.SetActive(true);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void settingsMenuOff()
    {
        settingsPanel.SetActive(false);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void oneGameAbilityHUD_On()
    {
        oneGameAbilityHUD.SetActive(true);
        OneGameAbilityScoreText.text = languageMar.GetTest(allScoreText.gameObject.name) + gameManager.allScore;
        soundManager.SoundHit(soundManager.buttonSound);
    }
    
    public void oneGameAbilityHUD_Off()
    {
        oneGameAbilityHUD.SetActive(false);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void oneGameAbilityButton_1(Button button)
    {
        if (gameManager.allScore >= 200)
        {
            gameManager.allScore -= 200;
            OneGameAbilityScoreText.text = languageMar.GetTest(allScoreText.gameObject.name) + gameManager.allScore;
            gameManager.isOneShootAsteroid = true;
            soundManager.SoundHit(soundManager.buttonSound);
            button.interactable = false;
        }
    }
    
    public void oneGameAbilityButton_2(Button button)
    {
        if (gameManager.allScore >= 400)
        {
            gameManager.allScore -= 400;
            OneGameAbilityScoreText.text = languageMar.GetTest(allScoreText.gameObject.name) + gameManager.allScore;
            gameManager.isMoreMoney = true;
            soundManager.SoundHit(soundManager.buttonSound);
            button.interactable = false;
        }
    }
    
    public void oneGameAbilityButton_3(Button button)
    {
        if (gameManager.allScore >= 400)
        {
            gameManager.allScore -= 400;
            OneGameAbilityScoreText.text = languageMar.GetTest(allScoreText.gameObject.name) + gameManager.allScore;
            gameManager.levelCount = 8;
            soundManager.SoundHit(soundManager.buttonSound);
            button.interactable = false;
        }
    }

    public void shopSkillOpen()
    {
        shopSkillPanel.SetActive(true);
        shopSkinPanel.SetActive(false);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void shopSkinOpen()
    {
        shopSkillPanel.SetActive(false);
        shopSkinPanel.SetActive(true);
        soundManager.SoundHit(soundManager.buttonSound);
    }

    public void rusLang_button()
    {
        gameManager.languageInt = 1;
        gameManager.SaveFile();

        SceneManager.LoadScene(2);
    }

    public void enLang_button()
    {
        gameManager.languageInt = 0;
        gameManager.SaveFile();

        SceneManager.LoadScene(2);
    }

    public void updateDeleteSave()
    {
        playerMove.fireRate = 0.5f;
        gameManager.allScore = 0;
        playerMove.dashInt = 0f;
        gameManager.fuelSpeed = 2.6f;
        playerMove.allBullet = 5;
        playerMove.maxBullet = 10;
        gameManager.bulletSpeedReload = 0.01f;
        gameManager.record = 0;
        gameManager.musicCheck = 1;
        gameManager.goldCount = 0;
        gameManager.boxCount = 0;
        gameManager.woodCount = 0;
        gameManager.rockCount = 0;
        gameManager.controlCount = 0;
        skinChange.restartSkin();

        guildSystemSave.DeleteGuild();
        shopSystemSave.DeleteShop();

        playerMove.playerDamage = 10;
        gameManager.playerAddLife = 0;
        gameManager.expBonus = 0f;
        gameManager.boxBonus = 0;
        gameManager.bulletBonus = 0;    

        gameManager.expCount = 0;
        gameManager.lvlCount = 0;
        
        recordText.text = "Ваш рекорд: " + gameManager.record;

        gameManager.skinNumber = 0;

        gameManager.SaveFile();
        shopSystemSave.SaveShop();
        guildSystemSave.SaveGuild();

        SceneManager.LoadScene(2);
    }
}
