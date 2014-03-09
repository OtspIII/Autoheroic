﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class CharEditorManager : MonoBehaviour {

    UIInput Name;
    UILabel Class;
    UILabel Cost;
    UILabel HP;
    UILabel Range;
    UILabel Speed;
    UIGrid Grid;
    List<TacticBoxController> Tactics = new List<TacticBoxController>();
    public GameObject TacticBoxType;
    Cub.Tool.Character Who;
    
    void Awake()
    {
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Character Name":
                    Name = (UIInput)child.gameObject.GetComponent("UIInput");
                    break;
                case "Class Name":
                    Class = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Points Total":
                    Cost = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "HPReadout":
                    HP = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "RangeReadout":
                    Range = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "SpeedReadout":
                    Speed = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Tactics List":
                    //foreach (Transform tac in child.gameObject.transform)
                    //{
                    //    if (tac.gameObject.name == "AI Panel")
                    //        Tactics.Add((TacticBoxController)tac.gameObject.GetComponent("TacticBoxController"));
                    //}
                    break;


                //case "AI Panel":
                //    Tactics.Add((TacticBoxController)child.gameObject.GetComponent("TacticBoxController"));
                //    break;
            }
        }
        Grid = (UIGrid)gameObject.GetComponentInChildren(System.Type.GetType("UIGrid"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(Cub.Tool.Character who)
    {
        foreach (Transform tran in Grid.transform)
            if (tran.gameObject.name == "AI Panel(Clone)")
                DestroyObject(tran.gameObject);
        Tactics = new List<TacticBoxController>();
        if (who != null)
        {
            Who = who;
            Name.value = who.Name;
            Class.text = who.Info.Class.ToString();
            Cost.text = who.Value.ToString() + "pts";
            HP.text = who.Info.MHP.ToString();
            Range.text = who.Info.Range.ToString();
            Speed.text = who.Info.Speed.ToString();
            int n = 0;
            foreach (Tactic tac in who.Tactics)
            {
                TacticBoxController tbc = (TacticBoxController)NGUITools.AddChild(Grid.gameObject, TacticBoxType).GetComponent("TacticBoxController");
                Tactics.Add(tbc);
                tbc.Imprint(n, who, who.Tactics[n]);
                n++;
            }
        }
        else
        {
            Who = null;
            Name.value = "---";
            Class.text = "";
            Cost.text = "";
            HP.text = "-";
            Range.text = "-";
            Speed.text = "-";
        }
        Grid.repositionNow = true;
        //Grid.Reposition();
    }

    public void AddEmptyTactic()
    {
        if (Who == null) return;
        TacticBoxController tbc = (TacticBoxController)NGUITools.AddChild(Grid.gameObject, TacticBoxType).GetComponent("TacticBoxController");
        Tactics.Add(tbc);
        Tactic tac = new Tactic(Cub.Condition.Any, Cub.Action.Explore, new List<object>());
        Who.Bought_Tactic.Add(tac);
        tbc.Imprint(Tactics.Count - 1, Who, tac);
        Grid.repositionNow = true;
    }
}
