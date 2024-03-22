using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;


public class CthuluController : EnemyController
{
    private float healthTemp;
    private float moveSpeedTemp;
    private Animator anim;
    public float distanceCheck;
    public Slider healthSlider;

    protected override void Start()
    {
        base.Start();
        healthTemp = health;
        moveSpeedTemp = moveSpeed;
        anim = GetComponentInChildren<Animator>();
        healthSlider.maxValue = healthTemp;
        healthSlider.value = health;
    }

    protected override void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf == true)
        {
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }
                if (knockBackCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * .5f);
                }
            }
            RushAttack();
            if (hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
        Flip();
        healthSlider.value = health;
    }
    public override void TakeDamage(float damageToTake)
    {
        base.TakeDamage(damageToTake);
    }

    public override void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        base.TakeDamage(damageToTake, shouldKnockback);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    
    private void RushAttack()
    {
        if(health<healthTemp/2)
        {
            anim.SetBool("isFly", true);
            moveSpeed = (float)(moveSpeedTemp * 1.5);
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed ;
            if (Vector3.Distance(target.position,transform.position) > distanceCheck)
            {
                knockBackTime = 0;
            }
            else 
            {
                knockBackTime = 0.05f;
            }

        }
        else 
        {
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
    }
    

   
}
