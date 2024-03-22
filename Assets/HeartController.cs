using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public static HeartController instance;
    private void Awake()
    {
        instance = this;
    }

    public HeartPickup heart;
    public void AddHeart()
    {
        SFXManager.instance.PlaySFXPitched(2);
    }
    public void DropHeart(Vector3 position, float value)
    {
        HeartPickup newHeart = Instantiate(heart, position + new Vector3(-.2f, -.1f, 0f), Quaternion.identity);
        newHeart.healAmount = value;
        newHeart.gameObject.SetActive(true);
    }
}


