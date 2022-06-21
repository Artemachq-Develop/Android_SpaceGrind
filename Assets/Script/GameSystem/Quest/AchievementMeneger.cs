using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMeneger : MonoBehaviour
{
    public GameObject[] buttonCheck;
    public Text[] missionText;

    public GuildSystem_Save guildSystemSave;
    public GameManager gameManager;
    public Menu_HUD menuHud;
    public LanguageMar languageMar;
    public Sprite readySprite;
    
    private static SaveAchievements saveAchievements = new SaveAchievements();

    public DropBoxObject dropBoxObject;

    public void CheckAchievement()
    {
        for (int i = 0; i < missionText.Length; i++)
        {
            missionText[i].text = languageMar.GetTest(missionText[i].gameObject.name);
        }

        if (achievementStatic.killCount >= 20)
        {
            buttonCheck[0].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady1){ buttonCheck[0].GetComponent<Button>().image.sprite = readySprite;}
            if (achievementStatic.killCount >= 50)
            {
                buttonCheck[1].GetComponent<Button>().interactable = true;
                if (achievementStatic.isReady2){ buttonCheck[1].GetComponent<Button>().image.sprite = readySprite;}
                if (achievementStatic.killCount >= 100)
                {
                    buttonCheck[2].GetComponent<Button>().interactable = true;
                    if (achievementStatic.isReady3){ buttonCheck[2].GetComponent<Button>().image.sprite = readySprite;}
                    if (achievementStatic.killCount >= 1000)
                    {
                        buttonCheck[3].GetComponent<Button>().interactable = true;
                        if (achievementStatic.isReady4){ buttonCheck[3].GetComponent<Button>().image.sprite = readySprite;}
                    }
                }
            }
        }
        else
        {
            buttonCheck[0].GetComponent<Button>().interactable = false;
            buttonCheck[1].GetComponent<Button>().interactable = false;
            buttonCheck[2].GetComponent<Button>().interactable = false;
            buttonCheck[3].GetComponent<Button>().interactable = false;
        }
        //
        if (achievementStatic.bossKillCount >= 1)
        {
            buttonCheck[4].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady5){ buttonCheck[4].GetComponent<Button>().image.sprite = readySprite;}
            if (achievementStatic.bossKillCount >= 10)
            {
                buttonCheck[5].GetComponent<Button>().interactable = true;
                if (achievementStatic.isReady6){ buttonCheck[5].GetComponent<Button>().image.sprite = readySprite;}
                if (achievementStatic.bossKillCount >= 50)
                {
                    buttonCheck[6].GetComponent<Button>().interactable = true;
                    if (achievementStatic.isReady7){ buttonCheck[6].GetComponent<Button>().image.sprite = readySprite;}
                }
            }
        }
        else
        {
            buttonCheck[4].GetComponent<Button>().interactable = false;
            buttonCheck[5].GetComponent<Button>().interactable = false;
            buttonCheck[6].GetComponent<Button>().interactable = false;
        }
        
        //
        if (achievementStatic.touchDamageCount >= 10)
        {
            buttonCheck[7].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady8){ buttonCheck[7].GetComponent<Button>().image.sprite = readySprite;}
            if (achievementStatic.touchDamageCount >= 100)
            {
                buttonCheck[8].GetComponent<Button>().interactable = true;
                if (achievementStatic.isReady9){ buttonCheck[8].GetComponent<Button>().image.sprite = readySprite;}
                if (achievementStatic.touchDamageCount >= 1000)
                {
                    buttonCheck[9].GetComponent<Button>().interactable = true;
                    if (achievementStatic.isReady10){ buttonCheck[9].GetComponent<Button>().image.sprite = readySprite;}
                }
            }
        }
        else
        {
            buttonCheck[7].GetComponent<Button>().interactable = false;
            buttonCheck[8].GetComponent<Button>().interactable = false;
            buttonCheck[9].GetComponent<Button>().interactable = false;
        }
        //
        if (guildSystemSave.mechanicLevel >= 10)
        {
            buttonCheck[10].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady11){ buttonCheck[10].GetComponent<Button>().image.sprite = readySprite;}
        }
        else
        {
            buttonCheck[10].GetComponent<Button>().interactable = false;
        }
        
        if (guildSystemSave.doctorLevel >= 10)
        {
            buttonCheck[11].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady12){ buttonCheck[11].GetComponent<Button>().image.sprite = readySprite;}
        }
        else
        {
            buttonCheck[11].GetComponent<Button>().interactable = false;
        }

        if (guildSystemSave.engeneerLevel >= 10)
        {
            buttonCheck[12].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady13){ buttonCheck[12].GetComponent<Button>().image.sprite = readySprite;}
        }
        else
        {
            buttonCheck[12].GetComponent<Button>().interactable = false;
        }
        
        if (guildSystemSave.pilotLevel >= 10)
        {
            buttonCheck[13].GetComponent<Button>().interactable = true;
            if (achievementStatic.isReady14){ buttonCheck[13].GetComponent<Button>().image.sprite = readySprite;}
        }
        else
        {
            buttonCheck[13].GetComponent<Button>().interactable = false;
        }
    }
    
    public static void SaveAchievement()
    {
        saveAchievements.killCount_Save = achievementStatic.killCount;
        saveAchievements.bossKillCount_Save = achievementStatic.bossKillCount;
        saveAchievements.touchDamageCount_Save = achievementStatic.touchDamageCount;

        saveAchievements.isReady1_Save = achievementStatic.isReady1;
        saveAchievements.isReady2_Save = achievementStatic.isReady2;
        saveAchievements.isReady3_Save = achievementStatic.isReady3;
        saveAchievements.isReady4_Save = achievementStatic.isReady4;
        saveAchievements.isReady5_Save = achievementStatic.isReady5;
        saveAchievements.isReady6_Save = achievementStatic.isReady6;
        saveAchievements.isReady7_Save = achievementStatic.isReady7;
        saveAchievements.isReady8_Save = achievementStatic.isReady8;
        saveAchievements.isReady9_Save = achievementStatic.isReady9;
        saveAchievements.isReady10_Save = achievementStatic.isReady10;
        saveAchievements.isReady11_Save = achievementStatic.isReady11;
        saveAchievements.isReady12_Save = achievementStatic.isReady12;
        saveAchievements.isReady13_Save = achievementStatic.isReady13;
        saveAchievements.isReady14_Save = achievementStatic.isReady14;

        //Сохранение
        PlayerPrefs.SetString("SaveAchievements", JsonUtility.ToJson(saveAchievements));
    }
    
    public void LoadAchievement()
    {
        if (!PlayerPrefs.HasKey("SaveAchievements"))
        {
            Debug.Log("Dont Exist Save File (Achievement)");
            deleteAchievement();
            SaveAchievement();
            LoadAchievement();
            CheckAchievement();
        }
        else
        {
            saveAchievements = JsonUtility.FromJson<SaveAchievements>(PlayerPrefs.GetString("SaveAchievements"));
            
            achievementStatic.killCount = saveAchievements.killCount_Save;
            achievementStatic.bossKillCount = saveAchievements.bossKillCount_Save;
            achievementStatic.touchDamageCount = saveAchievements.touchDamageCount_Save;
            
            achievementStatic.isReady1 = saveAchievements.isReady1_Save;
            achievementStatic.isReady2 = saveAchievements.isReady2_Save;
            achievementStatic.isReady3 = saveAchievements.isReady3_Save;
            achievementStatic.isReady4 = saveAchievements.isReady4_Save;
            achievementStatic.isReady5 = saveAchievements.isReady5_Save;
            achievementStatic.isReady6 = saveAchievements.isReady6_Save;
            achievementStatic.isReady7 = saveAchievements.isReady7_Save;
            achievementStatic.isReady8 = saveAchievements.isReady8_Save;
            achievementStatic.isReady9 = saveAchievements.isReady9_Save;
            achievementStatic.isReady10 = saveAchievements.isReady10_Save;
            achievementStatic.isReady11 = saveAchievements.isReady11_Save;
            achievementStatic.isReady12 = saveAchievements.isReady12_Save;
            achievementStatic.isReady13 = saveAchievements.isReady13_Save;
            achievementStatic.isReady14 = saveAchievements.isReady14_Save;
        }
    }

    public void onClickAch1()
    {
        if (!achievementStatic.isReady1)
        {
            gameManager.allScore += 100;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 100);
            buttonCheck[0].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady1 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch2()
    {
        if (!achievementStatic.isReady2)
        {
            gameManager.allScore += 200;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 200);
            buttonCheck[1].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady2 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch3()
    {
        if (!achievementStatic.isReady3)
        {
            gameManager.allScore += 500;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 500);
            buttonCheck[2].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady3 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch4()
    {
        if (!achievementStatic.isReady4)
        {
            gameManager.allScore += 1000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 1000);
            buttonCheck[3].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady4 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch5()
    {
        if (!achievementStatic.isReady5)
        {
            gameManager.allScore += 100;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 100);
            buttonCheck[4].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady5 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch6()
    {
        if (!achievementStatic.isReady6)
        {
            gameManager.allScore += 600;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 600);
            buttonCheck[5].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady6 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch7()
    {
        if (!achievementStatic.isReady7)
        {
            gameManager.allScore += 2000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 2000);
            buttonCheck[6].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady7 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch8()
    {
        if (!achievementStatic.isReady8)
        {
            gameManager.allScore += 100;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 100);
            buttonCheck[7].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady8 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch9()
    {
        if (!achievementStatic.isReady9)
        {
            gameManager.allScore += 400;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 400);
            buttonCheck[8].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady9 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch10()
    {
        if (!achievementStatic.isReady10)
        {
            gameManager.allScore += 1000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 1000);
            buttonCheck[9].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady10 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch11()
    {
        if (!achievementStatic.isReady11)
        {
            gameManager.allScore += 1000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 1000);
            buttonCheck[10].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady11 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch12()
    {
        if (!achievementStatic.isReady12)
        {
            gameManager.allScore += 1000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 1000);
            buttonCheck[11].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady12 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch13()
    {
        if (!achievementStatic.isReady13)
        {
            gameManager.allScore += 1000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 1000);
            buttonCheck[12].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady13 = true;
            SaveAchievement();
        }
    }
    
    public void onClickAch14()
    {
        if (!achievementStatic.isReady14)
        {
            gameManager.allScore += 1000;
            dropBoxObject.ShowNotification(menuHud.moneySprite, "Валюта", "Currency", 1000);
            buttonCheck[13].GetComponent<Button>().image.sprite = readySprite;
            achievementStatic.isReady14 = true;
            SaveAchievement();
        }
    }

    public void deleteAchievement()
    {
        achievementStatic.killCount = 0;
        achievementStatic.touchDamageCount = 0;
        achievementStatic.bossKillCount = 0;
    }
    public static class achievementStatic
    {
        public static int killCount;
        public static int touchDamageCount;
        public static int bossKillCount;

        public static bool isReady1;
        public static bool isReady2;
        public static bool isReady3;
        public static bool isReady4;
        public static bool isReady5;
        public static bool isReady6;
        public static bool isReady7;
        public static bool isReady8;
        public static bool isReady9;
        public static bool isReady10;
        public static bool isReady11;
        public static bool isReady12;
        public static bool isReady13;
        public static bool isReady14;
    }
}


