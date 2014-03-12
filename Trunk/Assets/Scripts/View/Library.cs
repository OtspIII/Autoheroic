using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public static class Library
    {
        private static bool Trigger = true;

        private static GameObject Prefab_Cube { get; set; }
        private static GameObject Prefab_Character { get; set; }

        private static Dictionary<Cub.Class, Cub.View.Character_Model> Dictionary_Character_Model { get; set; }
        private static Dictionary<Cub.Event, Cub.View.Event_Processor.Base> Dictionary_Event_Processor { get; set; }

        public static void Initialization()
        {
            if (Trigger)
            {
                Prefab_Cube = Resources.Load<GameObject>("Prefabs/Cube");
                Prefab_Character = Resources.Load<GameObject>("Prefabs/Character");

                Dictionary_Character_Model = new Dictionary<Class, Character_Model>();
                Dictionary_Character_Model[Class.Knight] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Model_Character_Knight.xml") as Cub.View.Character_Model;

                Dictionary_Event_Processor = new Dictionary<Cub.Event, Event_Processor.Base>();
                Dictionary_Event_Processor[Cub.Event.Move] = new Cub.View.Event_Processor.Move();

                Trigger = false;
            }
        }

        public static void Unlock()
        {
            Trigger = true;
        }

        public static Cub.View.Character_Model Get_Character_Model(Cub.Class _C)
        {
            return Dictionary_Character_Model[_C];
        }

        public static Cub.View.Event_Processor.Base Get_Event_Processor(Cub.Event _E)
        {
            return Dictionary_Event_Processor[_E];
        }

        public static GameObject Get_Cube()
        {
            return Prefab_Cube;
        }

        public static GameObject Get_Character()
        {
            return Prefab_Character;
        }
    }
}
