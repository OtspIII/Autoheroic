using UnityEngine;
using System.Collections;

public class TacticBoxController : MonoBehaviour {

    InterfaceController IC;
    Cub.Tool.Tactic Tactic = null;
    public int Number;
    UILabel NumLabel;
    UILabel ActionDesc;
    UILabel ConditionDesc;
    UILabel Between;

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
                case "Between Label":
                    Between = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(Cub.Tool.Character who)
    {
        NumLabel.text = (Number + 1).ToString();
        if (who.Tactics.Count <= Number)
            return;
        Tactic = who.Tactics[Number];
        Debug.Log("A: " + who.Name + " / " + Tactic.A + " / " + Cub.Tool.Library.Get_Action(Tactic.A) + " / " + Tactic.C + " / " + who.Tactics.Count.ToString());
        ActionDesc.text = Cub.Tool.Library.Get_Action(Tactic.A).Description;
        Debug.Log("B");
        ConditionDesc.text = Cub.Tool.Library.Get_Condition(Tactic.C).Description;
        Debug.Log("C");

    }
}
