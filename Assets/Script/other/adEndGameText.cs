using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adEndGameText : MonoBehaviour
{
    public GameSystem_HUD gameSystemHud;
    public GameManager gameManager;
    
    private void OnEnable()
    {
            if (gameManager.languageInt == 0)
            {
                gameSystemHud.adRemainedText.text = "(on hand " + gameManager.goldCount + " cript)";
                if (gameSystemHud.adViewed == false)
                {
                    gameSystemHud.adLookText.text = "(AD was not viewed)";
                }
                else
                {
                    gameSystemHud.adLookText.text = "(AD was viewed)";
                }
                
            }
            else if (gameManager.languageInt == 1)
            {
                gameSystemHud.adRemainedText.text = "(в наличии " + gameManager.goldCount + " крипты)";
                if (gameSystemHud.adViewed == false)
                {
                    gameSystemHud.adLookText.text = "(реклама не просмотрена)";
                }
                else
                {
                    gameSystemHud.adLookText.text = "(реклама просмотрена)";
                }
            }
    }
}
