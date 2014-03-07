﻿using UnityEngine;
using System.Collections;

public class TeamEditorController : MonoBehaviour {

    InterfaceController IC;
    UIInput TeamName;
    UIInput OwnerName;
    UILabel NumChars;
    UILabel TotalPts;
    public CharEditorManager CharEditor;
    GameObject CharList;

    Cub.Tool.Team Team = null;

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

    public void ImprintTeam(Cub.Tool.Team team)
    {
        Team = team;
        TeamName.value = Team.Name;
        OwnerName.value = Team.Owner_Name;
        System.Collections.Generic.List<Cub.Tool.Character> chars = team.Return_List_Character();
        NumChars.text = chars.Count.ToString() + " Characters";
        int pts = Team.TotalValue;
        TotalPts.text = pts.ToString() + "pts (" + (1000 - pts).ToString() + "pts left)";
        foreach (Transform child in CharList.transform)
            if (child.gameObject.name == "CharacterBox")
                ((CharacterButtonController)child.gameObject.GetComponent("CharacterButtonController")).Imprint(chars);
    }
}