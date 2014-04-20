using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayScreenController : MonoBehaviour
{

    //GameMode CurrentMode;
    //GameplayTeamPickerController Tpc;
    //public ScoreCardManager Scm;
    public MasterGameController GM;
    public DescriptionManager Desc;
    Cub.Model.Team TeamOne = null;
    Cub.Model.Team TeamTwo = null;
    public Cub.View.Runtime RT;
    bool RTStarted = false;
    //Cub.Position2 StageSize;
    public bool CurrentlyActive = false;

    // Use this for initialization
    void Start()
    {
        //StageSize = new Cub.Position2(10, 10);
        RT.GSC = this;
        //Tpc = (GameplayTeamPickerController)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("GameplayTeamPickerController"));
        //Scm = (ScoreCardManager)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("ScoreCardManager"));
        //Desc = (DescriptionManager)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("DescriptionManager"));
        Cub.View.NarratorController.Initialize(Desc);
        //SwitchModes(GameMode.TeamPick);
        //Cub.View.Library.Initialization();
        //Cub.Model.Library.Initialization();
        //BuildMap();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    Application.LoadLevel("Main Menu");
        //switch (CurrentMode)
        //{
        //    case GameMode.TeamPick:
        //        TeamPickUpdate();
        //        break;
        //    case GameMode.Gameplay:
        //        GameplayUpdate();
        //        break;
        //    case GameMode.Postgame:
        //        PostGameUpdate();
        //        break;
        //}
        if (CurrentlyActive)
            GameplayUpdate();
    }

    //void TeamPickUpdate()
    //{

    //}

    void GameplayUpdate()
    {
        if (Cub.Model.Main.GameStillRunning())
            MatchBuildUpdate();
    }

    void MatchBuildUpdate()
    {
        List<Cub.View.Eventon> events = Cub.Model.Main.Go();
        foreach (Cub.View.Eventon e in events)
        {
            Cub.View.Runtime.Add_Eventon(e);
        }
        if (!RTStarted && events.Count > 0)
        {
            RT.Run_Eventon();
            RTStarted = true;
        }
    }

    //void PostGameUpdate()
    //{
    //    if (Input.GetKey(KeyCode.Return))
    //        Application.LoadLevel("Main Menu");
    //}

    //public void SwitchModes(GameMode mode)
    //{
    //    CurrentMode = mode;
    //    switch (mode)
    //    {
    //        case GameMode.TeamPick:
    //            Scm.gameObject.SetActive(false);
    //            Desc.gameObject.SetActive(false);
    //            break;
    //        case GameMode.Gameplay:
    //            Scm.gameObject.SetActive(false);
    //            Tpc.gameObject.SetActive(false);
    //            Desc.gameObject.SetActive(false);
    //            break;
    //        case GameMode.Postgame:
    //            Scm.gameObject.SetActive(true);
    //            Tpc.gameObject.SetActive(false);
    //            Desc.gameObject.SetActive(false);
    //            break;
    //    }
    //}

    public void StartGame(Cub.Model.Team T1, Cub.Model.Team T2)
    {
        TeamOne = T1;
        TeamTwo = T2;
        if (TeamOne.Colour_Primary.r == TeamTwo.Colour_Primary.r &&
            TeamOne.Colour_Primary.g == TeamTwo.Colour_Primary.g && TeamOne.Colour_Primary.b == TeamTwo.Colour_Primary.b)
        {
            Color32 p = TeamTwo.Colour_Primary;
            TeamTwo.Colour_Primary = TeamTwo.Colour_Secondary;
            TeamTwo.Colour_Secondary = p;
        }

        //TeamOne.Colour_Primary = Color.red;
        //TeamOne.Colour_Secondary = Color.yellow;
        //TeamTwo.Colour_Primary = Color.green;
        //TeamTwo.Colour_Secondary = Color.blue;
        //TeamOne.MakeUnique();
        //TeamTwo.MakeUnique();
        foreach (Cub.Model.Character c in TeamOne.List_Character)
        {
            c.Stat.Position = TranslateStartPosition(c.Stat.Position, true);
            Cub.View.Runtime.Add_Character(c);
        }
        foreach (Cub.Model.Character c in TeamTwo.List_Character)
        {
            c.Stat.Position = TranslateStartPosition(c.Stat.Position, false);
            Cub.View.Runtime.Add_Character(c);

            Cub.View.Character view = Cub.View.Runtime.Get_Character(c.ID);

            Quaternion rot = view.gameObject.transform.rotation;
            rot.y = 180;
            view.gameObject.transform.rotation = rot;

        }
        Cub.Model.Main.Initialization(TeamOne, TeamTwo);
        //SwitchModes(GameMode.Gameplay);
        CurrentlyActive = true;
        Desc.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        //SwitchModes(GameMode.Postgame);
        GM.TurnOnScoreCard(TeamOne, TeamTwo);
        Cub.Tool.Xml.Serialize(GM.Teams, "Data/Team_Saves.xml");
    }

    public void ClearMap()
    {
        foreach (Cub.Model.Character c in TeamOne.List_Character)
        {
            Destroy(Cub.View.Runtime.Remove_Character(c.ID).gameObject);
        }
        foreach (Cub.Model.Character c in TeamTwo.List_Character)
        {
            Destroy(Cub.View.Runtime.Remove_Character(c.ID).gameObject);
        }
    }

    //void BuildMap()
    //{
    //    Cub.Terrain[][] map = Cub.Model.Library.Stage_Terrain;
    //    for (int y = 0; y < map.Length; y++)
    //    {
    //        for (int x = 0; x < map[0].Length; x++)
    //        {

    //            GameObject t = Cub.View.Library.Get_Terrain(map[y][x]);
    //            if (t != null)
    //                Instantiate(t, new Vector3(x, -0.5f, y), Quaternion.identity);
    //        }
    //    }
    //}

    public Cub.Position2 TranslateStartPosition(Cub.Position2 pos, bool teamOne)
    {
        int x = pos.X;
        int y = pos.Y;
        Cub.Position2 r = new Cub.Position2(x, y);
        if (!teamOne)
            r = new Cub.Position2(GM.xMapSize - x, GM.yMapSize - y);
        if (r.X > GM.xMapSize || r.X < 0 || r.Y > GM.yMapSize || r.Y < 0)
            Debug.Log("TRANSLATE ERROR");
        return r;
    }

    public void Reset()
    {
        RTStarted = false;
        TeamOne = null;
        TeamTwo = null;
    }
}

//public enum GameMode
//{
//    TeamPick,
//    Gameplay,
//    Postgame
//}
