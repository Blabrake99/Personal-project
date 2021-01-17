using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopStatsPage : MonoBehaviour
{
    [SerializeField]
    GameObject UnitInfoPage;
    [SerializeField]
    Players_Script Player;
    [SerializeField]
    HealthSlider SliderScript;
    [SerializeField]
    GameObject UnitAttack;
    [SerializeField]
    GameObject HpNumber;


    // Update is called once per frame
    void Update()
    {
        if(Player.SelectedUnitList.Count == 1)
        {
            //this is here fot if the unit gets destroyed while
            //it's trying to figure out the health
            if (Player.SelectedUnitList[0] != null)
            {
                UnitInfoPage.SetActive(true);

                UnitAttack.SetActive(true);
                UnitAttack.GetComponent<Text>().text = "Attack Damage: " +
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().attackDamage.ToString();

                HpNumber.SetActive(true);
                HpNumber.GetComponent<Text>().text = "Health: " +
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().health.ToString() + " / " + 
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().fullHealth.ToString(); ;

                SliderScript.gameObject.SetActive(true);
                SliderScript.SetHealth(Player.SelectedUnitList[0].GetComponent<Actual_AI>().fullHealth,
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().health);
            } else
            {
                //this is here so the slider will get deactivated if the unit dies
                SliderScript.gameObject.SetActive(false);
                UnitAttack.SetActive(false);
                HpNumber.SetActive(false);
                UnitInfoPage.SetActive(false);
            }
        }
        else 
        {
            SliderScript.gameObject.SetActive(false);
            UnitAttack.SetActive(false);
            HpNumber.SetActive(false);
            UnitInfoPage.SetActive(false);
        }
    }
}
