using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Scene
{
    public class Test : MonoBehaviour
    {
        public void Start()
        {
            Cub.View.Library.Initialization();

            Cub.Model.Character C = new Model.Character();

            C.Name = "fdsa";
            C.ID = System.Guid.NewGuid();
            C.Info = new Model.Character_Info();
            C.Info.Head = Bodypart_Head.Soldier;
            C.Info.Body = Bodypart_Body.Medium;
            C.Info.Arms = Bodypart_Arms.Rifle;
            C.Info.Legs = Bodypart_Legs.Hover;

            C.Stat = new Model.Character_Stat();
            C.Stat.Position = new Position2(0, 0);
            C.Stat.Team = new Model.Team();
            
            Cub.View.Runtime.Add_Character(C);
        }
    }
}
