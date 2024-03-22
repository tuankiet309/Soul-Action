using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickup pickup;

    [SerializeField] public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;

    public List<Weapon> weaponsToUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if(currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);

        SFXManager.instance.PlaySFXPitched(2);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        if (expValue == 1)
        {
            pickup.transform.localScale = Vector3.one * 0.4f;
            pickup.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else if (expValue == 2)
        {
            pickup.transform.localScale = Vector3.one * 0.5f;
            pickup.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else if (expValue == 3)
        {
            pickup.transform.localScale = Vector3.one * 0.7f;
            pickup.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else if (expValue == 4)
        {
            pickup.transform.localScale = Vector3.one * 1f;
            pickup.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else if (expValue == 5)
        {
            pickup.transform.localScale = Vector3.one * 1.3f;
            pickup.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }
        else if(expValue >= 6)
        {
            pickup.transform.localScale = Vector3.one * 1.5f;
            pickup.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            
        }
            
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if(currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        //PlayerController.instance.activeWeapon.LevelUp();

        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        //UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[0]);

        //UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.levelUpButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);

        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if(availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for(int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for(int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }


        for(int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if(i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            } else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatController.instance.UpdateDisplay();
    }
}
