using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownAttackTrainingScript : MonoBehaviour
{
    private Dropdown dropdown;
    public GameObject[] AttacksUI = new GameObject[1];

    public GameObject[] AttacksTextUI = new GameObject[1];

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.value = PlayerPrefs.GetInt("AttackManagmentUI", 0);
        AttacksUI[dropdown.value].SetActive(true);
        AttacksTextUI[dropdown.value].SetActive(true);
    }

    public void ChoiseAttack()
    {
        for (int i = 0; i < AttacksUI.Length; i++)
        {
            AttacksUI[i].SetActive(false);
            AttacksTextUI[i].SetActive(false);
        }
        PlayerPrefs.SetInt("AttackManagmentUI", dropdown.value);
        AttacksUI[dropdown.value].SetActive(true);
        AttacksTextUI[dropdown.value].SetActive(true);
    }

}
