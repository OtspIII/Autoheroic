using UnityEngine;
using System.Collections;
using Cub.Model;
using System.Collections.Generic;

public class TeamEditorManager : MonoBehaviour
{

    public GameObject SquareMarker;
    //public GameObject SquareMarkerType;
    //public List<GameObject> SquareMarkers;
    Cub.Position2 SelectedSquare;
    public MasterGameController GM;
    public bool CurrentlyActive = false;
    bool Clicking = false;

    private Dictionary<System.Guid, Cub.View.Character> Dictionary_Character { get; set; }
    private Dictionary<System.Guid, Cub.Model.Character_Save> Dictionary_CharSave { get; set; }
    private Dictionary<Cub.Position2, System.Guid> Dictionary_CharPos { get; set; }
    private List<GameObject> CharacterModels;

    public TeamSave Team = null;
    Team FakeTeam = null;

    Cub.View.Character Current_Char = null;
    Cub.Model.Character_Save Current_CharSave = null;
    bool MovingCharacter = false;
    Cub.Position2 OldPos = new Cub.Position2(999, 999);
    Vector3 OldVec = Vector3.zero;

    float SelectTimer = 0.2f;
    float VertTimer;
    float HoriTimer;
    public bool PlayerOne;
    public bool PlayerTwo;
    public Rect SelectRange;

    public bool Ready;
    public UISprite ReadyButton;

    Cub.Interface.TeamPickerManager MyPicker;
    //CharacterEditorManager MyCEditor;
    TextInputController MyTextEditor;

    public Texture Green;
    public Texture Red;
    public UILabel TeamName;
    public UILabel OwnerName;
    //public UILabel WinNum;
    //public UILabel LossNum;
    //public UILabel WinPerc;
    //public UITexture WRBack;
    //public UITexture WRFront;
    public UILabel PtsLeft;
    public UITexture PtsBack;
    public UITexture PtsFront;
    //public UISprite PrimaryColor;
    //public UISprite SecondaryColor;


