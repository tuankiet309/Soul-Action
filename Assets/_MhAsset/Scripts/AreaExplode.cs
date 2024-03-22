using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExplode : BossSkill
{
    public GameObject areaWarning;
    public GameObject areaScale;
    public AreaDmg areaBulletPrefab;

    Transform target;
    [Header("------------ Stats -----------------")]
    public float dmg;
    public float timeWarning = 2f;

    private void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    public override void CallSkill(float delay)
    {
        base.CallSkill(delay);

        StartCoroutine(OnSkill(delay));
    }

    IEnumerator OnSkill(float delay)
    {
        yield return new WaitForSeconds(delay);

        areaScale.transform.localScale = Vector3.zero;
        areaWarning.transform.position = target.position;
        areaWarning.transform.SetParent(null);
        areaWarning.SetActive(true);
        areaScale.transform.DOScale(Vector3.one, timeWarning);

        yield return new WaitForSeconds(timeWarning);
        areaWarning.SetActive(false);


        AreaDmg newArea = Instantiate(areaBulletPrefab, areaWarning.transform.position, Quaternion.identity);
        newArea.Init(dmg, true, areaWarning.transform.localScale.x);
        Destroy(newArea.gameObject, 0.5f);

    }

    private void OnDisable()
    {
        if(areaWarning) areaWarning.SetActive(false);
    }
}
