using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject TeamPickers;
    public Camera MainCamera;

    MasterStage Stage = MasterStage.Waiting;

    Vector3 CameraStart;
    Vector3 CameraDestination;
    float CDTimerMax;
    float CDTimer;

    float TimerMax;
    float Timer;

    //bool BlockStuff;
    Dictionary<GameObject, float> BlockTimersMax;
    Dictionary<GameObject, float> BlockTimers;
    List<GameObject> Blocks = new List<GameObject>();

    public GameObject LeftPicker;
    public GameObject RightPicker;


    // Use this for initialization
    void Start()
    {
        Cub.View.Library.Initialization();
        Cub.Model.Library.Initialization();
        BuildMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (CDTimer > 0)
        {
            CDTimer = Mathf.Max(0, CDTimer - Time.deltaTime);
            float x = Mathf.Lerp(CameraStart.x, CameraDestination.x, (CDTimerMax - CDTimer) / CDTimerMax);
            float y = Mathf.Lerp(CameraStart.y, CameraDestination.y, (CDTimerMax - CDTimer) / CDTimerMax);
            float z = Mathf.Lerp(CameraStart.z, CameraDestination.z, (CDTimerMax - CDTimer) / CDTimerMax);
            MainCamera.transform.position = new Vector3(x, y, z);
        }
        if (Stage != MasterStage.Waiting)
        {
            HandleStage(Stage);
        }
        
    }
    void HandleStage(MasterStage stage)
    {
        switch (stage)
        {
            case MasterStage.CameraMoving:
                if (CDTimer <= 0)
                    SetStage(MasterStage.Waiting);
                break;
            case MasterStage.BlockBuilding:
                bool keepGoing = false;
                foreach (GameObject block in Blocks)
                {
                    BlockTimers[block] = Mathf.Max(0, BlockTimers[block] - Time.deltaTime);
                    Vector3 where = block.transform.position;
                    where.y = Mathf.Lerp(20.5f, -0.5f, (BlockTimersMax[block] - BlockTimers[block]) / BlockTimersMax[block]);
                    block.transform.position = where;
                    if (BlockTimers[block] > 0)
                        keepGoing = true;
                }
                if (!keepGoing)
                {
                    Timer = TimerMax = 1;
                    SetStage(MasterStage.TeamPickersSlideIn);
                }
                break;
            case MasterStage.TeamPickersSlideIn:
                Timer = Mathf.Max(0, Timer - Time.deltaTime);
                float x = Mathf.Lerp(300f, 198f, (TimerMax - Timer) / TimerMax);
                LeftPicker.transform.localPosition = new Vector3(-x, LeftPicker.transform.localPosition.y, LeftPicker.transform.localPosition.z);
                RightPicker.transform.localPosition = new Vector3(x, LeftPicker.transform.localPosition.y, LeftPicker.transform.localPosition.z);
                if (Timer <= 0)
                    SetStage(MasterStage.Waiting);
                break;
        }
    }


    public void GotoFightScreen()
    {
        MainMenu.SetActive(false);
        //CameraToPoint(new Vector3(-2, 3, -2),1);
        BlockTimers = new Dictionary<GameObject, float>();
        BlockTimersMax = new Dictionary<GameObject, float>();
        foreach (GameObject block in Blocks)
        {
            float n = Random.Range(0.8f, 1.2f);
            BlockTimers.Add(block, n);
            BlockTimersMax.Add(block, n);
        }
        SetStage(MasterStage.BlockBuilding);
        Debug.Log("FIGHT!");
    }

    public void GotoEditScreen()
    {
        MainMenu.SetActive(false);
        Debug.Log("EDIT!");
    }

    void CameraToPoint(Vector3 where, float time)
    {
        CameraStart = MainCamera.transform.position;
        CameraDestination = where;
        CDTimer = CDTimerMax = time;
    }

    void BuildMap()
    {
        Cub.Terrain[][] map = Cub.Model.Library.Stage_Terrain;
        Debug.Log(map.Length.ToString());
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {

                GameObject t = Cub.View.Library.Get_Terrain(map[y][x]);
                if (t != null)
                    Blocks.Add((GameObject)Instantiate(t, new Vector3(x, 20.5f, y), Quaternion.identity));
            }
        }
    }

    void SetStage(MasterStage stage)
    {
        Stage = stage;
    }
}

public enum MasterStage
{
    Waiting,
    CameraMoving,
    BlockBuilding,
    TeamPickersSlideIn
}
