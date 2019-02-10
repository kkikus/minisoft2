using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownCaption : MonoBehaviour {

	void Start () {
        if (PlayerPrefs.GetInt("numberTask") - 1 > 0)
        {
            this.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("numberTask") - 1;
        }
	}
	
    public void changeValue()
    {
        PlayerPrefs.SetInt("numberTask", this.GetComponent<Dropdown>().value + 1);
    }
}
