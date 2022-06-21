using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AD_Test : MonoBehaviour
{
    private string gameId = "4043903", type = "video";
    public bool testMode = true;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void showAd()
    {
        if (Advertisement.IsReady(type))
        {
            Advertisement.Show(type);
            Debug.Log("isReady");
        }
    }
}
