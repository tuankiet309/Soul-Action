using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBullet : BossSkill
{
    [Header("----------------------- Component ----------------")]
    public Transform posFire;
    public BaseEnemyBullet bulletPrefab;

    [Header("------------------------- Stats -----------------------------")]
    public int countWave;

    public float delayWaveShoot = 0.5f;
    public int countBulletPerWave = 5;
    public float angle = 20f;

    [Space]
    public float dmg = 1;
    public float speedBullet = 5;

    Transform target;

    private void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    public override void CallSkill(float delay)
    {
        StartCoroutine(SpawnListWave(delay));
    }


    IEnumerator SpawnListWave(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i=0; i < countWave; i++)
        {
            SpawnAWave();
            yield return new WaitForSeconds(delayWaveShoot);
        }
    }

    void SpawnAWave()
    {
        Vector2 dirStart = target.transform.position - posFire.position;    

        for (int i=0; i < countBulletPerWave; i++)
        {
            Vector2 dir = Quaternion.Euler(0, 0, angle/2 - i * ( angle / (countBulletPerWave -1) ) ) * dirStart;
            ShootABullet(dir);
        }
    }

    void ShootABullet(Vector2 dir)
    {
        BaseEnemyBullet newBullet = Instantiate(bulletPrefab, posFire.position, Quaternion.identity);
        newBullet.Init(dmg, speedBullet, dir);
    }
}
