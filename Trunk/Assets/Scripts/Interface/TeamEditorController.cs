using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamEditorController : MonoBehaviour {

    InterfaceController IC;
    UIInput TeamName;
    UIInput OwnerName;
    UILabel NumChars;
    UILabel TotalPts;
    UIGrid CLGrid;
    public CharEditorManager CharEditor;
    GameObject CharList;
    public GameObject CharButtonType;
    List<CharacterButtonController> CButtons = new List<CharacterButtonController>();
    Cub.Tool.Team Team = null;
    TeamEditorButton TeamButton = null;

	// Use this for initialization
    void Awake()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team Name":
                    TeamName = (UIInput)child.gameObject.GetComponent("UIInput");
                    break;
                case "Owner Name":
                    OwnerName = (UIInput)child.gameObject.GetComponent("UIInput");
                    break;
                case "Num Chars":
                    NumChars = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Total Points":
                    TotalPts = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Char List":
                    CharList = child.gameObject;
                    CLGrid = (UIGrid)CharList.gameObject.GetComponentInChildren(System.Type.GetType("UIGrid"));
                    break;
                case "Char Editor":
                    CharEditor = (CharEditorManager)child.gameObject.GetComponent("CharEditorManager");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ImprintTeam(Cub.Tool.Team team, TeamEditorButton button)
    {
        TeamButton = button;
        foreach (Transform tran in CLGrid.transform)
            if (tran.gameObject.name == "CharacterBox(Clone)")
                DestroyObject(tran.gameObject);
        Team = team;
        TeamName.value = Team.Name;
        OwnerName.value = Team.Owner_Name;
        System.Collections.Generic.List<Cub.Tool.Character> chars = team.Return_List_Character();
        NumChars.text = chars.Count.ToString() + " Characters";
        int pts = Team.TotalValue;
        TotalPts.text = pts.ToString() + "pts (" + (1000 - pts).ToString() + "pts left)";
        int n = 0;
        foreach (Cub.Tool.Character c in chars)
        {
            CharacterButtonController cbc = (CharacterButtonController)NGUITools.AddChild(CLGrid.gameObject, CharButtonType).GetComponent("CharacterButtonController");
            CButtons.Add(cbc);
            cbc.Imprint(n, c);
            n++;
        }
        CLGrid.Reposition();
        //Find Grid
        //foreach (Transform child in CharList.transform)
        //    if (child.gameObject.name == "CharacterBox")
        //        ((CharacterButtonController)child.gameObject.GetComponent("CharacterButtonController")).Imprint(chars);
        //CharEditor.Imprint(null);
        CLGrid.repositionNow = true;
    }

    public void CloseWindow()
    {
        IC.TeamPicker.gameObject.SetActive(true);
        IC.TeamEditor.gameObject.SetActive(false);
    }

    public void SaveButton()
    {
        Debug.Log("SAVE STUFF");
        IC.TeamPicker.gameObject.SetActive(true);
        IC.TeamEditor.gameObject.SetActive(false);
    }

    public void UpdateTeamName()
    {
        Team.Name = TeamName.value;
        TeamButton.UpdateNames();
    }

    public void UpdateOwnerName()
    {
        Team.Owner_Name = OwnerName.value;
        TeamButton.UpdateNames();
    }

    public void AddNewCharacter()
    {
        Debug.Log("A");
    }
}
