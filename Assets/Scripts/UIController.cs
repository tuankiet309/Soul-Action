using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }
    

    public Slider explvlSlider;
    public TMP_Text expLvlText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelUpPanel;

    public TMP_Text coinText;

    public TMP_Text killText;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    public TMP_Text timeText;
    public TMP_Text levelText;

    public TMP_Text anounceText;

    public GameObject levelEndScreen;
    public TMP_Text looseTimeText;
    public TMP_Text looseKillText;

    public GameObject levelWinScreen;
    public TMP_Text winTimeText;
    public TMP_Text winKillText;

    public string mainMenuName;

    public GameObject pauseScreen;

    [HideInInspector] public int killCount = 0;

    // Weapon Information
    [Header("Weapon")]
    public TMP_Text swordLevel;
    public CloseAttackWeapon closeAttackWeapon;
    public TMP_Text fireballLevel;
    public SpinWeapon spinWeapon;
    public TMP_Text axeLevel;
    public WeaponThrower thrownWeapon;
    public TMP_Text circleLevel;
    public ZoneWeapon zoneWeapon;
    public TMP_Text kunaiLevel;
    public ProjectileWeapon projectileWeapon;
    // Weapon GetInforamtio

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevel();
        StartCoroutine(Anounce());
    }          
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        expLvlText.text = "Level: " + currentLvl;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
    public void UpdateKills()
    {
        killCount++;
        killText.text = "Kill: " + killCount;
    }    

    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt( time / 60f);
        float seconds = Mathf.FloorToInt( time % 60);

        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }
    public void UpdateLevel()
    {
        levelText.text = "Map: " + (DataManager.Ins.GetLevelChosed() + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator Anounce()
    {
        anounceText.text = GetText();
        yield return new WaitForSeconds(2f);
        anounceText.gameObject.SetActive(false);
    }
    public string GetText()
    {
        if (DataManager.Ins.GetLevelChosed() == 0)
            return "SURVIVE FOR 3M30S !";
        else if (DataManager.Ins.GetLevelChosed() == 1)
            return "SURVIVE FOR 3M00S !";
        else if (DataManager.Ins.GetLevelChosed() == 5)
            return "JUST SURVIVE";
        else
            return "KILL THE BOSS";
    }    

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void UpdateWeapon()
    {
        if(closeAttackWeapon.gameObject.active == true)
            swordLevel.text = (closeAttackWeapon.weaponLevel +1) + "/" + closeAttackWeapon.stats.Count.ToString();
        if(spinWeapon.gameObject.active == true)
            fireballLevel.text = (spinWeapon.weaponLevel + 1) + "/" + spinWeapon.stats.Count.ToString();
        if (thrownWeapon.gameObject.active == true)
            axeLevel.text = (thrownWeapon.weaponLevel + 1) + "/" + thrownWeapon.stats.Count.ToString();
        if (projectileWeapon.gameObject.active == true)
            kunaiLevel.text = (projectileWeapon.weaponLevel +1) +"/" + projectileWeapon.stats.Count.ToString();
        if (zoneWeapon.gameObject.active == true)
            circleLevel.text = (zoneWeapon.weaponLevel+1)+ "/" + zoneWeapon.stats.Count.ToString();
    }
}