    // Use this for initialization
    void Start()
    {
        GM = (MasterGameController)GameObject.Find("Game Master").GetComponent("MasterGameController");
        if (PlayerOne)
        {
            MyPicker = GM.LeftPicker;
            //MyCEditor = GM.LeftCEditor;
            MyTextEditor = GM.LeftNameEditor;
        }
        else
        {
            MyPicker = GM.RightPicker;
            //MyCEditor = GM.RightCEditor;
            MyTextEditor = GM.RightNameEditor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!CurrentlyActive)
            return;
        if (VertTimer > 0)
            VertTimer -= Time.deltaTime;
        if (HoriTimer > 0)
            HoriTimer -= Time.deltaTime;

        Cub.Position2 Move = new Cub.Position2();

        if (GetInput("Vertical") > 0.1f && VertTimer <= 0)
        {
            Move.Y = 1;
            VertTimer = SelectTimer;
        }
        else if (GetInput("Vertical") < -0.1f && VertTimer <= 0)
        {
            Move.Y = -1;
            VertTimer = SelectTimer;
        }
        else if (VertTimer > 0 && Mathf.Abs(GetInput("Vertical")) < 0.1f)
            VertTimer = 0;

        if (GetInput("Horizontal") > 0.1f && HoriTimer <= 0)
        {
            Move.X = 1;
            HoriTimer = SelectTimer;
        }
        else if (GetInput("Horizontal") < -0.1f && HoriTimer <= 0)
        {
            Move.X = -1;
            HoriTimer = SelectTimer;
        }
        else if (HoriTimer > 0 && Mathf.Abs(GetInput("Horizontal")) < 0.1f)
            HoriTimer = 0;

        if (Move.X != 0 || Move.Y != 0)
            SlideSelector(Move);

        if (GetInput("Click") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                Click();
            }
        }
        else if (GetInput("Ready") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                if (Ready)
                {
                    Ready = false;
                    ReadyButton.color = Color.white;
                }
                else if (Team.TotalValue <= Cub.Model.Library.PointCap)
                {
                    Ready = true;
                    ReadyButton.color = Color.green;
                    GM.CheckMatchReady();
                }
            }
        }
        else if (GetInput("Delete") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                if (Current_CharSave != null)
                {
                    Team.Remove_Character(Current_CharSave);
                    Dictionary_CharSave.Remove(Current_CharSave.ID);
                    Dictionary_Character.Remove(Current_CharSave.ID);
                    Dictionary_CharPos.Remove(Current_Char.Stat.Position);
                    CharacterModels.Remove(Current_Char.gameObject);
                    //foreach (GameObject go in SquareMarkers.ToArray())
                    //    if (go.transform.position.x == (float)Current_Char.Stat.Position.X && go.transform.position.z == (float)Current_Char.Stat.Position.Y)
                    //    {
                    //        SquareMarkers.Remove(go);
                    //        Destroy(go);
                    //    }
                    Destroy(Current_Char.gameObject);
                    Current_Char = null;
                    Current_CharSave = null;
                    Refresh();
                }
            }
        }
        else if (GetInput("Escape") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                if (MovingCharacter)
                {
                    Current_Char.gameObject.transform.position = OldVec;
                    OldVec = Vector3.zero;
                    OldPos = new Cub.Position2(999, 999);
                    MovingCharacter = false;
                    SquareMarker.renderer.material.color = Color.red;
                }
                else
                {
                    MyPicker.gameObject.SetActive(true);
                    MyPicker.Clicking = true;
                    MyPicker.MyInstructions.SetActive(true);
                    Clear();
                    Cub.Tool.Xml.Serialize(GM.Teams, "Data/Team_Saves.xml");
                }

            }
        }
        else if (GetInput("Move") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                if (Current_Char != null)
                {
                    if (MovingCharacter)
                    {
                        MovingCharacter = false;
                        SquareMarker.renderer.material.color = Color.red;
                        Cub.Position2 where = new Cub.Position2((int)SquareMarker.transform.position.x, (int)SquareMarker.transform.position.z);
                        Current_Char.Stat.Position = where;
                        Dictionary_CharPos.Add(where, Current_CharSave.ID);
                        if (!PlayerOne)
                        {
                            where = new Cub.Position2(GM.xMapSize - where.X, GM.yMapSize - where.Y);
                        }
                        Current_CharSave.Position = where;
                        Dictionary_CharPos.Remove(OldPos);
                        OldPos = new Cub.Position2(999, 999);
                        OldVec = Vector3.zero;
                    }
                    else
                    {
                        MovingCharacter = true;
                        SquareMarker.renderer.material.color = Color.yellow;
                        OldPos = Current_Char.Stat.Position;
                        OldVec = Current_Char.gameObject.transform.position;
                    }
                }
            }
        }
        else if (GetInput("Color") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                int index = Mathf.Max(0, GM.Colors.IndexOf(Team.Colour_Primary));
                index++;
                if (index >= GM.Colors.Count)
                    index = 0;
                Team.Colour_Primary = GM.Colors[index];
                Refresh();
            }
        }
        else if (GetInput("Color") < -0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                int index = Mathf.Max(0, GM.Colors.IndexOf(Team.Colour_Secondary));
                index++;
                if (index >= GM.Colors.Count)
                    index = 0;
                Team.Colour_Secondary = GM.Colors[index];
                Refresh();
            }
        }
        else if (GetInput("Namer") < -0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                MyTextEditor.gameObject.SetActive(true);
                MyTextEditor.SetupTeam(this, Team);
                gameObject.SetActive(false);
            }
        }
        else if (GetInput("Namer") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                MyTextEditor.gameObject.SetActive(true);
                MyTextEditor.SetupOwner(this, Team);
                gameObject.SetActive(false);
            }
        }
        else
            Clicking = false;
    }

    public void Clear()
    {
        ClearMarkers();
        gameObject.SetActive(false);
    }

    public void ClearMarkers()
    {
        Dictionary_Character.Clear();
        Dictionary_CharPos.Clear();
        Dictionary_CharSave.Clear();
        //foreach (GameObject go in SquareMarkers)
        //    Destroy(go);
        foreach (GameObject go in CharacterModels)
            Destroy(go);
        CharacterModels.Clear();
        Current_Char = null;
        Current_CharSave = null;
        CurrentlyActive = false;
        SquareMarker.transform.position = new Vector3(-5, 1000, 5);
    }

    public void Setup(Cub.Model.TeamSave team)
    {

        Team = team;
        SquareMarker.SetActive(true);
        if (PlayerOne)
        {
            SelectRange = new Rect(0, 0, GM.PlacementSize - 1, GM.yMapSize);
            Team.Colour_Primary = Color.red;
            Team.Colour_Secondary = Color.yellow;
        }
        else
        {
            SelectRange = new Rect(GM.xMapSize - GM.PlacementSize + 1, 0, GM.PlacementSize - 1, GM.yMapSize);
            Team.Colour_Primary = Color.green;
            Team.Colour_Secondary = Color.blue;
        }
        SelectedSquare = new Cub.Position2((int)SelectRange.x, (int)SelectRange.y);
        //SquareMarkers = new List<GameObject>();
        MoveSelector();
        Dictionary_Character = new Dictionary<System.Guid, Cub.View.Character>();
        Dictionary_CharSave = new Dictionary<System.Guid, Character_Save>();
        Dictionary_CharPos = new Dictionary<Cub.Position2, System.Guid>();
        CharacterModels = new List<GameObject>();
        FakeTeam = Team.Extract_Team();
        foreach (Character c in FakeTeam.List_Character)
        {
            AddCharacter(c);
        }
        SlideSelector(new Cub.Position2(0, 0));
        Refresh();
        Clicking = true;
        Ready = false;
    }

    public void Refresh()
    {
        TeamName.text = Team.Name;
        OwnerName.text = Team.Owner_Name;
        //float wins = Team.Wins;
        //float losses = Team.Losses;
        //WinNum.text = wins.ToString();
        //LossNum.text = losses.ToString();
        //float perc = 100;
        //if (wins + losses != 0)
        //    perc = 100.0f * wins / (losses + wins);
        //WinPerc.text = Mathf.RoundToInt(perc).ToString() + "%";
        //WRFront.SetDimensions(Mathf.RoundToInt(perc * 150 / 100), 10);
        int TotalPts = Cub.Model.Library.PointCap;
        int Spent = Team.TotalValue;
        PtsLeft.text = (TotalPts - Spent).ToString() + "pts";
        PtsFront.SetDimensions(6, Mathf.RoundToInt(100 * Spent / TotalPts));
        if (Spent > TotalPts)
        {
            PtsFront.mainTexture = Red;
            ReadyButton.color = Color.red;

        }
        else
        {
            PtsFront.mainTexture = Green;
            ReadyButton.color = Color.white;
        }
        Ready = false;
        //PrimaryColor.color = new Color(Team.Colour_Primary.r, Team.Colour_Primary.g, Team.Colour_Primary.b);
        //SecondaryColor.color = new Color(Team.Colour_Secondary.r, Team.Colour_Secondary.g, Team.Colour_Secondary.b);
    }

    Cub.View.Character AddCharacter(Cub.Model.Character c)
    {
        Vector3 Rot = new Vector3(0, 270, 0);
        if (PlayerOne)
            Rot = new Vector3(0, 90, 0);
        c.Stat.Position = TranslateStartPosition(c.Stat.Position, PlayerOne);
        Cub.View.Character C = Cub.View.Runtime.Add_Character(c);

        Dictionary_Character[c.ID_Save] = C;
        foreach (Character_Save cs in Team.Chars)
            if (cs.ID == c.ID_Save)
                Dictionary_CharSave[c.ID_Save] = cs;
        C.transform.rotation = Quaternion.Euler(Rot);
        //SquareMarkers.Add((GameObject)Instantiate(SquareMarkerType, C.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity));
        Dictionary_CharPos.Add(c.Stat.Position, c.ID_Save);
        CharacterModels.Add(C.gameObject);
        return C;
    }

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

    void SlideSelector(Cub.Position2 move)
    {
        if (!MovingCharacter)
        {
            if (Current_Char != null)
            {
                //Current_Char.gameObject.SetActive(false);
                Current_Char = null;
                Current_CharSave = null;
            }
        }

        int x = Mathf.Min((int)(SelectRange.x + SelectRange.width), Mathf.Max((int)(SelectRange.x), SelectedSquare.X + move.X));
        int y = Mathf.Min((int)(SelectRange.y + SelectRange.height), Mathf.Max((int)(SelectRange.y), SelectedSquare.Y + move.Y));
        if (MovingCharacter)
        {
            if (move.X > 0)
            {
                while (Dictionary_CharPos.ContainsKey(new Cub.Position2(x, y)))
                    x++;
            }
            else if (move.X < 0)
            {
                while (Dictionary_CharPos.ContainsKey(new Cub.Position2(x, y)))
                    x--;
            }
            if (move.Y > 0)
            {
                while (Dictionary_CharPos.ContainsKey(new Cub.Position2(x, y)))
                    y++;
            }
            else if (move.Y < 0)
            {
                while (Dictionary_CharPos.ContainsKey(new Cub.Position2(x, y)))
                    y--;
            }
            if (x < (int)(SelectRange.x) || x > (int)(SelectRange.x + SelectRange.width) ||
                y < (int)(SelectRange.y) || y > (int)(SelectRange.y + SelectRange.height))
                return;
        }
        SelectedSquare = new Cub.Position2(x, y);
        MoveSelector();
        if (MovingCharacter)
        {
            Vector3 where = SquareMarker.transform.position;
            where.y += 0.5f;
            Current_Char.gameObject.transform.position = where;
        }
        if (Dictionary_CharPos.ContainsKey(SelectedSquare))
        {
            Current_Char = Dictionary_Character[Dictionary_CharPos[SelectedSquare]];
            Current_CharSave = Dictionary_CharSave[Dictionary_CharPos[SelectedSquare]];
        }
    }

    void MoveSelector()
    {
        SquareMarker.transform.position = new Vector3(SelectedSquare.X, -0.5f, SelectedSquare.Y);
    }

    float GetInput(string axis)
    {
        float r = 0;
        if (PlayerOne)
            r += Input.GetAxis(axis + " P1");
        if (PlayerTwo)
            r += Input.GetAxis(axis + " P2");
        return r;
    }

    void Click()
    {
        if (Current_CharSave == null)
        {
            Cub.Position2 where = new Cub.Position2((int)SquareMarker.transform.position.x, (int)SquareMarker.transform.position.z);
            if (!PlayerOne)
            {
                where = new Cub.Position2(GM.xMapSize - where.X, GM.yMapSize - where.Y);
            }
            Character_Save cs = new Character_Save(Cub.Model.Library.CharacterName(), Cub.Part_Head.Soldier, Cub.Part_Arms.Rifle, Cub.Part_Body.Medium,
                Cub.Part_Legs.Humanoid, where.X, where.Y);
            Team.Add_Character(cs);
            Cub.Model.Character ch = new Character(cs);
            FakeTeam.Add_Character(ch);

            Current_Char = AddCharacter(ch);
            Current_CharSave = cs;
        }
        foreach (GameObject go in CharacterModels)
        {
            if (Current_Char.gameObject != go)
                go.SetActive(false);
        }
        GM.EditCharacter(this, Current_Char, Current_CharSave);
    }


    public void TurnOn()
    {
        SquareMarker.SetActive(true);
        foreach (GameObject go in CharacterModels)
        {
            go.SetActive(true);
        }
        Refresh();
    }

    public void TurnOff()
    {
        SquareMarker.SetActive(false);
        gameObject.SetActive(false);
    }

    public void TeamNameUpdate(string name)
    {
        Team.Name = name;
        FakeTeam.Name = name;
        TeamName.text = name;
    }

    public void OwnerNameUpdate(string name)
    {
        Team.Owner_Name = name;
        FakeTeam.Owner_Name = name;
        OwnerName.text = name;
    }
}
