using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticBoxController : MonoBehaviour {

    InterfaceController IC;
    Cub.Tool.Tactic Tactic = null;
    public int Number;
    UILabel NumLabel;
    UILabel ActionDesc;
    UILabel ConditionDesc;
    UILabel Between;
    UIPopupList ActionList;
    UIPopupList ConditionList;

	// Use this for initialization
    void Awake()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Number":
                    NumLabel = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Action Description":
                    ActionDesc = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Condition Description":
                    ConditionDesc = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Action List":
                    ActionList = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                case "Condition List":
                    ConditionList = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                case "Between Label":
                    Between = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(int n, Cub.Tool.Character who, Cub.Tool.Tactic tac)
    {
        Number = n;
        NumLabel.text = (Number + 1).ToString();
        if (who != null && who.Tactics.Count > Number)
        {
            Tactic = tac;
            Cub.Action oldAct = Tactic.A;
            ActionList.items = new List<string>{};
            List<Cub.Tool.Action.Base> acts = Cub.Tool.Library.List_Actions(who.Info.Class);
            foreach (Cub.Tool.Action.Base act in acts)
                ActionList.items.Add(act.Name);
            ActionList.value = Cub.Tool.Library.Get_Action(Tactic.A).Name;
            ConditionList.value = Cub.Tool.Library.Get_Condition(Tactic.C).Name;
        }
        else
        {
            Tactic = null;
            ActionDesc.text = "";
            ConditionDesc.text = "";
        }
    }

    public void NewActionSelected()
    {
        if (UIPopupList.current != null && Tactic != null)
        {
            string text = UIPopupList.current.isLocalized ?
                Localization.Get(UIPopupList.current.value) :
                UIPopupList.current.value;
            Tactic.SetAction(Cub.Tool.Library.String_Action(text));
            SetAction();
        }
    }

    public void SetAction()
    {
        if (Tactic == null) return;
        ActionDesc.text = Cub.Tool.Library.Get_Action(Tactic.A).Description;
        ConditionList.items = new List<string> { };
        List<Cub.Tool.Condition.Base> cons = Cub.Tool.Library.List_Conditions(Tactic.A);
        foreach (Cub.Tool.Condition.Base con in cons)
            ConditionList.items.Add(con.Name);
        if (!ConditionList.items.Contains(Cub.Tool.Library.Get_Condition(Tactic.C).Name))
        {
            Tactic.SetCondition(Cub.Condition.Any);
            ConditionList.value = Cub.Tool.Library.Get_Condition(Tactic.C).Name;
        }
    }

    public void SetCondition()
    {
        if (Tactic == null) return;
        ConditionDesc.text = Cub.Tool.Library.Get_Condition(Tactic.C).Description;
    }

    public void NewConditionSelected()
    {
        if (UIPopupList.current != null && Tactic != null)
        {
            string text = UIPopupList.current.isLocalized ?
                Localization.Get(UIPopupList.current.value) :
                UIPopupList.current.value;
            Tactic.SetCondition(Cub.Tool.Library.String_Condition(text));
            //if (Tactic.C == Cub.Condition.None) return;
            SetCondition();
        }
    }
}
