using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;

    public float lifetime;
    private float lifeCounter;

    public float floatSpeed = 1f;

    

    // Update is called once per frame
    void Update()
    {
        
        
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if(lifeCounter <= 0)
            {
                //Destroy(gameObject);

                DamageNumberController.instance.PlaceInPool(this);
            }
        }

        /* if(Input.GetKeyDown(KeyCode.U))
        {
            Setup(45);
        } */

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;

        damageText.text = damageDisplay.ToString();
    }
}
