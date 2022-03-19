using System.Collections.Generic;
using UnityEngine;
public class Building : MonoBehaviour
{
    [SerializeField]
    private Team team;

    public Team Teams => team;

    public int health = 500;

    [SerializeField]
    Players_Script player;

    public GameObject BuildingUI;
    public GameObject BuildingOBJ;
    public GameObject DestroyButton;
    public bool InUI;
    public GameObject Spawner;
    public List<GameObject> Unit;
    Material Mat;

    //for the ProgressBar
    public Unit_ProgressBar ProgressCanvas;
    public GameObject CanvasPrefab;
    GameObject temp;
    bool onlyOnce;
    int tempCounter;
    
    [HideInInspector]
    public HideBuildingUI hideBuildingUI;
    void Awake()
    {
        hideBuildingUI = FindObjectOfType<HideBuildingUI>();
        player = GameObject.Find("PlayerOBJ").GetComponent<Players_Script>();
        SetTeam(player.Teams.ToString());
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject == this.gameObject && Teams.ToString() == player.Teams.ToString())
                {
                    if (hideBuildingUI.SetActive)
                        hideBuildingUI.OnButtonPress();

                    hideBuildingUI.gameObject.SetActive(false);
                    InUI = true;
                    BuildingOBJ = Instantiate(BuildingUI);
                    BuildingOBJ.GetComponent<BuildingCanvas>().CurrentBase = this;
                }
            }
        }
        if (InUI && Input.GetKey(KeyCode.Escape) ||
            Input.GetMouseButton(1))
        {
            hideBuildingUI.gameObject.SetActive(true);
            InUI = false;
        }
        #region ProgressBar

        if (BuildingOBJ != null && tempCounter == 0)
        {
            tempCounter = BuildingOBJ.GetComponent<Button_Unit>().spawnCounter;
        }

        if (tempCounter > 0)
        {
            if (!onlyOnce)
            {
                GameObject TempCanvas = Instantiate(CanvasPrefab);
                GameObject tempobj = TempCanvas;
                TempCanvas.transform.SetParent(this.gameObject.transform);
                ProgressCanvas = tempobj.GetComponentInChildren<Canvas>().transform.GetComponentInChildren<Unit_ProgressBar>();
                TempCanvas.transform.position = this.gameObject.transform.position + new Vector3(0, 7, 0);
                onlyOnce = true;
            }

            if (ProgressCanvas != null)
                ProgressCanvas.StartTimer(Unit[0].GetComponent<Actual_AI>().buildSpeed);

            if (ProgressCanvas.IsDone())
            {
                spawn_Unit();
                if (tempCounter > 0)
                {
                    ProgressCanvas.ResetProgressBar();
                }

            }
        }
        else
        {
            if (ProgressCanvas != null)
            {
                GameObject DeleteOBJ = this.gameObject.transform.Find("Progress_CanvasOBJ(Clone)").gameObject;
                Destroy(DeleteOBJ);
            }
            onlyOnce = false;
        }
        #endregion

        if (temp != null && Spawner != null)
        {

            temp.GetComponent<Actual_AI>().GoToArea = Spawner.transform.position;
            temp.GetComponent<Actual_AI>().Selected = true;

            temp = null;

        }
        if (temp != null && Spawner == null)
        {

            temp.GetComponent<Actual_AI>().GoToArea = new Vector3(Random.Range(this.gameObject.transform.position.x - 10, this.gameObject.transform.position.x + 10), transform.position.y + .5f,
                Random.Range(this.gameObject.transform.position.z - 12, this.gameObject.transform.position.z - 15));
            temp.GetComponent<Actual_AI>().Selected = true;
            temp = null;
        }


    }
    public void spawn_Unit()
    {
        if (BuildingOBJ != null)
        {
            BuildingOBJ.GetComponent<Button_Unit>().spawnCounter -= 1;
        }

        temp = Instantiate(Unit[0], this.gameObject.transform.position - new Vector3(0, -.5f, this.gameObject.transform.localScale.z), Quaternion.identity);
        temp.GetComponent<Actual_AI>().SetTeam(Teams.ToString());
        Unit.Remove(Unit[0]);
        tempCounter = Unit.Count;
    }
    public void DestroySpawnPoint()
    {
        if (Spawner != null)
        {
            Destroy(Spawner);
        }
    }
    public void SetTeam(System.String t)
    {
        team = (Team)System.Enum.Parse(typeof(Team), t);
    }
    public enum Team
    {
        blue,
        red,
        green,
        yellow,
        brown,
        pink,
        orange,
        black,
        white
    }

}
