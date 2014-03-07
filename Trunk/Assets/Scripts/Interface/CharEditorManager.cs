using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharEditorManager : MonoBehaviour {

    UIInput Name;
    UILabel Class;
    UILabel Cost;
    UILabel HP;
    UILabel Range;
    UILabel Speed;
    List<TacticBoxController> Tactics = new List<TacticBoxController>();
    
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
                case "AI Panel":
                    Tactics.Add((TacticBoxController)child.gameObject.GetComponent("TacticBoxController"));
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(Cub.Tool.Character who)
    {
        Name.value = who.Name;
        Class.text = who.Info.Class.ToString();
        Cost.text = who.Value.ToString() + "pts";
        HP.text = who.Info.MHP.ToString();
        Range.text = who.Info.Range.ToString();
        Speed.text = who.Info.Speed.ToString();
        foreach (TacticBoxController tbc in Tactics)
            tbc.Imprint(who);
    }
}
