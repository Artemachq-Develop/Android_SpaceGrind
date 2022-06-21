using UnityEngine;
using UnityEngine.UI;

public class DropBoxObject : MonoBehaviour
{
    public enum TypeDrop {Wood, Rock, Money, Coin};

    public Text nameText;
    public Image typeImage;

    public Sprite[] typeSprite;

    public int countDrop;

    public Color[] rareColor;

    public GameManager gameManager;

    private Image rareImage;

    private Animator animator;

    private void Start()
    {
        rareImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void AttackSpawn()
    {
        TypeDrop cardSuit = (TypeDrop)Random.Range(0, 4);
        spawnDrop(cardSuit);
    }

    public TypeDrop spawnDrop(TypeDrop drop)
    {
        if (drop == TypeDrop.Wood)
        {
            countDrop = Random.Range(5, 15);
            GameManager.Instance.woodCount += countDrop;
            GameManager.Instance.rockCount += 1;
            if (gameManager.languageInt == 0)
            {
                nameText.text = "Bloodstone " + " x" + countDrop;
            } else if (gameManager.languageInt == 1)
            {
                nameText.text = "Бладстоун " + " x" + countDrop;
            }
            typeImage.sprite = typeSprite[0];
            rareImage.color = rareColor[1];
        }else if (drop == TypeDrop.Rock)
        {
            countDrop = Random.Range(5, 20);
            GameManager.Instance.rockCount += countDrop;
            GameManager.Instance.woodCount += 1;
            if (gameManager.languageInt == 0)
            {
                nameText.text = "Scrap metal " + " x" + countDrop;
            } else if (gameManager.languageInt == 1)
            {
                nameText.text = "Металлолом " + " x" + countDrop;
            }
            typeImage.sprite = typeSprite[1];
            rareImage.color = rareColor[1];
        }else if (drop == TypeDrop.Money)
        {
            countDrop = Random.Range(50, 150);
            GameManager.Instance.allScore += countDrop;
            GameManager.Instance.rockCount += 1;
            GameManager.Instance.woodCount += 1;
            if (gameManager.languageInt == 0)
            {
                nameText.text = "Сurrency " + " x" + countDrop;
            } else if (gameManager.languageInt == 1)
            {
                nameText.text = "Валюта " + " x" + countDrop;
            }
            typeImage.sprite = typeSprite[2];
            rareImage.color = rareColor[0];
        }else if (drop == TypeDrop.Coin)
        {
            countDrop = Random.Range(1, 3);
            GameManager.Instance.goldCount += countDrop;
            GameManager.Instance.rockCount += 1;
            GameManager.Instance.woodCount += 1;
            if (gameManager.languageInt == 0)
            {
                nameText.text = "Crypts " + " x" + countDrop;
            } else if (gameManager.languageInt == 1)
            {
                nameText.text = "Крипта " + " x" + countDrop;
            }
            typeImage.sprite = typeSprite[3];
            rareImage.color = rareColor[2];
        }
        
        return drop;
    }

    public void ShowNotification(Sprite sprite, string string_ru, string string_eng, int number)
    {
        animator.SetBool("isActive", true);
        
        if (gameManager.languageInt == 0)
        {
            nameText.text = string_eng + " x" + number;
        } else if (gameManager.languageInt == 1)
        {
            nameText.text = string_ru + " x" + number;
        }

        typeImage.sprite = sprite;
    }
}
