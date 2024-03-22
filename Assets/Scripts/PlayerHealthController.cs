using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    //Singleton
    private void Awake()
    {
        instance = this;
    }
    //UI
    public float currentHealth, maxHealth;
    public Slider healthSlider;
    public GameObject deathEffect;
    //FX
    private Hit_FX hitFX;
    // Start is called before the first frame update
    void Start()
    {
        hitFX = GetComponent<Hit_FX>();
        maxHealth = PlayerStatController.instance.health[0].value;

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        } */
        Mathf.Clamp(currentHealth, float.MinValue, maxHealth);

    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        hitFX.StartCoroutine("FlashFX");
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);

            LevelManager.instance.EndLevel(false);

            Instantiate(deathEffect, transform.position, transform.rotation);

            SFXManager.instance.PlaySFX(3);
        }
        healthSlider.value = currentHealth;
    }
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        Mathf.Clamp(currentHealth, float.MinValue, maxHealth);
    }
}
