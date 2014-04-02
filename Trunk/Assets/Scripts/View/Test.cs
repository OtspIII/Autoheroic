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

            Cub.Model.Character C = new Model.Character();

            C.Name = "fdsa";
            C.ID = System.Guid.NewGuid();
            C.Info = new Model.Character_Info();
            C.Info.Head = Part_Head.Soldier;
            C.Info.Body = Part_Body.Medium;
            C.Info.Arms = Part_Arms.Rifle;
            C.Info.Legs = Part_Legs.Tread;

            C.Stat = new Model.Character_Stat();
            C.Stat.Position = new Position2(0, 0);
            C.Stat.Team = new Model.Team();
            
            Cub.View.Runtime.Add_Character(C);
        

            /*
            List<Cub.View.Cubon> C = new List<View.Cubon>();
            C.Add(new View.Cubon(Colour.Black, new List<Position3>() { new Position3(0, 0, 0) }));

            Cub.Tool.Xml.Serialize(C, "Data/View_Part_Legs_Right_Hover.xml");*/
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("fdsa");

                GameObject.Find("Character(Clone)").transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Attack_Range");

                /*
                foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Cube"))
                {
                    DestroyImmediate(GO);
                }

                Start();*/
            }
        }
    }
}
