using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Ins;

    private const string nameMaxLevelOpened = "Max_Level_Opened";

    [Header("------------------------- Status ----------------------------")]
    [SerializeField] private int levelChosed = 0;
    public bool open = false;
    public bool close = false;


    private void Awake()
    {
        if (Ins != null && Ins != this)
        {
            Destroy(this);
        }
        else
        {
            Ins = this;
        }
        DontDestroyOnLoad(this.gameObject);

        Init();
    }

    void Init()
    {
        if (open == true)
            OpenAllMap();
        if(open == false)
            ResetAllMap();
    }


    public void ChoseLevel(int level)
    {
        levelChosed = level;
    }

    public int GetLevelChosed()
    {
        return levelChosed;    
    }

    public int GetMaxLevelOpened()
    {
        return PlayerPrefs.GetInt(nameMaxLevelOpened);
    }

    public void OpenNextLevel()
    {
        if(GetMaxLevelOpened() == GetLevelChosed()) PlayerPrefs.SetInt(nameMaxLevelOpened, GetMaxLevelOpened() + 1);
    }

    
    void OpenAllMap()
    {
        PlayerPrefs.SetInt(nameMaxLevelOpened,5 );
    }
    void ResetAllMap()
    {
        PlayerPrefs.SetInt(nameMaxLevelOpened, 0);
    }
}

