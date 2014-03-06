using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterButtonController : MonoBehaviour {

    InterfaceController IC;
    Cub.Tool.Character Who = null;
    public int Number;
    UILabel NumTxt;
    UILabel Name;
    UILabel Class;
    UILabel Cost;

	// Use this for initialization
	void Awake () {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Character Class":
                    Class = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Character Name":
                    Name = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Character Points":
                    Cost = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Number":
                    NumTxt = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Imprint(List<Cub.Tool.Character> chars)
    {
        if (chars.Count > Number)
        {
            Cub.Tool.Character who = chars[Number];
            Who = who;
            Name.text = who.Name;
            Class.text = who.Info.Class.ToString();
            Cost.text = who.Value.ToString() + "pts";
        }
        else
        {
            Name.text = "-Empty-";
            Class.text = "--";
            Cost.text = "0pts";
        }
    }

    public void GetClicked()
    {
        IC.TeamEditor.CharEditor.Imprint(Who);
    }
}
