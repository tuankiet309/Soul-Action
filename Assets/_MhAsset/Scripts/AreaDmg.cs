using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDmg : DmgObj
{
    public void Init(float dmg, bool dmgForPlayer = false, float size = 1)
    {
        this.dmg = dmg;

        transform.localScale = Vector3.one * size;

        if (dmgForPlayer)
        {
            typeIsAttack = TypeIsAttacked.Player;
        }
        else
        {
            typeIsAttack = TypeIsAttacked.Enemy;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void EventCauseDmg()
    {
        eventCauseDmg?.Invoke();
    }

}

