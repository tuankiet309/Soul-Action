using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBullet : DmgObj
{

    [SerializeField] Rigidbody2D rb;

    public virtual void Init(float dmg, float speed, Vector2 dir)
    {
        if(rb == null) rb = GetComponent<Rigidbody2D>();    

        this.dmg = dmg;
        typeIsAttack = TypeIsAttacked.Player;

        
        rb.velocity = dir.normalized * speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        Debug.Log("Mh: Init Bullet !!! " + this.name);

        Destroy(gameObject, 5f);
    }


}
