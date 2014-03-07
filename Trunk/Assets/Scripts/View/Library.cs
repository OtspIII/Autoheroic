using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public static class Library
    {
        private static bool Trigger = true;

        private static GameObject Prefab_Cube { get; set; }

        private static Dictionary<Cub.Class, Cub.View.Character_Model> Dictionary_Character_Model { get; set; }

        private static Dictionary<Cub.Class, GameObject> Dictionary_Character { get; set; }

        public static void Initialization()
        {
            if (Trigger)
            {
                Prefab_Cube = Resources.Load<GameObject>("Prefabs/Cube");

                Dictionary_Character_Model = new Dictionary<Class, Character_Model>();
                Dictionary_Character = new Dictionary<Class, GameObject>();

                Dictionary_Character_Model[Class.Knight] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Model_Character_Knight.xml") as Cub.View.Character_Model;
                Dictionary_Character[Class.Knight] = Resources.Load<GameObject>("Prefabs/Classes/Knight");

                Trigger = false;
            }
        }

        public static void Yo()
        {
            Trigger = true;
        }

        public static Cub.View.Character_Model Get_Character_Model(Cub.Class _C)
        {
            return Dictionary_Character_Model[_C];
        }

        public static GameObject Get_Cube()
        {
            return Prefab_Cube;
        }

        public static GameObject Get_Character(Cub.Class _C)
        {
            return Dictionary_Character[_C];
        }
    }
}
