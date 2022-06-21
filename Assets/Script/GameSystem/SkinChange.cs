using UnityEngine;
using UnityEngine.UI;

public class SkinChange : MonoBehaviour
{
    public Sprite[] bulletSprite;
    public Button buttonText;

    public Text[] readyButtonText;
    public Text[] priceADText;

    public Sprite[] shipSprite;
    public Image shopShipImage;
    
    public GameManager gameManager;
    public PlayerMove playerMove;

    private void OnEnable()
    {
        for (int i = 0; i < readyButtonText.Length; i++)
        {
            if (gameManager.languageInt == 0)
            {
                readyButtonText[i].text = "Ready";
                priceADText[i].text = "Price: AD watch";
            }
            else if (gameManager.languageInt == 1)
            {
                readyButtonText[i].text = "Готово";
                priceADText[i].text = "Цена: за просмотр рекламы";
            }
        }
    }

    public void skinChange_1(Button button)
    {
        gameManager.ShowAd(3);
        if (buttonText != null)
        {
            if (gameManager.languageInt == 0){buttonText.GetComponentInChildren<Text>().text = "Ready";}
            else if (gameManager.languageInt == 1)
            {buttonText.GetComponentInChildren<Text>().text = "Готово";}
            
            buttonText.GetComponent<Image>().color = Color.white;
        }
        
        gameManager.playerAnimator.runtimeAnimatorController = gameManager.playerAnimations[0];
        if (playerMove.bulletPrefab != null)
        {
            playerMove.bulletPrefab.GetComponent<SpriteRenderer>().sprite = bulletSprite[0];
        }
        gameManager.skinNumber = 0;
        shopShipImage.sprite = shipSprite[0];
        buttonText = button;
        
        if (gameManager.languageInt == 0){buttonText.GetComponentInChildren<Text>().text = "Complete";}
        else if (gameManager.languageInt == 1)
        {buttonText.GetComponentInChildren<Text>().text = "Закончено";}
        
        buttonText.GetComponent<Image>().color = Color.green;
        gameManager.soundManager.SoundHit(gameManager.soundManager.buttonSound);
        gameManager.SaveFile();
    }
    
    public void skinChange_2(Button button)
    {
        gameManager.ShowAd(3);
        if (buttonText != null)
        {
            if (gameManager.languageInt == 0){buttonText.GetComponentInChildren<Text>().text = "Ready";}
            else if (gameManager.languageInt == 1)
            {buttonText.GetComponentInChildren<Text>().text = "Готово";}
            buttonText.GetComponent<Image>().color = Color.white;
        }
        
        gameManager.playerAnimator.runtimeAnimatorController = gameManager.playerAnimations[1];
        if (playerMove.bulletPrefab != null)
        {
            playerMove.bulletPrefab.GetComponent<SpriteRenderer>().sprite = bulletSprite[1];
        }
        gameManager.skinNumber = 1;
        shopShipImage.sprite = shipSprite[1];
        buttonText = button;
        if (gameManager.languageInt == 0){buttonText.GetComponentInChildren<Text>().text = "Complete";}
        else if (gameManager.languageInt == 1)
        {buttonText.GetComponentInChildren<Text>().text = "Закончено";}
        buttonText.GetComponent<Image>().color = Color.green;
        
        gameManager.soundManager.SoundHit(gameManager.soundManager.buttonSound);
    }
    
    public void skinChange_3(Button button)
    {
        gameManager.ShowAd(3);
        if (buttonText != null)
        {
            if (gameManager.languageInt == 0){buttonText.GetComponentInChildren<Text>().text = "Ready";}
            else if (gameManager.languageInt == 1)
            {buttonText.GetComponentInChildren<Text>().text = "Готово";}
            buttonText.GetComponent<Image>().color = Color.white;
        }
        
        gameManager.playerAnimator.runtimeAnimatorController = gameManager.playerAnimations[2];
        if (playerMove.bulletPrefab != null)
        {
            playerMove.bulletPrefab.GetComponent<SpriteRenderer>().sprite = bulletSprite[0];
        }
        gameManager.skinNumber = 2;
        shopShipImage.sprite = shipSprite[2];
        buttonText = button;
        if (gameManager.languageInt == 0){buttonText.GetComponentInChildren<Text>().text = "Complete";}
        else if (gameManager.languageInt == 1)
        {buttonText.GetComponentInChildren<Text>().text = "Закончено";}
        buttonText.GetComponent<Image>().color = Color.green;
        
        gameManager.soundManager.SoundHit(gameManager.soundManager.buttonSound);
    }

    public void restartSkin()
    {
        gameManager.skinNumber = 0;
        playerMove.bulletPrefab.GetComponent<SpriteRenderer>().sprite = bulletSprite[0];
    }
}
