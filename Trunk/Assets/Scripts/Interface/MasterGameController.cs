using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameController : MonoBehaviour
{
    public Cub.Interface.TitleScreenController MainMenu;
    public GameObject TeamPickers;
    public GameplayScreenController GSC;
    //public GameObject MainMenu;
    //public GameObject TeamPickers;
    public Camera MainCamera;
    //public GameObject PersonalCamera;

    public Cub.View.Runtime Runtime;

    List<Cub.Model.TeamSave> Teams;

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
    public List<GameObject> Blocks = new List<GameObject>();

    public Cub.Interface.TeamPickerManager LeftPicker;
    public Cub.Interface.TeamPickerManager RightPicker;
    public TeamEditorManager LeftEditor;
    public TeamEditorManager RightEditor;
    public CharacterEditorManager LeftCEditor;
    public CharacterEditorManager RightCEditor;


    // Use this for initialization
    void Start()
    {
        Cub.View.Library.Initialization();
        Cub.Model.Library.Initialization();
        BuildMap();
        string name = typeof(List<Cub.Model.TeamSave>).AssemblyQualifiedName;
        Teams = (List<Cub.Model.TeamSave>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/Team_Saves.xml");
        MainMenu.CurrentlyActive = true;
        GSC.gameObject.SetActive(false);
        LeftCEditor.PersonalCamera.SetActive(false);
        RightCEditor.PersonalCamera.SetActive(false);
        LeftEditor.gameObject.SetActive(false);
        RightEditor.gameObject.SetActive(false);
        LeftCEditor.gameObject.SetActive(false);
        RightCEditor.gameObject.SetActive(false);
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
        else if (TeamPickers.activeSelf)
        {
            if (LeftPicker.Chosen && RightPicker.Chosen && LeftPicker.SelectedTeam != null && RightPicker.SelectedTeam != null)
                StartGameplay();
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
                LeftPicker.transform.localPosition =
                    new Vector3(-x, LeftPicker.transform.localPosition.y, LeftPicker.transform.localPosition.z);
                RightPicker.transform.localPosition =
                    new Vector3(x, LeftPicker.transform.localPosition.y, LeftPicker.transform.localPosition.z);
                if (Timer <= 0)
                {
                    SetStage(MasterStage.Waiting);
                    LeftPicker.CurrentlyActive = true;
                    RightPicker.CurrentlyActive = true;
                }
                break;
        }
    }


    public void GotoFightScreen()
    {
        MainMenu.gameObject.SetActive(false);
        LeftPicker.Setup(Teams);
        RightPicker.Setup(Teams);
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
        MainMenu.gameObject.SetActive(false);
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
        //Debug.Log(map.Length.ToString());
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

    void StartGameplay()
    {
        Cub.Model.TeamSave TS1 = LeftPicker.SelectedTeam;
        Cub.Model.TeamSave TS2 = RightPicker.SelectedTeam;
        Cub.Model.Team TeamOne = TS1.Extract_Team();
        Cub.Model.Team TeamTwo = TS2.Extract_Team();
        TeamPickers.SetActive(false);
        GSC.gameObject.SetActive(true);
        GSC.StartGame(TeamOne, TeamTwo);
    }

    public Cub.Position2 TranslateStartPosition(Cub.Position2 pos, bool teamOne)
    {
        int x = pos.X;
        int y = pos.Y;
        Cub.Position2 r = new Cub.Position2(x, y);
        if (!teamOne)
            r = new Cub.Position2(11 - x, 11 - y);
        if (r.X > 11 || r.X < 0 || r.Y > 11 || r.Y < 0)
            Debug.Log("TRANSLATE ERROR");
        return r;
    }

    public void EditTeam(Cub.Model.TeamSave team, Cub.Interface.TeamPickerManager picker)
    {
        Debug.Log("EDIT TEAM: " + team.Name);
        picker.gameObject.SetActive(false);
        if (picker.PlayerOne) {
            LeftEditor.Setup(team);
            LeftEditor.gameObject.SetActive(true);
            LeftEditor.CurrentlyActive = true;
        }
        else
        {
            RightEditor.Setup(team);
            RightEditor.gameObject.SetActive(true);
            RightEditor.CurrentlyActive = true;
        }


    }

    public void EditCharacter(TeamEditorManager tem, Cub.View.Character VChar, Cub.Model.Character_Save SChar)
    {
        CharacterEditorManager cem;
        if (tem.PlayerOne)
            cem = LeftCEditor;
        else
            cem = RightCEditor;
        tem.gameObject.SetActive(false);
        cem.gameObject.SetActive(true);
        cem.Setup(VChar, SChar);
    }
}

public enum MasterStage
{
    Waiting,
    CameraMoving,
    BlockBuilding,
    TeamPickersSlideIn
}
