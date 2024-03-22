using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    //Component
    public Rigidbody2D theRB;
    public float moveSpeed;
    protected Transform target;
    //Stat
    public float damage;
    public float hitWaitTime = 1f;
    protected float hitCounter;
    public float health = 5f;
    public float knockBackTime = .5f;
    protected float knockBackCounter;
    public int expToGive = 1;
    public int coinValue = 1;
    public float healValue = 20f;
    public float coinDropRate = .5f;
    public float heartDropRate = 0.05f;
    //Flip
    public float flipX = 1f;
    public GameObject sprite;
    //FX
    private Hit_FX hitFX;
    //Death Effect
    public GameObject deathEffect;
    // Start is called before the first frame update

    protected bool canMove = true;
    protected virtual void Start()
    {
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        hitFX = GetComponent<Hit_FX>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf == true &&
            canMove)
        {
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if (knockBackCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * .5f);
                }
            }

            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

            if (hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        } else
        {
            theRB.velocity = Vector2.zero;
        }
        Flip();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    public virtual void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        hitFX.StartCoroutine("FlashFX");
        if(health <= 0)
        {
            Destroy(gameObject);
            DieEvent();

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            Debug.Log(Random.value);
            if(Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            if(Random.value <=heartDropRate)
            {
                HeartController.instance.DropHeart(transform.position, healValue);
            }
            UIController.instance.UpdateKills();
            SFXManager.instance.PlaySFXPitched(0);
        } else
        {
            SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public virtual void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if(shouldKnockback == true)
        {
            knockBackCounter = knockBackTime;
        }
    }
    protected  void Flip()
    {
        if(target.position.x <transform.position.x)
        {
            sprite.transform.localScale = new Vector3(-flipX, 1, 1);
        }
        else
        { 
            sprite.transform.localScale = new Vector3(flipX, 1, 1); 
        }
    }

    protected virtual void DieEvent()
    {

    }
}
