using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerScript : MovingEnemy
{
    public float AttackSpeed;

    private void Update()
    {
        if (Dammage)
        {
            transform.position = new Vector2(Random.Range(AimPosition.x - 0.2f, AimPosition.x + 0.2f), Random.Range(AimPosition.y - 0.2f, AimPosition.y + 0.2f));
            transform.position = Vector2.MoveTowards(transform.position, AimPosition, Time.deltaTime * 10);
        }
        else if (Attack)
        {
            AttackMove();
        }
        else if (ReadyAttack)
        {
            AimPosition = new Vector2(Player.position.x, MainCamera.position.y + 3);
            transform.position = Vector2.MoveTowards(transform.position, AimPosition, Time.deltaTime * Speed * SpeedMultiplier);
            if (Mathf.Abs(transform.position.x - Player.position.x) <= 0.2f)
            {
                AimPosition = new Vector2(AimPosition.x, Player.position.y - 3f);
                Attack = true;
                ReadyAttack = false;
            }
        }
        else
        {
            AimPosition = new Vector2(Player.position.x, MainCamera.position.y + 3);
            transform.position = Vector2.MoveTowards(transform.position, AimPosition, Time.deltaTime * Speed * SpeedMultiplier);
            if (Vector2.Distance(AimPosition, transform.position) <= 0.1f)
            {
                StartCoroutine(AttackPause());
            }
        }
    }

    public void AttackMove()
    {
        if (Vector2.Distance(AimPosition, transform.position) <= 0.1f)
        {
            Attack = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, AimPosition, Time.deltaTime * AttackSpeed * SpeedMultiplier);
        }
    }
}
