using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;
    
    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name + " - Lvl " + theWeapon.weaponLevel;
        } else
        {
            upgradeDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }

        assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if(assignedWeapon != null)
        {

            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
                UIController.instance.UpdateWeapon();

            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
