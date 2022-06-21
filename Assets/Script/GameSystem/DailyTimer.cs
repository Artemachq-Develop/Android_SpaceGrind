using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DailyTimer : MonoBehaviour
{
    public float msToWait = 24f;
    
    public Text timer;
    public Text mainText;
    public Text giftCountText;
    public  Button RewardButton;
    
    private ulong lastOpen;

    public GameObject dailyGiftPanel;

    public GameManager gameManager;
    public LanguageMar languageMar;
    public DailyQuestSystem dailyQuestSystem;

    private int giftCount;
    
    private void Awake()
    {
        msToWait *= 3600000;
        if (!PlayerPrefs.HasKey("lastOpen"))
            PlayerPrefs.SetString("lastOpen", lastOpen.ToString());
    }
    private void Start()
    {
        StartCoroutine(LateStart(0.5f));
    }

    private void Update()
    {
        
    }

    public void Click()
    {
        lastOpen = (ulong) DateTime.Now.Ticks;
        PlayerPrefs.SetString("lastOpen", lastOpen.ToString());
        RewardButton.interactable = false;
        dailyGiftPanel.SetActive(false);
        gameManager.allScore += giftCount;
    }

    bool isReady()
    {
        ulong diff = ((ulong) DateTime.Now.Ticks - lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconLeft = (float) ((msToWait) - m) / 1000f;

        if (seconLeft < 0)
        {
            if (gameManager.languageInt == 0)
            {
                timer.text = "Ready";
                mainText.text = "Your daily gift:";
            } else if (gameManager.languageInt == 1)
            {
                timer.text = "Готово";
                mainText.text = "Ваш ежедневный подарок:";            
            }
            dailyGiftPanel.SetActive(true);
            giftCount = gameManager.randomGiftCount();
            giftCountText.text = languageMar.GetTest(giftCountText.gameObject.name) + '\n' + giftCount.ToString();
            dailyQuestSystem.deleteDayliQuestInfo();
            return true;
        }

        return false;
    }
    
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        lastOpen = ulong.Parse(PlayerPrefs.GetString("lastOpen"));

        if (!isReady())
        {
            RewardButton.interactable = false;
            dailyGiftPanel.SetActive(false);
        }
        
        if (!RewardButton.IsInteractable())
        {
            if (isReady())
            {
                RewardButton.interactable = true;
                if (gameManager.languageInt == 0)
                {
                    timer.text = "Ready";
                } else if (gameManager.languageInt == 1)
                {
                    timer.text = "Готово";
                }
                dailyGiftPanel.SetActive(true);
                yield break;
            }
            ulong diff = ((ulong) DateTime.Now.Ticks - lastOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float seconLeft = (float) ((msToWait) - m) / 1000f;

            string t = "";

            t += ((int) seconLeft / 3600).ToString() + "ч ";
            seconLeft -= ((int) seconLeft / 3600) * 3600;
            t += ((int) seconLeft / 60).ToString("00") + "м ";
            t += ((int) seconLeft % 60).ToString("00") + "с ";

            timer.text = t;
        }
        StopCoroutine(LateStart(0.5f));
    }
}
