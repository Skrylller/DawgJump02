using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public string mode;


    public void Delete()
    {
        Destroy(gameObject);
    }

    public void ChangeMode(int value)
    {
        if(mode == null)
        {
            mode = "mode";
        }

        gameObject.GetComponent<Animator>().SetInteger(mode, value);
    }
}
