using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : RangeEnemy
{
    //[Header("------------------ Fish Enemy --------------------------")]

    protected override void Start()
    {
        base.Start();

        StartCoroutine(CheckToShoot());
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator CheckToShoot()
    {
        yield return new WaitForSeconds(delayWaveShoot);

        if( target == null || PlayerController.instance.gameObject.activeSelf == false)
        {
            yield break;    
        }

        if (Vector2.Distance( this.transform.position, target.position) <= distanceShoot )
        {
            Shoot();
        }

        StartCoroutine(CheckToShoot());
    }


    void Shoot()
    {
        Vector2 dir = target.position - this.transform.position;
        BaseEnemyBullet newBullet = Instantiate(bulletPrefabs, posFire.position, Quaternion.identity);
        newBullet.Init(dmg, speedBullet, dir);
    }
    
}
