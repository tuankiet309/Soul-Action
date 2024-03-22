using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyController
{
    [Header("--------------- Range Enemy --------------------")]
    [SerializeField]protected BaseEnemyBullet bulletPrefabs;
    [SerializeField]protected Transform posFire;

    [SerializeField]protected float delayWaveShoot;
    [SerializeField] protected float distanceShoot;

    [SerializeField]protected float speedBullet;
    [SerializeField]protected float dmg;

}
