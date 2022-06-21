using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSystem : MonoBehaviour
{
    private bool isChanged;
    private void Update()
    {
        if (!isChanged)
        {
            StartCoroutine(loadScene(0.2f));
            isChanged = true;
        }
    }
    
    private IEnumerator loadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (PlayerPrefs.HasKey("SaveQuest"))
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        StopCoroutine(loadScene(waitTime));
    }
}
