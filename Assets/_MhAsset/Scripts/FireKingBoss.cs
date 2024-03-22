using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKingBoss : Boss
{    

    protected override void Start()
    {
        base.Start();

        StartCoroutine(CallSkill());
    }
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator CallSkill()
    {
        yield return new WaitForSeconds(Random.Range(delayCallSKill.x, delayCallSKill.y));

        if (target == null || !PlayerHealthController.instance.gameObject.activeInHierarchy)
        {
            yield break;
        }

        int indexSkill = Random.Range(0, listSkill.Count);

        StartCoroutine(Stop(0.4f));
        animator.SetTrigger("Attack");
        
        listSkill[indexSkill].CallSkill(0.23f);

       

        StartCoroutine(CallSkill());

    }

    IEnumerator Stop(float timeStop)
    {
        canMove = false;
        yield return new WaitForSeconds(timeStop);
        canMove = true;
    }
}
