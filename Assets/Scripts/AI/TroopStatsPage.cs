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
    [SerializeField]
    GameObject UnitsTeam;

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
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().fullHealth.ToString();

                UnitsTeam.SetActive(true);
                UnitsTeam.GetComponent<Text>().text = "Team: " +
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().Teams.ToString();


                SliderScript.gameObject.SetActive(true);
                SliderScript.SetHealth(Player.SelectedUnitList[0].GetComponent<Actual_AI>().fullHealth,
                    Player.SelectedUnitList[0].GetComponent<Actual_AI>().health);
            } else
            {
                if (Player.SelectedBuilding == null && Player.OtherTeamUnitSelected == null)
                {
                    //this is here so the slider will get deactivated if the unit dies
                    UnitsTeam.SetActive(false);
                    SliderScript.gameObject.SetActive(false);
                    UnitAttack.SetActive(false);
                    HpNumber.SetActive(false);
                    UnitInfoPage.SetActive(false);
                }
            }
        }
        else 
        {
            if (Player.SelectedBuilding == null && Player.OtherTeamUnitSelected == null)
            {
                UnitsTeam.SetActive(false);
                SliderScript.gameObject.SetActive(false);
                UnitAttack.SetActive(false);
                HpNumber.SetActive(false);
                UnitInfoPage.SetActive(false);
            }
        }
        if(Player.OtherTeamUnitSelected != null)
        {
            UnitInfoPage.SetActive(true);

            UnitAttack.SetActive(true);
            UnitAttack.GetComponent<Text>().text = "Attack Damage: " +
                Player.OtherTeamUnitSelected.GetComponent<Actual_AI>().attackDamage.ToString();

            HpNumber.SetActive(true);
            HpNumber.GetComponent<Text>().text = "Health: " +
                Player.OtherTeamUnitSelected.GetComponent<Actual_AI>().health.ToString() + " / " +
                Player.OtherTeamUnitSelected.GetComponent<Actual_AI>().fullHealth.ToString();

            UnitsTeam.SetActive(true);
            UnitsTeam.GetComponent<Text>().text = "Team: " +
                Player.OtherTeamUnitSelected.GetComponent<Actual_AI>().Teams.ToString();


            SliderScript.gameObject.SetActive(true);
            SliderScript.SetHealth(Player.OtherTeamUnitSelected.GetComponent<Actual_AI>().fullHealth,
                Player.OtherTeamUnitSelected.GetComponent<Actual_AI>().health);
        }    
        if (Player.SelectedBuilding != null)
        {
            if (Player.SelectedBuilding.GetComponent<Harvester>())
            {
                UnitInfoPage.SetActive(true);

                HpNumber.SetActive(true);
                HpNumber.GetComponent<Text>().text = "Health: " +
                    Player.SelectedBuilding.GetComponent<Harvester>().health.ToString() + " / " +
                    Player.SelectedBuilding.GetComponent<Harvester>().fullHealth.ToString();

                UnitsTeam.SetActive(true);
                UnitsTeam.GetComponent<Text>().text = "Team: " +
                    Player.SelectedBuilding.GetComponent<Harvester>().Teams.ToString();


                SliderScript.gameObject.SetActive(true);
                SliderScript.SetHealth(Player.SelectedBuilding.GetComponent<Harvester>().fullHealth,
                    Player.SelectedBuilding.GetComponent<Harvester>().health);
            }
            if(Player.SelectedBuilding.GetComponent<Building>())
            {
                HpNumber.SetActive(true);
                HpNumber.GetComponent<Text>().text = "Health: " +
                    Player.SelectedBuilding.GetComponent<Building>().health.ToString() + " / " +
                    Player.SelectedBuilding.GetComponent<Building>().fullHealth.ToString();

                UnitsTeam.SetActive(true);
                UnitsTeam.GetComponent<Text>().text = "Team: " +
                    Player.SelectedBuilding.GetComponent<Building>().Teams.ToString();


                SliderScript.gameObject.SetActive(true);
                SliderScript.SetHealth(Player.SelectedBuilding.GetComponent<Building>().fullHealth,
                    Player.SelectedBuilding.GetComponent<Building>().health);
            }
        }
    }
}
