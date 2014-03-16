using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class GameplayTeamPickerController : MonoBehaviour {

    List<Cub.Tool.Team> Teams;
    List<UIPopupList> Selectors = new List<UIPopupList>();
    Dictionary<string, Team> TeamDictionary = new Dictionary<string, Team>();

	// Use this for initialization
	void Start () {
        Teams = LoadTeams();
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team Picker":
                    Selectors.Add((UIPopupList)child.gameObject.GetComponentInChildren(System.Type.GetType("UIPopupList")));
                    break;
            }
        }
        List<string> teamNames = new List<string>();
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
            if (teamNames.Count > 0)
                pop.value = teamNames[0];
            else
                pop.value = "No Teams Yet Created";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    List<Team> LoadTeams()
    {
        string name = typeof(List<Team>).AssemblyQualifiedName;
        return (List<Team>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/TeamSaves.xml");
    }
}
