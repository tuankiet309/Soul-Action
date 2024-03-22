using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI textLevel;
    public Button btnChoseLevel;

    public void Init(int indexLevel, UnityAction action)
    {
        textLevel.text = "Map " + ( indexLevel + 1 );
        btnChoseLevel.onClick.AddListener( action);
    }

    
}
