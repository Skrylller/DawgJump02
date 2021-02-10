using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{

    public string NameSettings;
    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.value = PlayerPrefs.GetInt(NameSettings, 0);
    }

    public void ChoiseManagment()
    {
        PlayerPrefs.SetInt(NameSettings, dropdown.value);
    }
}
