using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
