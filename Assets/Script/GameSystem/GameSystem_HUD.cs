using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem_HUD : MonoBehaviour
{
    [Header("Transform \n")]
    public Transform bulletTextTransform;

    [Header("GameObject \n")]
    public GameObject fuelHUD;
    public GameObject gameHUD;
    public GameObject endGameHud;
    public GameObject pausePanel;
    public GameObject pauseHUD;
    public GameObject touchButtonsPanel;
    public GameObject playerAddLifeObject;

    [Header("UI \n")]
    public Text bulletText;
    public Text scoreText;
    public Text lengthRoadText;
    public Text infinityHealthText;
    public Text adRemainedText;
    public Text adLookText;

    public Image fuelProgress;
    public Image hpImage;
    public Image fuelImage;
    public Image musicImage;

    public Button leftTapButton;
    public Button rightTapButton;

    [Header("Sprite \n")]
    public Sprite[] musicSprite;

    [Header("Another \n")]
    public Camera cameraMain;
    public bool isMusic = true;
    public bool isFuel = false;
    public bool adViewed;
    public bool isAddLife;
    private bool isPauseWas;

    [Header("Scripts \n")]
    public PlayerMove playerMove;
    public Menu_HUD hud;
    public SoundManager soundManager;
    public GameManager gameManager;

    //Private
    

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlay)
        {
            Vector2 namePos = cameraMain.WorldToScreenPoint(bulletTextTransform.transform.position);
            bulletText.transform.position = Vector2.Lerp(bulletText.transform.position, namePos, 6f * Time.deltaTime);

            bulletText.text = playerMove.allBullet.ToString();

            
        }

        if (isFuel)
        {
            isFuelOpen();
        }
    }

    public void isFuelOpen()
    {
        gameManager.fuelTimer -= 0.1f * Time.unscaledDeltaTime;
        Time.timeScale = 0;
        gameManager.isPlay = false;
        bulletText.enabled = false;

        fuelProgress.fillAmount = gameManager.fuelTimer;

        if (gameManager.fuelTimer <= 0 || gameManager.fuelCount >= 100)
        {
            isFuel = false;
            gameManager.isPlay = true;
            gameManager.StartCoroutine("infinityHealth", 1f);
            fuelHUD.SetActive(false);
            bulletText.enabled = true;
            Time.timeScale = 1;
            gameManager.fuelTimer = 1;
        }
    }

    public void fuelTap()
    {
        if (gameManager.fuelCount < 100)
        {
            gameManager.fuelCount += Random.Range(2f, 8f);
            soundManager.SoundHit(soundManager.fuelClickSound[Random.Range(0, soundManager.fuelClickSound.Length)]);
        }
    }

    public void deadPanelButton_Continue()
    {
        if (gameManager.goldCount >= 5)
        {
            ResetContinueGame();
            gameManager.goldCount -= 5;
        }
    }
    public void adPanelButton_Continue()
    {
        if (!adViewed)
        {
            gameManager.ShowAd(0);
            adViewed = true;
        }
    }

    public void ResetContinueGame()
    {
        playerMove.isDead = false;
        playerMove.HP = 10;
        gameManager.isPlay = true;
        gameManager.fuelCount = 100;
        gameManager.StartCoroutine("infinityHealth", 3f);
        gameHUD.SetActive(true);
        endGameHud.SetActive(false);
        playerMove.enabled = true;

        Time.timeScale = 1;
    }

    public void isPauseOn()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        gameManager.isPlay = false;
    }

    public void isPauseOff()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        if (!isPauseWas)
        {
            gameManager.StartCoroutine("infinityHealth", 3f);
        }
        gameManager.isPlay = true;
        isPauseWas = true;
    }

    public void swipeSettingButton()
    {
        gameManager.isControlSwipe = true;
        gameManager.controlCount = 0;
        touchButtonsPanel.SetActive(false);
    }

    public void touchSettingButton()
    {
        gameManager.isControlSwipe = false;
        gameManager.controlCount = 1;
        touchButtonsPanel.SetActive(true);
    }

    public void musicOff()
    {
        isMusic = !isMusic;
        if (isMusic)
        {
            musicImage.sprite = musicSprite[0];
            gameManager.musicSource.volume = 0.008f;
            gameManager.musicCheck = 1;
        }
        else if (!isMusic)
        {
            musicImage.sprite = musicSprite[1];
            gameManager.musicSource.volume = 0f;
            gameManager.musicCheck = 0;
        }
    }

    public void unPause()
    {
        if (playerMove.isDead)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            gameHUD.SetActive(true);
            pauseHUD.SetActive(false);
            playerMove.enabled = true;

            Time.timeScale = 1;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Debug.Log("Focus");
        }
        else
        {
            if (gameHUD.activeInHierarchy)
            {
                gameHUD.SetActive(false);
                pauseHUD.SetActive(true);
                playerMove.enabled = false;

                Time.timeScale = 0;

                Debug.Log("Unfocus");
            }
        }
    }
}
