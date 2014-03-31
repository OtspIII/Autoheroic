using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public static class Library
    {
        private static bool Trigger = true;

        private static GameObject Prefab_Cube { get; set; }
        private static GameObject Prefab_Bullet { get; set; }
        private static GameObject Prefab_Character { get; set; }

        private static Cub.View.Character_Model Dictionary_Character_Model { get; set; }

        private static Dictionary<Bodypart_Head, Cub.View.Cubon> Dictionary_Bodypart_Head { get; set; }
        private static Dictionary<Bodypart_Body, Cub.View.Cubon> Dictionary_Bodypart_Body { get; set; }
        private static Dictionary<Bodypart_Arms, Cub.View.Cubon> Dictionary_Bodypart_Arms_Left { get; set; }
        private static Dictionary<Bodypart_Arms, Cub.View.Cubon> Dictionary_Bodypart_Arms_Right { get; set; }
        private static Dictionary<Bodypart_Legs, Cub.View.Cubon> Dictionary_Bodypart_Legs_Left { get; set; }
        private static Dictionary<Bodypart_Legs, Cub.View.Cubon> Dictionary_Bodypart_Legs_Right { get; set; }

        private static Dictionary<Cub.Event, Cub.View.Event.Base> Dictionary_Event { get; set; }

        private static Dictionary<string, AudioClip> Dictionary_Sound { get; set; }

        private static Dictionary<Cub.Terrain, GameObject> Dictionary_Terrain = new Dictionary<Terrain, GameObject>();

        public static void Initialization()
        {
            if (Trigger)
            {
                Prefab_Cube = Resources.Load<GameObject>("Prefabs/Cube");
                Prefab_Cube.renderer.material = Resources.Load<Material>("Materials/Transparent");

                Prefab_Character = Resources.Load<GameObject>("Prefabs/Character");
                Prefab_Bullet = Resources.Load<GameObject>("Prefabs/Bullet");

                Dictionary_Terrain.Add(Cub.Terrain.Desert, Resources.Load<GameObject>("Prefabs/Terrains/Desert"));
                Dictionary_Terrain.Add(Cub.Terrain.Grass, Resources.Load<GameObject>("Prefabs/Terrains/Grass"));

                Dictionary_Character_Model = new Character_Model();
                Dictionary_Character_Model = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/View_Model_Character.xml") as Cub.View.Character_Model;

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
                Dictionary_Event[Cub.Event.TimeOut] = new Cub.View.Event.TimeOut();

                Dictionary_Sound = new Dictionary<string, AudioClip>();
                Dictionary_Sound.Add("Scream", (AudioClip)Resources.Load<AudioClip>("Sounds/Scream"));

                Dictionary_Bodypart_Head = new Dictionary<Bodypart_Head, Cubon>();
                Dictionary_Bodypart_Head.Add(Bodypart_Head.Soldier, Cub.Tool.Xml.Deserialize(typeof(Cub.View.Cubon), "Data/View_Model_Bodypart_Head_Solider.xml") as Cub.View.Cubon);

                Dictionary_Bodypart_Body = new Dictionary<Bodypart_Body, Cubon>();
                Dictionary_Bodypart_Body.Add(Bodypart_Body.Medium, Cub.Tool.Xml.Deserialize(typeof(Cub.View.Cubon), "Data/View_Model_Bodypart_Body_Medium.xml") as Cub.View.Cubon);

                Dictionary_Bodypart_Arms_Left = new Dictionary<Bodypart_Arms, Cubon>();
                Dictionary_Bodypart_Arms_Left.Add(Bodypart_Arms.Rifle, Cub.Tool.Xml.Deserialize(typeof(Cub.View.Cubon), "Data/View_Model_Bodypart_Arms_Left_Rifle.xml") as Cub.View.Cubon);

                Dictionary_Bodypart_Arms_Right = new Dictionary<Bodypart_Arms, Cubon>();
                Dictionary_Bodypart_Arms_Right.Add(Bodypart_Arms.Rifle, Cub.Tool.Xml.Deserialize(typeof(Cub.View.Cubon), "Data/View_Model_Bodypart_Arms_Right_Rifle.xml") as Cub.View.Cubon);

                Dictionary_Bodypart_Legs_Left = new Dictionary<Bodypart_Legs, Cubon>();
                Dictionary_Bodypart_Legs_Left.Add(Bodypart_Legs.Hover, Cub.Tool.Xml.Deserialize(typeof(Cub.View.Cubon), "Data/View_Model_Bodypart_Legs_Left_Hover.xml") as Cub.View.Cubon);

                Dictionary_Bodypart_Legs_Right = new Dictionary<Bodypart_Legs, Cubon>();
                Dictionary_Bodypart_Legs_Right.Add(Bodypart_Legs.Hover, Cub.Tool.Xml.Deserialize(typeof(Cub.View.Cubon), "Data/View_Model_Bodypart_Legs_Right_Hover.xml") as Cub.View.Cubon);

                Trigger = false;
            }
        }

        public static void Unlock()
        {
            Trigger = true;
        }


        public static Cub.View.Character_Model Get_Character_Model()
        {
            return Dictionary_Character_Model;
        }

        public static Cub.View.Event.Base Get_Event(Cub.Event _E)
        {
            if (!Dictionary_Event.ContainsKey(_E))
                Debug.Log(_E.ToString());
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

        public static GameObject Get_Terrain(Cub.Terrain t)
        {
            if (Dictionary_Terrain.ContainsKey(t))
                return Dictionary_Terrain[t];
            return null;
        }

        public static GameObject Get_Bullet()
        {
            return Prefab_Bullet;
        }

        public static AudioClip Get_Sound(string name)
        {
            if (Dictionary_Sound.ContainsKey(name))
                return Dictionary_Sound[name];
            return null;
        }

        public static Cubon Get_Bodypart_Head(Bodypart_Head _Head)
        {
            if (Dictionary_Bodypart_Head.ContainsKey(_Head))
            {
                return Dictionary_Bodypart_Head[_Head];
            }
            return null;
        }

        public static Cubon Get_Bodypart_Body(Bodypart_Body _Body)
        {
            if (Dictionary_Bodypart_Body.ContainsKey(_Body))
            {
                return Dictionary_Bodypart_Body[_Body];
            }
            return null;
        }

        public static Cubon Get_Bodypart_Arms_Left(Bodypart_Arms _Arms)
        {
            if (Dictionary_Bodypart_Arms_Left.ContainsKey(_Arms))
            {
                return Dictionary_Bodypart_Arms_Left[_Arms];
            }
            return null;
        }

        public static Cubon Get_Bodypart_Arms_Right(Bodypart_Arms _Arms)
        {
            if (Dictionary_Bodypart_Arms_Right.ContainsKey(_Arms))
            {
                return Dictionary_Bodypart_Arms_Right[_Arms];
            }
            return null;
        }

        public static Cubon Get_Bodypart_Legs_Left(Bodypart_Legs _Legs)
        {
            if (Dictionary_Bodypart_Legs_Left.ContainsKey(_Legs))
            {
                return Dictionary_Bodypart_Legs_Left[_Legs];
            }
            return null;
        }

        public static Cubon Get_Bodypart_Legs_Right(Bodypart_Legs _Legs)
        {
            if (Dictionary_Bodypart_Legs_Right.ContainsKey(_Legs))
            {
                return Dictionary_Bodypart_Legs_Right[_Legs];
            }
            return null;
        }
    }
}
