using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class TeamPickerController : MonoBehaviour {

    InterfaceController IC;
    UIGrid Grid;
    public List<Team> Teams = new List<Team>();
    List<TeamEditorButton> Buttons = new List<TeamEditorButton>();
    public GameObject TeamButtonType;
    
    // Use this for initialization
	void Start () {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        Grid = (UIGrid)gameObject.GetComponentInChildren(System.Type.GetType("UIGrid"));
        Teams = LoadTeams();
        BuildButtons();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuildButtons()
    {
        foreach (Transform tran in Grid.transform)
            if (tran.gameObject.name == "Team Button(Clone)")
                DestroyObject(tran.gameObject);
        int n = 0;
        foreach (Team team in Teams)
        {
            TeamEditorButton teb = (TeamEditorButton)NGUITools.AddChild(Grid.gameObject, TeamButtonType).GetComponent("TeamEditorButton");
            Buttons.Add(teb);
            teb.Setup(n);
            n++;
        }
        Grid.repositionNow = true;
    }

    List<Team> LoadTeams()
    {
        Assets.Scripts.Interface.TempArmyBuilder tab = new Assets.Scripts.Interface.TempArmyBuilder();
        return new List<Team> { tab.RedTeam, tab.BlueTeam };
    }

    public void AddNewTeam()
    {
        TeamEditorButton teb = (TeamEditorButton)NGUITools.AddChild(Grid.gameObject, TeamButtonType).GetComponent("TeamEditorButton");
        Buttons.Add(teb);

        Team team = new Team();
        Teams.Add(team);

        teb.Setup(Teams.Count - 1);

        Grid.repositionNow = true;
    }
}
