using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DailyQuestSystem : MonoBehaviour
{
    //Variable
    public int requestCount1 = 0;
    public int requestCount2 = 0;
    public int requestCount3 = 0;
    
    public int completeCount1;
    public int completeCount2;
    public int completeCount3;

    private int rewardCount1;
    private int rewardCount2;
    private int rewardCount3;
    
    //Text
    public Text nameText1;
    public Text nameText2;
    public Text nameText3;
    [Space]
    public Text Require_1;
    public Text Require_2;
    public Text Require_3;
    [Space]
    public Button checkButton1;
    public Button checkButton2;
    public Button checkButton3;
    
    private SaveQuest saveQuest = new SaveQuest();
    public GameManager gameManager;
    public Menu_HUD menuHud;
    public AchievementMeneger achievementMeneger;
    public DropBoxObject dropBoxObject;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        if (requestCount1 < completeCount1)
        {
            if (gameManager.languageInt == 1)
            {
                checkButton1.GetComponentInChildren<Text>().text = "Не завершено";
            }
            else if (gameManager.languageInt == 0)
            {
                checkButton1.GetComponentInChildren<Text>().text = "Not completed";
            }
        }
        
        if (requestCount2 < completeCount2)
        {
            if (gameManager.languageInt == 1)
            {
                checkButton2.GetComponentInChildren<Text>().text = "Не завершено";
            }
            else if (gameManager.languageInt == 0)
            {
                checkButton2.GetComponentInChildren<Text>().text = "Not completed";
            }
        }
        
        if (requestCount3 < completeCount3)
        {
            if (gameManager.languageInt == 1)
            {
                checkButton3.GetComponentInChildren<Text>().text = "Не завершено";
            }
            else if (gameManager.languageInt == 0)
            {
                checkButton3.GetComponentInChildren<Text>().text = "Not completed";
            }
        }
    }

    public void GenerateStructure()
    {
        completeCount1 = Random.Range(50, 200);
        completeCount2 = Random.Range(4, 10);
        completeCount3 = Random.Range(10, 40);

        rewardCount1 = Random.Range(100, 400);
        rewardCount2 = Random.Range(100, 400);
        rewardCount3 = Random.Range(100, 400);
    }

    public void CompletionText()
    {
        LoadQuest();
        
        if (gameManager.languageInt == 1)
        {
            nameText1.text = "Убить " + completeCount1 + " противников";
            nameText2.text = "Убить " + completeCount2 + " боссов";
            nameText3.text = "Умереть " + completeCount3 + " раз";
        } else if (gameManager.languageInt == 0)
        {
            nameText1.text = "Kill " + completeCount1 + " enemy";
            nameText2.text = "Kill " + completeCount2 + " bosses";
            nameText3.text = "Died " + completeCount3 + " times";
        }

        Require_1.text = requestCount1 + "/" + completeCount1;
        Require_2.text = requestCount2 + "/" + completeCount2;
        Require_3.text = requestCount3 + "/" + completeCount3;
        
        CheckIsReady();
    }

    public void CheckIsReady()
    {
            if (requestCount1 >= completeCount1)
            {
                checkButton1.interactable = true;
                checkButton1.GetComponent<Image>().color = Color.green;
                if (gameManager.languageInt == 1)
                {
                    checkButton1.GetComponentInChildren<Text>().text = "Завершено";
                }
                else if (gameManager.languageInt == 0)
                {
                    checkButton1.GetComponentInChildren<Text>().text = "Complete";
                }
            }
            //
            
            if (requestCount2 >= completeCount2)
            {
                checkButton2.interactable = true;
                checkButton2.GetComponent<Image>().color = Color.green;
                
                if (gameManager.languageInt == 1)
                {
                    checkButton2.GetComponentInChildren<Text>().text = "Завершено";
                }
                else if (gameManager.languageInt == 0)
                {
                    checkButton2.GetComponentInChildren<Text>().text = "Complete";
                }
            }
            //
            
            if (requestCount3 >= completeCount3)
            {
                checkButton3.interactable = true;
                checkButton3.GetComponent<Image>().color = Color.green;
                
                if (gameManager.languageInt == 1)
                {
                    checkButton3.GetComponentInChildren<Text>().text = "Завершено";
                }
                else if (gameManager.languageInt == 0)
                {
                    checkButton3.GetComponentInChildren<Text>().text = "Complete";
                }
            }
    }

    public void questButton1()
    {
        if (rewardCount1 != 0)
        {
            gameManager.allScore += rewardCount1;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", rewardCount1);
            rewardCount1 = 0;
            SaveQuest();
        }
    }
    
    public void questButton2()
    {
        if (rewardCount1 != 0)
        {
            gameManager.allScore += rewardCount2;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", rewardCount2);
            rewardCount2 = 0;
            SaveQuest();
        }
    }
    
    public void questButton3()
    {
        if (rewardCount1 != 0)
        {
            gameManager.allScore += rewardCount3;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", rewardCount3);
            rewardCount3 = 0;
            SaveQuest();
        }
    }
    
    public void LoadQuest()
    {
        if (!PlayerPrefs.HasKey("SaveQuest"))
        {
            Debug.Log("Dont Exist Save File");
            deleteDayliQuestInfo();
            LoadQuest();
        }
        else
        {
                saveQuest = JsonUtility.FromJson<SaveQuest>(PlayerPrefs.GetString("SaveQuest"));

                requestCount1 = saveQuest.dayliQuestRequire1_Save;
                requestCount2 = saveQuest.dayliQuestRequire2_Save;
                requestCount3 = saveQuest.dayliQuestRequire3_Save;

                completeCount1 = saveQuest.dayliQuestComplete1_Save;
                completeCount2 = saveQuest.dayliQuestComplete2_Save;
                completeCount3 = saveQuest.dayliQuestComplete3_Save;

                rewardCount1 = saveQuest.dayliQuestReward1_Save;
                rewardCount2 = saveQuest.dayliQuestReward2_Save;
                rewardCount3 = saveQuest.dayliQuestReward3_Save;
        }
    }
    
    public void SaveQuest()
    {
        saveQuest.dayliQuestRequire1_Save = requestCount1;
        saveQuest.dayliQuestRequire2_Save = requestCount2;
        saveQuest.dayliQuestRequire3_Save = requestCount3;

        saveQuest.dayliQuestComplete1_Save = completeCount1;
        saveQuest.dayliQuestComplete2_Save = completeCount2;
        saveQuest.dayliQuestComplete3_Save = completeCount3;

        saveQuest.dayliQuestReward1_Save = rewardCount1;
        saveQuest.dayliQuestReward2_Save = rewardCount2;
        saveQuest.dayliQuestReward3_Save = rewardCount3;

        //Сохранение
        PlayerPrefs.SetString("SaveQuest", JsonUtility.ToJson(saveQuest));
    }

    public void deleteDayliQuestInfo()
    {
        requestCount1 = 0;
        requestCount2 = 0;
        requestCount3 = 0;
        GenerateStructure();
        SaveQuest();
        CompletionText();
        CheckIsReady();
    }
    
    public void missionOn()
    {
        menuHud.missionPanel.SetActive(true);
        CompletionText();
        /*missionPanel.GetComponent<MissionSystem>().enabled = true;*/
        menuHud.soundManager.SoundHit(menuHud.soundManager.buttonSound);
    }

    public void missionOff()
    {
        menuHud.missionPanel.SetActive(false);
        /*missionPanel.GetComponent<MissionSystem>().enabled = false;*/
        menuHud.soundManager.SoundHit(menuHud.soundManager.buttonSound);
    }

    public void DayliQuestPanelOpen()
    {
        menuHud.questHUD.SetActive(true);
        menuHud.achievementHUD.SetActive(false);
    }
    
    public void achievementHUD_On()
    {
        achievementMeneger.LoadAchievement();
        menuHud.questHUD.SetActive(false);
        menuHud.achievementHUD.SetActive(true);
        achievementMeneger.CheckAchievement();
    }
}
