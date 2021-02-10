using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutDialogueScript : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string[] texts = new string[1];
    }
    public Dialogue[] dialogues = new Dialogue[1];

    public Text text;
    public GameObject DialogueWindow;
    private RectTransform winTransform;
    public AudioMaster Audio;
    private int rand;
    private int counter;
    private int a;

    private void Awake()
    {
        winTransform = DialogueWindow.GetComponent<RectTransform>();
    }

    private void Start()
    {
        a = 0;
        counter = 0;
        rand = Random.Range(0, dialogues.Length);
        DialogueWindow.SetActive(false);
    }

    public void GetDialogue()
    {
        Audio.AudioPlay(4, 0f);
        DialogueWindow.SetActive(true);
        text.text = dialogues[rand].texts[counter];
        winTransform.sizeDelta = new Vector2(winTransform.sizeDelta.x, text.text.Length / 20 * 50 + 90);
        if(dialogues[rand].texts.Length - 1 > counter)
        {
            counter++;
        }
        else
        {
            counter = 0;
            rand = Random.Range(0, dialogues.Length);
        }
        StartCoroutine(WindowDeActive());
    }

    IEnumerator WindowDeActive()
    {
        a++;
        yield return new WaitForSeconds(10f);
        a--;
        if(a <= 0)
        {
            DialogueWindow.SetActive(false);
        }
    }
}