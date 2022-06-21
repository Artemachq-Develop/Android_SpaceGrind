using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWord : MonoBehaviour {
	void Start () 
    {
       this.gameObject.GetComponent<Text>().text = LanguageMar.Instance.GetTest(this.gameObject.name);
    }
}
