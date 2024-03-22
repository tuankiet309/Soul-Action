using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public LevelUI levelUiPrefab;
    public Transform content;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) Init(); 
    }

    void Init()
    {
        for (int i=0; i<= DataManager.Ins.GetMaxLevelOpened() ; i++)
        {
            CreateLevelUI(i);
        }
    }


    public void ChoseLevel(int indexlevel)
    {
        DataManager.Ins.ChoseLevel(indexlevel);
        SceneManager.LoadScene(1);
    }


    public void LoadLevelEndless()
    {
        SceneManager.LoadScene("Endless Level");
    }
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        ChoseLevel(DataManager.Ins.GetLevelChosed());  
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void NextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ChoseLevel(DataManager.Ins.GetLevelChosed() + 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void CreateLevelUI(int index)
    {
        LevelUI newLevelUI = Instantiate(levelUiPrefab);
        newLevelUI.transform.SetParent(content);
        newLevelUI.Init(index, () => ChoseLevel(index));
    }
}
