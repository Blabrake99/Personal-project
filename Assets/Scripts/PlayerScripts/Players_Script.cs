//using System;
using System.Collections.Generic;
using UnityEngine;

public class Players_Script : MonoBehaviour
{
    [SerializeField]
    private Team team;

    public Team Teams => team;

    #region Unit Selecting
    //this is for the mouse selecting 
    bool isSelecting = false;
    Vector3 mousePosition1;
    public GameObject[] UnitArr;
    public List<GameObject> SelectedUnitList;

    public Vector3 worldPosition;

    public int[] resources = new int[3];


    void Awake()
    {
        //wood
        resources[0] = 500;
        //metal
        resources[1] = 500;
        //oil
        resources[2] = 500;

        // this will just pick a random team out 
        // of the current max we have now
        // team = (Team)Random.Range(0, System.Enum.GetValues(typeof(Team)).Length);
        //but for testing i'm just going to start off on black team
        team = Team.black;
        UnitArr = GameObject.FindGameObjectsWithTag("Unit");
        SelectedUnitList = new List<GameObject>();
    }
    void Update()
    {
        UnitArr = GameObject.FindGameObjectsWithTag("Unit");
        Plane plane = new Plane(Vector3.up, 0);



        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        if (Input.GetMouseButtonDown(0))
        {

            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            //if (SelectedUnitList.Count > 0)
            //{
            //    for (int i = 0; i < SelectedUnitList.Count; i++)
            //    {

            //        if (SelectedUnitList[i] != null && team.ToString() == SelectedUnitList[i].GetComponent<Actual_AI>().Teams.ToString())
            //        {

            //            if (SelectedUnitList[i].GetComponent<Actual_AI>().Selected)
            //            {
            //                //SelectedUnitList[i].GetComponent<Actual_AI>().Selected = false;
            //            }
                        
            //        }
            //    }

            //}

            SelectedUnitList.Clear();
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {

            isSelecting = false;

        }
        if (Input.GetMouseButtonDown(1))
        {

            List<Vector3> targetPosList = GetPositionListAround(new Vector3(worldPosition.x, worldPosition.y, worldPosition.z), 5f, SelectedUnitList.Count);

            for (int i = 0; i < SelectedUnitList.Count; i++)
            {
               
                if (SelectedUnitList[i] != null)
                {
                    //it'll make formation for the units if there is more than 1 
                    if (SelectedUnitList.Count > 1)
                    {
                        SelectedUnitList[i].GetComponent<Actual_AI>().GoToArea = new Vector3(targetPosList[i].x,
                            SelectedUnitList[i].GetComponent<Actual_AI>().transform.position.y, targetPosList[i].z);
                        SelectedUnitList[i].GetComponent<Actual_AI>().Selected = true;
                    } else
                    {
                        SelectedUnitList[i].GetComponent<Actual_AI>().GoToArea = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                        SelectedUnitList[i].GetComponent<Actual_AI>().Selected = true;
                    }

                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            foreach (GameObject S in SelectedUnitList)
            {
                if (S != null)
                {
                    if (S.GetComponent<Actual_AI>().Selected)
                        S.GetComponent<Actual_AI>().Selected = false;
                }
            }
        }
        foreach (GameObject G in UnitArr)
        {
            if (G != null)
            {
                if (IsWithinSelectionBounds(G) && team.ToString() == G.GetComponent<Actual_AI>().Teams.ToString())
                {
                    if (!SelectedUnitList.Contains(G))
                    {
                        SelectedUnitList.Add(G);
                    }
                }
            }
        }

    }
    private List<Vector3> GetPositionListAround(Vector3 startpos, float dist, int posCount)
    {
        List<Vector3> posList = new List<Vector3>();
        for (int i = 0; i < posCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(startpos.x - dist, startpos.x + dist), startpos.y, Random.Range(startpos.z - dist, startpos.z + dist));
            posList.Add(pos);
        }
        return posList;
    }
    public void SetTeam(System.String t)
    {
        team = (Team)System.Enum.Parse(typeof(Team), t);
    }
    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }
    #endregion


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
