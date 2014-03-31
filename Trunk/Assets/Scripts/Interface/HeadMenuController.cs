﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadMenuController : MonoBehaviour
{

    Cub.Tool.Character_Save Who;
    InterfaceController IC;

    UIPopupList Options;
    UILabel Cost;
    UILabel Effects;
    UILabel Desc;
    UISprite Portrait;

    // Use this for initialization
    void Start()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Body Picker":
                    Options = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                case "Cost Label":
                    Cost = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Effects Label":
                    Effects = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Description Label":
                    Desc = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Portrait":
                    Portrait = (UISprite)child.gameObject.GetComponent("UISprite");
                    break;
            }
        }
        Options.items = new List<string> { };
        foreach (Cub.Model.Bodypart bp in Cub.Tool.Library.List_Heads())
            Options.items.Add(bp.Name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Imprint(Cub.Tool.Character_Save who)
    {
        Who = who;
        Cub.Model.Bodypart H = who.Head_Part;
        if (Options.value != H.Name)
            Options.value = H.Name;
        Cost.text = "Cost: " + H.Cost.ToString() + "pts";
        Effects.text = "Effects: --";
        Desc.text = H.Description;
    }

    public void UpdateBodypart()
    {
        if (Who == null) return;
        Cub.Bodypart_Head current = Who.Head;
        string bp = Options.value;
        Who.Head = Cub.Tool.Library.Get_Head(bp);
        //Imprint(Who);
        IC.TeamEditor.Refresh();
        IC.TeamEditor.CharEditor.Refresh();
    }
}