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
        private static GameObject Prefab_Rocket { get; set; }
        private static GameObject Prefab_Explosion { get; set; }
        private static GameObject Prefab_Healer { get; set; }

        private static Dictionary<Part_Head, List<Cub.View.Cubon>> Dictionary_Part_Head { get; set; }
        private static Dictionary<Part_Body, List<Cub.View.Cubon>> Dictionary_Part_Body { get; set; }
        private static Dictionary<Part_Arms, List<Cub.View.Cubon>> Dictionary_Part_Arms_Left { get; set; }
        private static Dictionary<Part_Arms, List<Cub.View.Cubon>> Dictionary_Part_Arms_Right { get; set; }
        private static Dictionary<Part_Legs, List<Cub.View.Cubon>> Dictionary_Part_Legs_Left { get; set; }
        private static Dictionary<Part_Legs, List<Cub.View.Cubon>> Dictionary_Part_Legs_Right { get; set; }

        private static Material Dictionary_Material { get; set; }

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
                Prefab_Rocket = Resources.Load<GameObject>("Prefabs/Rocket");
                Prefab_Explosion = Resources.Load<GameObject>("Prefabs/Explosion");
                Prefab_Healer = Resources.Load<GameObject>("Prefabs/Healer");

                Dictionary_Terrain = new Dictionary<Terrain, GameObject>();
                Dictionary_Terrain.Add(Cub.Terrain.Desert, Resources.Load<GameObject>("Prefabs/Terrains/Desert"));
                Dictionary_Terrain.Add(Cub.Terrain.Grass, Resources.Load<GameObject>("Prefabs/Terrains/Grass"));

                Dictionary_Event = new Dictionary<Cub.Event, Event.Base>();
                Dictionary_Event[Cub.Event.Attack_Heal] = new Cub.View.Event.Attack_Heal();
                Dictionary_Event[Cub.Event.Attack_Harm] = new Cub.View.Event.Attack_Harm();
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
                Dictionary_Event[Cub.Event.BlowUp] = new Cub.View.Event.BlowUp();

                Dictionary_Sound = new Dictionary<string, AudioClip>();
                Dictionary_Sound.Add("Scream", (AudioClip)Resources.Load<AudioClip>("Sounds/Scream"));

                Dictionary_Part_Head = new Dictionary<Part_Head, List<Cubon>>();
                Dictionary_Part_Head.Add(Part_Head.Soldier, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Solider.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Head.Add(Part_Head.Hunter, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Hunter.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Head.Add(Part_Head.Idiot, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Idiot.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Head.Add(Part_Head.Protector, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Protector.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Body = new Dictionary<Part_Body, List<Cubon>>();
                Dictionary_Part_Body.Add(Part_Body.Light, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Light.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Medium, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Medium.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Heavy, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Heavy.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Bomber, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Bomber.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Healer, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Healer.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Arms_Left = new Dictionary<Part_Arms, List<Cubon>>();
                Dictionary_Part_Arms_Left.Add(Part_Arms.Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Sword, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Sword.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Axe, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Axe.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Pistol, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Pistol.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Sniper_Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Sniper_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.RPG, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_RPG.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Heal_Gun, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Heal_Gun.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Arms_Right = new Dictionary<Part_Arms, List<Cubon>>();
                Dictionary_Part_Arms_Right.Add(Part_Arms.Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Sword, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Sword.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Axe, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Axe.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Pistol, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Pistol.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Sniper_Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Sniper_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.RPG, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_RPG.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Heal_Gun, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Heal_Gun.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Legs_Left = new Dictionary<Part_Legs, List<Cubon>>();
                Dictionary_Part_Legs_Left.Add(Part_Legs.Hover, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Left_Hover.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Left.Add(Part_Legs.Humanoid, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Left_Humanoid.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Left.Add(Part_Legs.Tread, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Left_Tread.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Legs_Right = new Dictionary<Part_Legs, List<Cubon>>();
                Dictionary_Part_Legs_Right.Add(Part_Legs.Hover, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Right_Hover.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Right.Add(Part_Legs.Humanoid, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Right_Humanoid.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Right.Add(Part_Legs.Tread, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Right_Tread.xml") as List<Cub.View.Cubon>);

                Dictionary_Material = Resources.Load<Material>("Materials/Cube");

                Trigger = false;
            }
        }

        public static void Unlock()
        {
            Trigger = true;
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

        public static GameObject Get_Rocket()
        {
            return Prefab_Rocket;
        }

        public static GameObject Get_Explosion()
        {
            return Prefab_Explosion;
        }

        public static GameObject Get_Healer()
        {
            return Prefab_Healer;
        }

        public static AudioClip Get_Sound(string name)
        {
            if (Dictionary_Sound.ContainsKey(name))
                return Dictionary_Sound[name];
            return null;
        }

        public static List<Cubon> Get_Part_Head(Part_Head _Head)
        {
            if (Dictionary_Part_Head.ContainsKey(_Head))
            {
                return Dictionary_Part_Head[_Head];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Body(Part_Body _Body)
        {
            if (Dictionary_Part_Body.ContainsKey(_Body))
            {
                return Dictionary_Part_Body[_Body];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Arms_Left(Part_Arms _Arms)
        {
            if (Dictionary_Part_Arms_Left.ContainsKey(_Arms))
            {
                return Dictionary_Part_Arms_Left[_Arms];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Arms_Right(Part_Arms _Arms)
        {
            if (Dictionary_Part_Arms_Right.ContainsKey(_Arms))
            {
                return Dictionary_Part_Arms_Right[_Arms];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Legs_Left(Part_Legs _Legs)
        {
            if (Dictionary_Part_Legs_Left.ContainsKey(_Legs))
            {
                return Dictionary_Part_Legs_Left[_Legs];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Legs_Right(Part_Legs _Legs)
        {
            if (Dictionary_Part_Legs_Right.ContainsKey(_Legs))
            {
                return Dictionary_Part_Legs_Right[_Legs];
            }
            return null;
        }

        public static Material Get_Material()
        {
            return Dictionary_Material;
        }
    }
}
