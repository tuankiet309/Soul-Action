using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<EnemySpawner> listEnemySpawer = new();
    public List<GameObject> listMap;
    public Transform gridParent;
         
    private void Awake()
    {
        instance = this;

        Init();
    }

    private bool gameActive;
    public float timer;
    public float waitToShowEndScreen = 1f;
    [Space]
    public int indexLevelCr;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    void Init()
    {
        foreach (EnemySpawner spawnCtrl in listEnemySpawer)
        {
            spawnCtrl.gameObject.SetActive(false);
        }

        

        if(DataManager.Ins) indexLevelCr = DataManager.Ins.GetLevelChosed();

        if(indexLevelCr < listEnemySpawer.Count)
        {
            listEnemySpawer[indexLevelCr].gameObject.SetActive(true);
            Instantiate(listMap[indexLevelCr], Vector3.zero, Quaternion.identity,gridParent);
        }
        else listEnemySpawer[listEnemySpawer.Count].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
        if(timer>210&& DataManager.Ins.GetLevelChosed()==0)
        {

            EndLevel(true);
        }
        if (timer > 200 && DataManager.Ins.GetLevelChosed() == 1)
        {

            EndLevel(true);

        }
    }

    public void EndLevel(bool isWin = false)
    {
        gameActive = false;

        if (!isWin) StartCoroutine(EndLevelCo());
        else StartCoroutine(WinLevel());
    }
    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);
        UIController.instance.levelEndScreen.SetActive(true);
        UIController.instance.looseTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        UIController.instance.looseKillText.text = UIController.instance.killCount + " monsters";
        
    }

    IEnumerator WinLevel()
    {
        DataManager.Ins.OpenNextLevel();
        PlayerHealthController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.levelWinScreen.SetActive(true);
        UIController.instance.winTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        UIController.instance.winKillText.text = UIController.instance.killCount + " monsters";
        
    }


}
