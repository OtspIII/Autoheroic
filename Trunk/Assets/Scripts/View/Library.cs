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
        private static Dictionary<Cub.Event, Cub.View.Event.Base> Dictionary_Event { get; set; }

        public static void Initialization()
        {
            if (Trigger)
            {
                Prefab_Cube = Resources.Load<GameObject>("Prefabs/Cube");
                Prefab_Cube.renderer.material = Resources.Load<Material>("Materials/Transparent");

                Prefab_Character = Resources.Load<GameObject>("Prefabs/Character");

                Dictionary_Character_Model = new Dictionary<Class, Character_Model>();
                Dictionary_Character_Model[Class.Knight] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Knight.xml") as Cub.View.Character_Model;

                Dictionary_Event = new Dictionary<Cub.Event, Event.Base>();
                Dictionary_Event[Cub.Event.Attack_Heal] = new Cub.View.Event.Attack_Heal();
                Dictionary_Event[Cub.Event.Attack_Melee] = new Cub.View.Event.Attack_Melee();
                Dictionary_Event[Cub.Event.Attack_Range] = new Cub.View.Event.Attack_Range();
                Dictionary_Event[Cub.Event.Attack_Rocket] = new Cub.View.Event.Attack_Rocket();
                Dictionary_Event[Cub.Event.Attack_Snipe] = new Cub.View.Event.Attack_Snipe();
                Dictionary_Event[Cub.Event.Be_Attacked] = new Cub.View.Event.Be_Attacked();
                Dictionary_Event[Cub.Event.Be_Healed] = new Cub.View.Event.Be_Healed();
                Dictionary_Event[Cub.Event.Die] = new Cub.View.Event.Die();
                Dictionary_Event[Cub.Event.Idle] = new Cub.View.Event.Idle();
                Dictionary_Event[Cub.Event.Move] = new Cub.View.Event.Move();
                Dictionary_Event[Cub.Event.Win] = new Cub.View.Event.Win();
                
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

        public static Cub.View.Event.Base Get_Event_Processor(Cub.Event _E)
        {
            return Dictionary_Event[_E];
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
