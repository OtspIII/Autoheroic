using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public static class Interface
    {


        public static void Initialize_Character(Cub.Tool.Character _Character)
        {
            GameObject GO = GameObject.Instantiate(Cub.View.Library.Get_Character(), _Character.Stat.Position.ToVector3(), Quaternion.identity) as GameObject;

        }

        public static void Push_Event(Cub.Event _Event)
        {
            
        }
        
    }
}
