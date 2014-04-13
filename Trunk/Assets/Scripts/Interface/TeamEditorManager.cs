using UnityEngine;
using System.Collections;
using Cub.Model;
using System.Collections.Generic;

public class TeamEditorManager : MonoBehaviour {

    public GameObject SquareMarker;
    Cub.Position2 SelectedSquare;
    public MasterGameController GM;
    public bool CurrentlyActive = false;
    bool Clicking = false;

    private Dictionary<System.Guid, Cub.View.Character> Dictionary_Character { get; set; }
    private Dictionary<System.Guid, Cub.Model.Character_Save> Dictionary_CharSave { get; set; }

    TeamSave Team = null;
    Team FakeTeam = null;

    float SelectTimer = 0.2f;
    float VertTimer;
    float HoriTimer;
    public bool PlayerOne;
    public bool PlayerTwo;
    public Rect SelectRange;

	// Use this for initialization
	void Start () {
        GM = (MasterGameController)GameObject.Find("Game Master").GetComponent("MasterGameController");
	}
	
	// Update is called once per frame
	void Update () {
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
        else
            Clicking = false;
	}

    public void Setup(Cub.Model.TeamSave team )
    {
        Team = team;
        SelectedSquare = new Cub.Position2((int)SelectRange.x, (int)SelectRange.y);
        MoveSelector();
        Dictionary_Character = new Dictionary<System.Guid, Cub.View.Character>();
        Dictionary_CharSave = new Dictionary<System.Guid, Character_Save>();
        FakeTeam = Team.Extract_Team();
        Vector3 Rot = new Vector3(0,270,0);
        if (PlayerOne)
            Rot = new Vector3(0,90, 0);
        foreach (Character c in FakeTeam.List_Character)
        {
            c.Stat.Position = TranslateStartPosition(c.Stat.Position, PlayerOne);
            Cub.View.Character C = Cub.View.Runtime.Add_Character(c);

            Dictionary_Character[c.ID] = C;
            foreach (Character_Save cs in Team.Chars)
                if (cs.ID == c.ID_Save)
                    Dictionary_CharSave[c.ID] = cs;
            C.transform.rotation = Quaternion.Euler(Rot);
        }
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

    void SlideSelector(Cub.Position2 move)
    {
        int x = Mathf.Min((int)(SelectRange.x + SelectRange.width),Mathf.Max((int)(SelectRange.x),SelectedSquare.X + move.X));
        int y = Mathf.Min((int)(SelectRange.y + SelectRange.height), Mathf.Max((int)(SelectRange.y), SelectedSquare.Y + move.Y));
        SelectedSquare = new Cub.Position2(x,y);
        MoveSelector();
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

    }
}
