using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class DmgObj : MonoBehaviour
{
    [SerializeField]protected float dmg;
    public UnityEvent eventCauseDmg;

    public enum TypeIsAttacked
    {
        None,
        Player,
        Enemy
    }

    [SerializeField]protected TypeIsAttacked typeIsAttack;


    protected virtual void Start()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        CheckToCauseDmg(collision.gameObject);
    }

    protected virtual void CheckToCauseDmg(GameObject obj)
    {
        if ( obj.GetComponent<PlayerHealthController>() && // dmg for player
             typeIsAttack == TypeIsAttacked.Player)
        {
            PlayerHealthController.instance.TakeDamage(dmg);
            EventCauseDmg();
        }

        if ( obj.TryGetComponent<EnemyController>( out EnemyController enemy) &&
             typeIsAttack == TypeIsAttacked.Enemy)
        {
            enemy.TakeDamage(dmg);
            EventCauseDmg();
        }
    }

   
    protected virtual void EventCauseDmg()
    {
        eventCauseDmg?.Invoke();
        Die();
    }

    protected virtual void Die(float delatDie = 0f)
    {
        Destroy(gameObject);    
    }
  
}
