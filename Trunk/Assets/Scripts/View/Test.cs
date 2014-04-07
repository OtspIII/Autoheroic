using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Scene
{
    public class Test : MonoBehaviour
    {
        public void Start()
        {
            Cub.View.Library.Unlock();

            Cub.View.Library.Initialization();

            Cub.Model.Character C0 = new Model.Character();
            C0.Name = "fdsa";
            C0.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            C0.Info = new Model.Character_Info();
            C0.Info.Head = Part_Head.Soldier;
            C0.Info.Body = Part_Body.Medium;
            C0.Info.Arms = Part_Arms.Rifle;
            C0.Info.Legs = Part_Legs.Hover;
            C0.Stat = new Model.Character_Stat();
            C0.Stat.Position = new Position2(0, 0);
            C0.Stat.Team = new Model.Team();

            Cub.View.Runtime.Add_Character(C0);

            Cub.Model.Character C1 = new Model.Character();
            C1.Name = "fdsa";
            C1.ID = new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1);
            C1.Info = new Model.Character_Info();
            C1.Info.Head = Part_Head.Soldier;
            C1.Info.Body = Part_Body.Medium;
            C1.Info.Arms = Part_Arms.Rifle;
            C1.Info.Legs = Part_Legs.Hover;
            C1.Stat = new Model.Character_Stat();
            C1.Stat.Position = new Position2(0, 3);
            C1.Stat.Team = new Model.Team();

            Cub.View.Runtime.Add_Character(C1);


            /*
            List<Cub.View.Cubon> C = new List<View.Cubon>();
            C.Add(new View.Cubon(Colour.Black, new List<Position3>() { new Position3(0, 0, 0) }));

            Cub.Tool.Xml.Serialize(C, "Data/View_Part_Legs_Right_Hover.xml");*/
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {                
                Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Range, "", new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), 0, 0 }));
                GameObject.Find("Runtime").SendMessage("Run_Eventon", SendMessageOptions.DontRequireReceiver);
                
                /*
                */
                /*
                foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Cube"))
                {
                    DestroyImmediate(GO);
                }

                Start();*/
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Move, "", new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), 0, 0 }));
                GameObject.Find("Runtime").SendMessage("Run_Eventon", SendMessageOptions.DontRequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Attacked, "", new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), 0, 0 }));
                GameObject.Find("Runtime").SendMessage("Run_Eventon", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
