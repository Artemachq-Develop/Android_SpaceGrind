using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuildSystem_Save : MonoBehaviour
{
    public int mechanicLevel = 1;
    public int engeneerLevel = 1;
    public int doctorLevel = 1;
    public int pilotLevel = 1;

    private int mechanicPrice = 20;
    private int engeneerPrice = 20;
    private int doctorPrice = 30;
    private int pilotPrice = 100;
    
    public byte chooseButton;

    public GameManager gameManager;
    public Menu_HUD menuHud;
    private SaveGuild saveGuild = new SaveGuild();

    private void Start()
    {
        menuHud.guildDiscriptionPanel.SetActive(false);
    }

    public void GuildObjectInfo_1()
    {
        if (chooseButton != 1)
        {
            chooseButton = 1;
            LoadGuild();
            menuHud.guildNameText.text = returnNamePerson("Доктор", "Doctor");
            ReturnLanguageValue(
                Random.Range(0, 40) + " к харизме " + '\n' + Random.Range(0, 40) + " к интеллекту", 
                Random.Range(0, 40) + " for charisma" + '\n' + Random.Range(0, 40) + " to intelligence",
                doctorPrice, doctorLevel);
            menuHud.guildPriceText.text = returnPrice(doctorPrice, "бладстоун", "bloodstone");
            menuHud.guildDiscriptionPanel.SetActive(true);
        }
    }
    
    public void GuildObjectInfo_2()
    {
        if (chooseButton != 2)
        {
            chooseButton = 2;
            LoadGuild();
            menuHud.guildNameText.text = returnNamePerson("Инженер", "Engeneer");
            ReturnLanguageValue(
                Random.Range(0, 40) + " к починке" + '\n' + Random.Range(0, 40) + " к выносливости", 
                Random.Range(0, 40) + " to fix" + '\n' + Random.Range(0, 40) + " to endurance",
                engeneerPrice, engeneerLevel);
            menuHud.guildPriceText.text = returnPrice(engeneerPrice, "металлолом", "scrap metal");
            menuHud.guildDiscriptionPanel.SetActive(true);
        }
    }
    
    public void GuildObjectInfo_3()
    {
        if (chooseButton != 3)
        {
            chooseButton = 3;
            LoadGuild();
            menuHud.guildNameText.text = returnNamePerson("Механик", "Mechanic");
            ReturnLanguageValue(
                Random.Range(0, 40) + " к управлению" + '\n' + "-" + Random.Range(0, 40) + " к рекламе", 
                Random.Range(0, 40) + " to manage" + '\n' + "-" + Random.Range(0, 40) + " to advertising",
                mechanicPrice, mechanicLevel);
            menuHud.guildPriceText.text = returnPrice(mechanicPrice, "металлолом", "scrap metal");
            menuHud.guildDiscriptionPanel.SetActive(true);
        }
    }
    
    public void GuildObjectInfo_4()
    {
        if (chooseButton != 4)
        {
            chooseButton = 4;
            LoadGuild();
            menuHud.guildNameText.text = returnNamePerson("Пилот", "Pilot");
            ReturnLanguageValue(
                Random.Range(0, 40) + " к пилотированию" + '\n' + Random.Range(0, 40) + " к гринду", 
                Random.Range(0, 40) + " to pilot" + '\n' + Random.Range(0, 40) + " to grind",
                pilotPrice, pilotLevel);
            menuHud.guildPriceText.text = returnPrice(pilotPrice, "валюты", "currency");
            menuHud.guildDiscriptionPanel.SetActive(true);
        }
    }

    public void GuildBuyButton()
    {
        if (chooseButton == 1 && gameManager.woodCount >= doctorPrice)
        {
            gameManager.woodCount -= doctorPrice;
            doctorLevel++;
            doctorPrice += 20;
            menuHud.guildLevelText.text = returnLevel(doctorLevel);
            menuHud.guildPriceText.text = returnPrice(doctorPrice, "бладстоун", "bloodstone");
            SaveGuild();
            gameManager.SaveFile();
        } else if (chooseButton == 2 && gameManager.rockCount >= engeneerPrice)
        {
            gameManager.rockCount -= engeneerPrice;
            engeneerLevel++;
            engeneerPrice += 10;
            menuHud.guildLevelText.text = returnLevel(engeneerLevel);
            menuHud.guildPriceText.text = returnPrice(engeneerPrice, "металлолом", "scrap metal");
            SaveGuild();
            gameManager.SaveFile();
        } else if (chooseButton == 3 && gameManager.rockCount >= mechanicPrice)
        {
            gameManager.rockCount -= mechanicPrice;
            mechanicLevel++;
            mechanicPrice += 10;
            menuHud.guildLevelText.text = returnLevel(mechanicLevel);
            menuHud.guildPriceText.text = returnPrice(mechanicPrice, "металлолом", "scrap metal");
            SaveGuild();
            gameManager.SaveFile();
        } else if (chooseButton == 4 && gameManager.allScore >= pilotPrice)
        {
            gameManager.allScore -= pilotPrice;
            pilotLevel++;
            pilotPrice += 100;
            menuHud.guildLevelText.text = returnLevel(pilotLevel);
            menuHud.guildPriceText.text = returnPrice(pilotPrice, "валюты", "currency");
            SaveGuild();
            gameManager.SaveFile();
        }
        menuHud.guildWoodText.text = gameManager.woodCount.ToString();
        menuHud.guildRockText.text = gameManager.rockCount.ToString();
    }

    private string returnNamePerson(string nameP_ru, string nameP_eng)
    {
        if (gameManager.languageInt == 1)
        {
            string value = nameP_ru;
            return value;
        }else if (gameManager.languageInt == 0)
        {
            string value = nameP_eng;
            return value;
        }
        else{return "";}
    }
    
    private void ReturnLanguageValue(string ru_Discription, string eng_Discrition, int priceObject, int levelObject)
    {
        if (gameManager.languageInt == 1)
        {
            menuHud.guildDiscriptionText.text = ru_Discription;
            menuHud.guildPriceText.text = "Цена: " + priceObject;
            menuHud.guildLevelText.text = "Уровень: " + levelObject;
        }else if (gameManager.languageInt == 0)
        {
            menuHud.guildDiscriptionText.text = eng_Discrition;
            menuHud.guildPriceText.text = "Price: " + priceObject;
            menuHud.guildLevelText.text = "Level: " + levelObject;
        }
    }
    
    private string returnPrice(int priceObject, string nameM_ru, string nameM_eng)
    {
        if (gameManager.languageInt == 1)
        {
            string value = "Цена: " + priceObject + " " + "\n" + nameM_ru;
            return value;
        }else if (gameManager.languageInt == 0)
        {
            string value = "Price: " + priceObject + " " + nameM_eng;
            return value;
        }
        else{return "";}
    }
    
    private string returnLevel(int priceObject)
    {
        if (gameManager.languageInt == 1)
        {
            string value = "Уровень: " + priceObject;
            return value;
        }else if (gameManager.languageInt == 0)
        {
            string value = "Level: " + priceObject;
            return value;
        }
        else{return "";}
    }
    
    public void LoadGuild()
    {
        if (!PlayerPrefs.HasKey("SaveGuild"))
        {
            Debug.Log("Dont Exist Save File (Guild)");
            SaveGuild();
        }
        else
        {
            saveGuild = JsonUtility.FromJson<SaveGuild>(PlayerPrefs.GetString("SaveGuild"));

            mechanicLevel = saveGuild.mechanicLevel_Save;
            engeneerLevel = saveGuild.engeneerLevel_Save;
            doctorLevel = saveGuild.doctorLevel_Save;
            pilotLevel = saveGuild.pilotLevel_Save;

            mechanicPrice = saveGuild.mechanicPrice_Save;
            engeneerPrice = saveGuild.engeneerPrice_Save;
            doctorPrice = saveGuild.doctorPrice_Save;
            pilotPrice = saveGuild.pilotPrice_Save;
        }
    }
    
    public void SaveGuild()
    {
        saveGuild.mechanicLevel_Save = mechanicLevel;
        saveGuild.engeneerLevel_Save = engeneerLevel;
        saveGuild.doctorLevel_Save = doctorLevel;
        saveGuild.pilotLevel_Save = pilotLevel;

        saveGuild.mechanicPrice_Save = mechanicPrice;
        saveGuild.engeneerPrice_Save = engeneerPrice;
        saveGuild.doctorPrice_Save = doctorPrice;
        saveGuild.pilotPrice_Save = pilotPrice;

        //Сохранение
        PlayerPrefs.SetString("SaveGuild", JsonUtility.ToJson(saveGuild));
    }

    public void DeleteGuild()
    {
        mechanicLevel = 1;
        engeneerLevel = 1;
        doctorLevel = 1;
        pilotLevel = 1;

        mechanicPrice = 20;
        engeneerPrice = 20;
        doctorPrice = 30;
        pilotPrice = 100;
    }

    public void GuildPanelOpen()
    {
        
        LoadGuild();
        menuHud.guildWoodText.text = gameManager.woodCount.ToString();
        menuHud.guildRockText.text = gameManager.rockCount.ToString();
        menuHud.earthUpgrade.SetActive(true);
        menuHud.soundManager.SoundHit(menuHud.soundManager.buttonSound);
    }
    
    public void GuildPanelClose()
    {
        SaveGuild();
        menuHud.earthUpgrade.SetActive(false);
        menuHud.guildDiscriptionPanel.SetActive(false);
        chooseButton = 0;
        menuHud.soundManager.SoundHit(menuHud.soundManager.buttonSound);
    }
}
