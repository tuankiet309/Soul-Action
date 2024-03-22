using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Death_Effect : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroySelf());
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

    }
}
