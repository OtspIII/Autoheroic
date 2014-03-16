using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class GameplayTeamPickerController : MonoBehaviour {

    List<Cub.Tool.Team> Teams;
    List<UIPopupList> Selectors = new List<UIPopupList>();
    Dictionary<string, Team> TeamDictionary = new Dictionary<string, Team>();
    UIPopupList SelOne;
    UIPopupList SelTwo;
    UIButton ReadyOne;
    UIButton ReadyTwo;
    bool T1Ready = false;
    bool T2Ready = false;
    bool GameStarted = false;

	// Use this for initialization
	void Start () {
        Teams = LoadTeams();
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team Picker One":
                    SelOne = (UIPopupList)child.gameObject.GetComponentInChildren(System.Type.GetType("UIPopupList"));
                    Selectors.Add(SelOne);
                    break;
                case "Team Picker Two":
                    SelTwo = (UIPopupList)child.gameObject.GetComponentInChildren(System.Type.GetType("UIPopupList"));
                    Selectors.Add(SelTwo);
                    break;
                case "Team One Ready":
                    ReadyOne = (UIButton)child.gameObject.GetComponent("UIButton");
                    break;
                case "Team Two Ready":
                    ReadyTwo = (UIButton)child.gameObject.GetComponent("UIButton");
                    break;
            }
        }
        List<string> teamNames = new List<string> { "-Select a Team-" };
        foreach (Team t in Teams)
        {
            string name = t.Name;
            while (teamNames.Contains(name))
                name += "+";
            teamNames.Add(name);
            TeamDictionary.Add(name, t);
        }
        foreach (UIPopupList pop in Selectors)
        {
            pop.items = teamNames;
            pop.value = teamNames[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStarted) return;
        if (T1Ready && SelOne.value == SelOne.items[0])
            TeamOneButton();
        if (T2Ready && SelTwo.value == SelTwo.items[0])
            TeamTwoButton();
        if (T1Ready && T2Ready)
            GameStart();
            
	}

    List<Team> LoadTeams()
    {
        string name = typeof(List<Team>).AssemblyQualifiedName;
        return (List<Team>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/TeamSaves.xml");
    }

    public void TeamOneButton()
    {
        if (T1Ready)
        {
            T1Ready = false;
            ReadyOne.defaultColor = Color.white;
        }
        else if (SelOne.value != SelOne.items[0])
        { 
            T1Ready = true;
            ReadyOne.defaultColor = Color.green;
        }
        ((UISprite)ReadyOne.GetComponent("UISprite")).color = ReadyOne.defaultColor;
    }

    public void TeamTwoButton()
    {
        if (T2Ready)
        {
            T2Ready = false;
            ReadyTwo.defaultColor = Color.white;
        }
        else if (SelTwo.value != SelTwo.items[0])
        {
            T2Ready = true;
            ReadyTwo.defaultColor = Color.green;
        }
        ((UISprite)ReadyTwo.GetComponent("UISprite")).color = ReadyTwo.defaultColor;
    }

    void GameStart()
    {
        GameStarted = true;
        Debug.Log("Game Starts");
    }
}
