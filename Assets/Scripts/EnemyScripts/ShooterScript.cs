using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MovingEnemy
{
    public GameObject Shell;
    private bool stopAim;
    private float Ydeviation;

    private void Update()
    {
        if (Dammage)
        {
            transform.position = new Vector2(Random.Range(AimPosition.x - 0.2f, AimPosition.x + 0.2f), Random.Range(AimPosition.y - 0.2f, AimPosition.y + 0.2f));
            transform.position = Vector2.MoveTowards(transform.position, AimPosition, Time.deltaTime * 10);
        }
        else
        {
            if (Mathf.Abs(AimPosition.x - gameObject.transform.position.x) < 0.1f)
            {
                if (!stopAim)
                {
                    Shoot();
                    stopAim = true;
                    StartCoroutine(ChangeAim());
                }
            }
            AimPosition = new Vector2(AimPosition.x, MainCamera.position.y + 4f + Ydeviation);
            transform.position = Vector2.MoveTowards(transform.position, AimPosition, Time.deltaTime * Speed * SpeedMultiplier);
        }
    }

    private void Shoot()
    {
        cameraScript.Shaking(0.05f);
        Audio.AudioPlay(5);
        Instantiate(Shell, transform.position, Quaternion.identity);
    }

    private IEnumerator ChangeAim()
    {
        yield return new WaitForSeconds(Random.Range(1f,3f));
        stopAim = false;
        Ydeviation = Random.Range(-1f, 1f);
        AimPosition = new Vector2(Random.Range(-3f, 3f), MainCamera.position.y + 4f);
    }


}
