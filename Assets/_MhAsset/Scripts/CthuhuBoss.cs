using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuhuBoss : Boss
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(CallSkill());
    }


    IEnumerator CallSkill()
    {
        yield return new WaitForSeconds(Random.Range(delayCallSKill.x, delayCallSKill.y));

        if (target == null || !PlayerHealthController.instance.gameObject.activeInHierarchy)
        {
            yield break;
        }

        int indexSkill = Random.Range(0, listSkill.Count);

        //StartCoroutine(Stop(0.4f));
        //animator.SetTrigger("Attack");
        //yield return new WaitForSeconds(0.23f);
        //listSkill[indexSkill].CallSkill();

        switch(indexSkill)
        {
            case 0:
                CallSkill_ShootBullet();
                break;
            case 1:
                CallSkill_AreaExplode();
                break;
        }

        StartCoroutine(CallSkill());

    }

    IEnumerator Stop(float timeStop)
    {
        canMove = false;
        yield return new WaitForSeconds(timeStop);
        canMove = true;
    }

    void CallSkill_ShootBullet()
    {
        StartCoroutine(Stop(0.35f));
        animator.SetTrigger("Attack");
        listSkill[0].CallSkill(0.08f);
    }

    void CallSkill_AreaExplode()
    {
        StartCoroutine(Stop(0.35f));
        animator.SetTrigger("Attack");
        listSkill[1].CallSkill(0.08f);
    }


}
