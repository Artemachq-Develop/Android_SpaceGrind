using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LanguageMar : MonoBehaviour {

    private static LanguageMar instance;

    public static LanguageMar Instance {
        get { return instance; }
    }

    [SerializeField]

    Dictionary<string, string> dict = new Dictionary<string, string>();

    public void LoadLanguage(SystemLanguage loadLanguage)
    {
        TextAsset ta = Resources.Load<TextAsset>(loadLanguage.ToString());
        
        if (ta == null) { Debug.LogWarning("Language is null"); return; }

         string[] lines = ta.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }
            else
            {

                string[] kv = lines[i].Split('|');                

                dict.Add(kv[0], kv[1]);

                /*Debug.Log("Key:" + kv[0] + "Value:" + kv[1]);*/
            }
        }
    }
    public string GetTest(string key)
    {
        if (dict.ContainsKey(key))
        {
            return dict[key];
        }
        else {
            
            return null;
        }
    }

    void Awake ()
    {
        instance = this;
    }
}
