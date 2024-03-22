using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyController
{
    [Header("---------------------- Boss ------------------------")]
    public bool bossEndLevel = true;
    [Space]
    [SerializeField] protected Animator animator;
    [SerializeField]protected List<BossSkill> listSkill;
    public string nameBoss = "boss";
    [SerializeField] protected Vector3 delayCallSKill = new Vector2(2, 3);
    public Slider healthSlider;

    protected override void DieEvent()
    {
        base.DieEvent();

        if(bossEndLevel == true)
        {
            LevelManager.instance.EndLevel(true);
        }
    }
    protected override void Start()
    {
        base.Start();
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    protected override void Update()
    {
        base.Update();
        healthSlider.value = health;
    }

}
