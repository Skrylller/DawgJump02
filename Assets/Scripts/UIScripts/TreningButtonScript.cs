using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreningButtonScript : MonoBehaviour
{
    public GameObject UI;

    public GameObject Text;

    private void Start()
    {
        StartCoroutine(Tap());
    }

    private void OnMouseUp()
    {
        UI.SetActive(true);
        Text.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator Tap()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("ddd");
        Text.SetActive(true);
    }


}
