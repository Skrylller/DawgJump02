using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagmentUIScript : MonoBehaviour
{
    public GameObject[] CharacterManagmentUI = new GameObject[1];
    public GameObject[] AttackManagmentUI = new GameObject[1];
    public Image[] UI = new Image[1];

    private void Awake()
    {
        CharacterManagmentUI[PlayerPrefs.GetInt("CharacterManagmentUI", 0)].SetActive(true);
        AttackManagmentUI[PlayerPrefs.GetInt("AttackManagmentUI", 0)].SetActive(true);

        for (int i = 0; i < UI.Length; i++)
        {
            UI[i].color = new Vector4(1, 1, 1, PlayerPrefs.GetFloat("TransparencyInterface", 0.5f));
        }
    }

}
