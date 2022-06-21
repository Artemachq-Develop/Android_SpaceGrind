using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopSystem_Save : MonoBehaviour
{
    //variable
    private int priceObject1 = 100;
    private int priceObject2 = 100;
    private int priceObject3 = 500;
    private int priceObject4 = 100;
    private int priceObject5 = 500;
    private int priceObject6 = 100;
    private int priceObject7 = 500;
    private int priceObject8 = 100;

    private int upgradeObject1 = 0;
    private int upgradeObject2 = 0;
    private int upgradeObject3 = 0;
    private int upgradeObject4 = 0;
    private int upgradeObject5 = 0;
    private int upgradeObject6 = 0;
    private int upgradeObject7 = 0;
    private int upgradeObject8 = 0;

    private int needLevelPerson1 = 1;
    private int needLevelPerson2 = 1;
    private int needLevelPerson3 = 4;
    private int needLevelPerson4 = 1;
    private int needLevelPerson5 = 4;
    private int needLevelPerson6 = 1;
    private int needLevelPerson7 = 4;
    private int needLevelPerson8 = 1;
    

    public byte chooseButton;
    
    public Menu_HUD hud;
    public GameManager gameManager;
    public LanguageMar languageMar;
    public SoundManager soundManager;
    public PlayerMove playerMove;
    public GuildSystem_Save guildSystemSave;
    
    private SaveShop saveShop = new SaveShop();
    
    private void Start()
    {
        hud.panelShop.SetActive(false);
        hud.priceText.text = returnPrice(0);
    }

    public void ShopObjectInfo_1()
    {
        chooseButton = 1;
        ReturnLanguageValue("Улучшение скорости стрельбы", "Upgrade rate of fire", priceObject1);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson1, guildSystemSave.pilotLevel, "Пилот", "Pilot");
        hud.upgradeLevel.fillAmount = upgradeObject1 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_2()
    {
        chooseButton = 2;
        ReturnLanguageValue("Улучшение скорости уменьшения энергии", "Upgrage of energy reduction", priceObject2);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson2, guildSystemSave.pilotLevel, "Пилот", "Pilot");
        hud.upgradeLevel.fillAmount = upgradeObject2 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_3()
    {
        chooseButton = 3;
        ReturnLanguageValue("Замедление времени", "Slow time mode", priceObject3);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson3, guildSystemSave.mechanicLevel, "Механик", "Mechanic");
        hud.upgradeLevel.fillAmount = upgradeObject3 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_4()
    {
        chooseButton = 4;
        ReturnLanguageValue("Улучшение урона игрока", "Upgrade speed movement", priceObject4);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson4, guildSystemSave.pilotLevel, "Пилот", "Pilot");
        hud.upgradeLevel.fillAmount = upgradeObject4 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_5()
    {
        chooseButton = 5;
        ReturnLanguageValue("Дополнительная жизнь", "Upgrade speed movement", priceObject5);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson5, guildSystemSave.doctorLevel, "Доктор", "Doctor");
        hud.upgradeLevel.fillAmount = upgradeObject5 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_6()
    {
        chooseButton = 6;
        ReturnLanguageValue("Улучшение получения опыта", "Upgrade speed movement", priceObject6);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson6, guildSystemSave.engeneerLevel, "Инженер", "Engeneer");
        hud.upgradeLevel.fillAmount = upgradeObject6 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_7()
    {
        chooseButton = 7;
        ReturnLanguageValue("Улучшение получение сундуков", "Upgrade speed movement", priceObject7);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson7, guildSystemSave.engeneerLevel, "Инженер", "Engeneer");
        hud.upgradeLevel.fillAmount = upgradeObject7 / 100f;
        hud.panelShop.SetActive(true);
    }
    
    public void ShopObjectInfo_8()
    {
        chooseButton = 8;
        ReturnLanguageValue("Улучшение выпадения патронов", "Upgrade speed movement", priceObject8);
        guildSystemSave.LoadGuild();
        hud.needLevelText.text = returnNameNeedLevel(needLevelPerson8, guildSystemSave.mechanicLevel, "Механик", "Mechanic");
        hud.upgradeLevel.fillAmount = upgradeObject8 / 100f;
        hud.panelShop.SetActive(true);
    }

    public void ShopBuyButton()
    {
        if (chooseButton == 1 && gameManager.allScore >= priceObject1 && upgradeObject1 < 100 && needLevelPerson1 <= guildSystemSave.pilotLevel)
        {
            hud.playerMove.fireRate -= 0.02f;
            hud.playerMove.maxBullet += 5;
            gameManager.bulletSpeedReload += 0.002f;
            
            priceObject1 += 20;
            upgradeObject1 += 5;
            needLevelPerson1 += 1;
            
            gameManager.allScore -= priceObject1;
            hud.upgradeLevel.fillAmount = upgradeObject1 / 100f;
            hud.priceText.text = returnPrice(priceObject1);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson1, guildSystemSave.pilotLevel, "Пилот", "Pilot");
            
            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 2 && gameManager.allScore >= priceObject2 && upgradeObject2 < 100 && needLevelPerson2 <= guildSystemSave.pilotLevel)
        {
            if (gameManager.fuelSpeed > 0.5f)
            {
                gameManager.fuelSpeed -= 0.3f;
            }
            else if (gameManager.fuelSpeed < 0.4f)
            {
                gameManager.fuelSpeed = 0.4f;
            }
            
            priceObject2 += 50;
            upgradeObject2 += 20;
            needLevelPerson2 += 1;
            
            gameManager.allScore -= priceObject2;
            hud.upgradeLevel.fillAmount = upgradeObject2 / 100f;
            hud.priceText.text = returnPrice(priceObject2);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson2, guildSystemSave.pilotLevel, "Пилот", "Pilot");
            
            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 3 && gameManager.allScore >= priceObject3 && upgradeObject3 < 100 && needLevelPerson3 <= guildSystemSave.mechanicLevel)
        {
            hud.playerMove.dashInt = 2f;
            
            priceObject3 += 500;
            upgradeObject3 += 100;
            needLevelPerson3 += 1;
            
            gameManager.allScore -= priceObject3;
            hud.upgradeLevel.fillAmount = upgradeObject3 / 100f;
            hud.priceText.text = returnPrice(priceObject3);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson3, guildSystemSave.mechanicLevel, "Механик", "Mechanic");
            
            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 4 && gameManager.allScore >= priceObject4 && upgradeObject4 < 100 && needLevelPerson4 <= guildSystemSave.pilotLevel)
        {
            playerMove.playerDamage += 2;
            
            priceObject4 += 150;
            upgradeObject4 += 10;
            needLevelPerson4 += 1;

            gameManager.allScore -= priceObject4;
            hud.upgradeLevel.fillAmount = upgradeObject4 / 100f;
            hud.priceText.text = returnPrice(priceObject4);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson4, guildSystemSave.pilotLevel, "Пилот", "Pilot");
            
            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 5 && gameManager.allScore >= priceObject5 && upgradeObject5 < 100 && needLevelPerson5 <= guildSystemSave.doctorLevel)
        {
            gameManager.playerAddLife = 1;
            
            priceObject5 += 500;
            upgradeObject5 += 100;
            needLevelPerson5 += 1;
            
            gameManager.allScore -= priceObject5;
            hud.upgradeLevel.fillAmount = upgradeObject5 / 100f;
            hud.priceText.text = returnPrice(priceObject5);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson5, guildSystemSave.doctorLevel, "Доктор", "Doctor");
            
            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 6 && gameManager.allScore >= priceObject6 && upgradeObject6 < 100 && needLevelPerson6 <= guildSystemSave.engeneerLevel)
        {
            gameManager.expBonus += 0.2f;
            
            priceObject6 += 150;
            upgradeObject6 += 20;
            needLevelPerson6 += 1;
            
            gameManager.allScore -= priceObject6;
            hud.upgradeLevel.fillAmount = upgradeObject6 / 100f;
            hud.priceText.text = returnPrice(priceObject6);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson6, guildSystemSave.engeneerLevel, "Инженер", "Engeneer");
            
            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 7 && gameManager.allScore >= priceObject7 && upgradeObject7 < 100 && needLevelPerson7 <= guildSystemSave.engeneerLevel)
        {
            gameManager.boxBonus = 1;
            
            priceObject7 += 500;
            upgradeObject7 += 100;
            needLevelPerson7 += 1;
            
            gameManager.allScore -= priceObject7;
            hud.upgradeLevel.fillAmount = upgradeObject7 / 100f;
            hud.priceText.text = returnPrice(priceObject7);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson7, guildSystemSave.engeneerLevel, "Инженер", "Engeneer");

            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        } else if (chooseButton == 8 && gameManager.allScore >= priceObject8 && upgradeObject8 < 100 && needLevelPerson8 <= guildSystemSave.mechanicLevel)
        {
            gameManager.bulletBonus += 5;
            
            priceObject8 += 100;
            upgradeObject8 += 20;
            needLevelPerson8 += 1;
            
            gameManager.allScore -= priceObject4;
            hud.upgradeLevel.fillAmount = upgradeObject8 / 100f;
            hud.priceText.text = returnPrice(priceObject8);
            hud.needLevelText.text = returnNameNeedLevel(needLevelPerson8, guildSystemSave.engeneerLevel, "Механик", "Mechanic");

            guildSystemSave.LoadGuild();
            SaveShop();
            gameManager.SaveFile();
        }
    }

    public void OpenShopPanel()
    {
        LoadShop();
        hud.shopHUD.SetActive(true);
        soundManager.SoundHit(soundManager.buttonSound);
    }
    
    public void CloseShopPanel()
    {
        SaveShop();
        hud.shopHUD.SetActive(false);
        hud.panelShop.SetActive(false);
        soundManager.SoundHit(soundManager.buttonSound);
        gameManager.SaveFile();
    }

    private void ReturnLanguageValue(string ru_Discription, string eng_Discrition, int priceObject)
    {
        if (gameManager.languageInt == 1)
        {
            hud.nameText.text = ru_Discription;
            hud.priceText.text = "Цена: " + priceObject;
        }else if (gameManager.languageInt == 0)
        {
            hud.nameText.text = eng_Discrition;
            hud.priceText.text = "Price: " + priceObject;
        }
    }

    private string returnPrice(int priceObject)
    {
        if (gameManager.languageInt == 1)
        {
            string value = "Цена: " + priceObject;
            return value;
        }else if (gameManager.languageInt == 0)
        {
            string value = "Price: " + priceObject;
            return value;
        }
        else{return "";}
    }
    
    private string returnNameNeedLevel(int needLevel,int levelObject, string namePerson_ru, string namePerson_eng)
    {
        if (gameManager.languageInt == 1)
        {
            string value = "Требуется " + namePerson_ru + " ур. " + levelObject + " / " + needLevel;
            return value;
        }else if (gameManager.languageInt == 0)
        {
            string value = "Required " + namePerson_eng + " lvl. " + levelObject + " / " + needLevel;
            return value;
        }
        else{return "";}
    }
    
    public void LoadShop()
    {
        if (!PlayerPrefs.HasKey("SaveShop")) Debug.Log("Dont Exist Save File (Shop)");
        else
        {
            saveShop = JsonUtility.FromJson<SaveShop>(PlayerPrefs.GetString("SaveShop"));
            
            //Prices
            priceObject1 = saveShop.shopPrice_Object1_Save;
            priceObject2 = saveShop.shopPrice_Object2_Save;
            priceObject3 = saveShop.shopPrice_Object3_Save;
            priceObject4 = saveShop.shopPrice_Object4_Save;
            priceObject5 = saveShop.shopPrice_Object5_Save;
            priceObject6 = saveShop.shopPrice_Object6_Save;
            priceObject7 = saveShop.shopPrice_Object7_Save;
            priceObject8 = saveShop.shopPrice_Object8_Save;
            
            //Upgrades
            upgradeObject1 = saveShop.shopUpgrade_Object1_Save;
            upgradeObject2 = saveShop.shopUpgrade_Object2_Save;
            upgradeObject3 = saveShop.shopUpgrade_Object3_Save;
            upgradeObject4 = saveShop.shopUpgrade_Object4_Save;
            upgradeObject5 = saveShop.shopUpgrade_Object5_Save;
            upgradeObject6 = saveShop.shopUpgrade_Object6_Save;
            upgradeObject7 = saveShop.shopUpgrade_Object7_Save;
            upgradeObject8 = saveShop.shopUpgrade_Object8_Save;
            
            //NeedLevel
            needLevelPerson1 = saveShop.shopNeedLevel_Object1_Save;
            needLevelPerson2 = saveShop.shopNeedLevel_Object2_Save;
            needLevelPerson3 = saveShop.shopNeedLevel_Object3_Save;
            needLevelPerson4 = saveShop.shopNeedLevel_Object4_Save;
            needLevelPerson5 = saveShop.shopNeedLevel_Object5_Save;
            needLevelPerson6 = saveShop.shopNeedLevel_Object6_Save;
            needLevelPerson7 = saveShop.shopNeedLevel_Object7_Save;
            needLevelPerson8 = saveShop.shopNeedLevel_Object8_Save;
        }
    }
    
    public void SaveShop()
    {
        //Prices
        saveShop.shopPrice_Object1_Save = priceObject1;
        saveShop.shopPrice_Object2_Save = priceObject2;
        saveShop.shopPrice_Object3_Save = priceObject3;
        saveShop.shopPrice_Object4_Save = priceObject4;
        saveShop.shopPrice_Object5_Save = priceObject5;
        saveShop.shopPrice_Object6_Save = priceObject6;
        saveShop.shopPrice_Object7_Save = priceObject7;
        saveShop.shopPrice_Object8_Save = priceObject8;
        
        //Upgrades
        saveShop.shopUpgrade_Object1_Save = upgradeObject1;
        saveShop.shopUpgrade_Object2_Save = upgradeObject2;
        saveShop.shopUpgrade_Object3_Save = upgradeObject3;
        saveShop.shopUpgrade_Object4_Save = upgradeObject4;
        saveShop.shopUpgrade_Object5_Save = upgradeObject5;
        saveShop.shopUpgrade_Object6_Save = upgradeObject6;
        saveShop.shopUpgrade_Object7_Save = upgradeObject7;
        saveShop.shopUpgrade_Object8_Save = upgradeObject8;
        
        //NeedLevel
        saveShop.shopNeedLevel_Object1_Save = needLevelPerson1;
        saveShop.shopNeedLevel_Object2_Save = needLevelPerson2;
        saveShop.shopNeedLevel_Object3_Save = needLevelPerson3;
        saveShop.shopNeedLevel_Object4_Save = needLevelPerson4;
        saveShop.shopNeedLevel_Object5_Save = needLevelPerson5;
        saveShop.shopNeedLevel_Object6_Save = needLevelPerson6;
        saveShop.shopNeedLevel_Object7_Save = needLevelPerson7;
        saveShop.shopNeedLevel_Object8_Save = needLevelPerson8;

        //Сохранение
        PlayerPrefs.SetString("SaveShop", JsonUtility.ToJson(saveShop));
    }

    public void DeleteShop()
    {
    priceObject1 = 100;
    priceObject2 = 100;
    priceObject3 = 500;
    priceObject4 = 100;
    priceObject5 = 500;
    priceObject6 = 100;
    priceObject7 = 500;
    priceObject8 = 100;

    upgradeObject1 = 0;
    upgradeObject2 = 0;
    upgradeObject3 = 0;
    upgradeObject4 = 0;
    upgradeObject5 = 0;
    upgradeObject6 = 0;
    upgradeObject7 = 0;
    upgradeObject8 = 0;

    needLevelPerson1 = 1;
    needLevelPerson2 = 1;
    needLevelPerson3 = 4;
    needLevelPerson4 = 1;
    needLevelPerson5 = 4;
    needLevelPerson6 = 1;
    needLevelPerson7 = 4;
    needLevelPerson8 = 1;
    }
}
