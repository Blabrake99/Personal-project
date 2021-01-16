using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PauseScript : MonoBehaviour
{
    string PlayersCurrentTeam;
    public bool IsPaused = false;

    [SerializeField]
    GameObject PauseCanvas;

    [SerializeField]
    GameObject PlayerOBJ;

    [SerializeField]
    Text CurrentTeamTxt;
    [SerializeField]
    Dropdown TeamDropDown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused == true)
            {
                IsPaused = false;

            }
            else
            {
                IsPaused = true;

            }
        }
        if (IsPaused == true)
        {
            PauseGame();
            PauseCanvas.SetActive(true);
            PlayersCurrentTeam = PlayerOBJ.GetComponent<Players_Script>().Teams.ToString();
            CurrentTeamTxt.text = "Current Team: " + PlayersCurrentTeam;
        }
        else
        {
            ResumeGame();
            PauseCanvas.SetActive(false);

        }
    }

    //pauses the game 
    public void PauseGame()
    {
        //this pauses everything that's time based except the update functions
        //but fixed updates wont be called 
        Time.timeScale = 0;
        //this will also pause the audio 
        AudioListener.pause = true;
    }
    //resumes the game 
    public void ResumeGame()
    {
        //unpaused time 
        Time.timeScale = 1;
        //this will unpause audio
        AudioListener.pause = false;
    }
    //this gets called when the someone selects something in the dropdown
    public void OnDropDownSwitch()
    {
        //this puts the player on a new team 
        PlayerOBJ.GetComponent<Players_Script>().SetTeam(TeamDropDown.options[TeamDropDown.value].text);
    }
}
