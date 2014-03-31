using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Model
{
    public static class Library
    {
        private static bool Trigger = true;

        public static int PointCap = 600;

        private static Dictionary<Cub.Condition, Cub.Model.Condition.Base> Dictionary_Condition { get; set; }
        private static Dictionary<Cub.Action, Cub.Model.Action.Base> Dictionary_Action { get; set; }
        private static Dictionary<string, Cub.Condition> Condition_Strings { get; set; }
        private static Dictionary<string, Cub.Action> Action_Strings { get; set; }
        private static Dictionary<Cub.Class, Cub.Model.Character_Info> Dictionary_Class_Info { get; set; }

        private static Dictionary<Cub.Part_Head, Cub.Model.Bodypart> Dictionary_Heads { get; set; }
        private static Dictionary<Cub.Part_Arms, Cub.Model.Bodypart> Dictionary_Arms { get; set; }
        private static Dictionary<Cub.Part_Body, Cub.Model.Bodypart> Dictionary_Bodies { get; set; }
        private static Dictionary<Cub.Part_Legs, Cub.Model.Bodypart> Dictionary_Legs { get; set; }
        private static Dictionary<Cub.Part_Arms, Cub.Model.Weapon> Dictionary_Weapons { get; set; }

        public static Cub.Terrain[][] Stage_Terrain { get; set; }
        public static Cub.Class[][] Stage_Unit { get; set; }

        public static void Initialization()
        {
            if (Trigger)
            {
                Dictionary_Class_Info = new Dictionary<Cub.Class, Cub.Model.Character_Info>();
                Dictionary_Class_Info[Class.None] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_None.xml") as Cub.Model.Character_Info;
                Dictionary_Class_Info[Class.Soldier] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_Soldier.xml") as Cub.Model.Character_Info;
                Dictionary_Class_Info[Class.Knight] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_Knight.xml") as Cub.Model.Character_Info;
                Dictionary_Class_Info[Class.Rocket] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_Rocket.xml") as Cub.Model.Character_Info;
                Dictionary_Class_Info[Class.Sniper] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_Sniper.xml") as Cub.Model.Character_Info;
                Dictionary_Class_Info[Class.Jerk] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_Jerk.xml") as Cub.Model.Character_Info;
                Dictionary_Class_Info[Class.Medic] = Cub.Tool.Xml.Deserialize(typeof(Cub.Model.Character_Info), "Data/Character_Info_Medic.xml") as Cub.Model.Character_Info;

                Dictionary_Heads = new Dictionary<Part_Head, Model.Bodypart>();
                Dictionary_Heads.Add(Cub.Part_Head.Soldier, new Model.Bodypart("Soldier",
                    "An AI module that marches towards the enemy and attacks. Simple but effecive.", 0, 0, 25));
                Dictionary_Heads.Add(Cub.Part_Head.Idiot, new Model.Bodypart("Idiot",
                    "An incredibly cheap AI module that wanders the battlefield at random. At least it's hard to damage.", 1, 0, 10));
                Dictionary_Heads.Add(Cub.Part_Head.Protector, new Model.Bodypart("Protector",
                    "An AI module that chooses to follow teammates around rather than seek the enemy out itself.", 0, 0, 20));
                Dictionary_Heads.Add(Cub.Part_Head.Hunter, new Model.Bodypart("Hunter-Killer",
                    "This AI module seeks out the highest-value unit on the opposing team and targets it first.", 0, 0, 30));

                Dictionary_Arms = new Dictionary<Part_Arms, Model.Bodypart>();
                Dictionary_Arms.Add(Part_Arms.Rifle, new Model.Bodypart("Rifle",
                    "A basic rifle. The standard by which all other weapons are measured.", 0, 0, 25));
                Dictionary_Arms.Add(Part_Arms.Sword, new Model.Bodypart("Sword & Shield",
                    "A sword and shield, granting defense at the cost of an extremely short attack range.", 1, 0, 25,
                    new List<Cub.Action> { Cub.Action.Charge }));
                Dictionary_Arms.Add(Part_Arms.Axe, new Model.Bodypart("Axe",
                    "A heavy axe, dealing terrible damage at close range but offering no protection.", 0, 0, 25,
                    new List<Cub.Action> { Cub.Action.Charge }));
                Dictionary_Arms.Add(Part_Arms.Sniper_Rifle, new Model.Bodypart("Sniper Rifle",
                    "A sniper rifle that is only mildly effective unless it can pull off a head-shot.", 0, 0, 30,
                    new List<Cub.Action> { Cub.Action.Snipe }));
                Dictionary_Arms.Add(Part_Arms.Pistol, new Model.Bodypart("Pistol",
                    "A cheap pistol with meager range and damage.", 0, 0, 10));
                Dictionary_Arms.Add(Part_Arms.RPG, new Model.Bodypart("RPG",
                    "A rocket launcher that is hard to aim but does damage in an area around where it hits.", 0, 0, 35,
                    new List<Cub.Action> { Cub.Action.Missile }));
                Dictionary_Arms.Add(Part_Arms.Heal_Gun, new Model.Bodypart("Heal Gun",
                    "A heal-gun that can be used both to heal allies and to harm the enemy.", 0, 0, 25,
                    new List<Cub.Action> { Cub.Action.Heal }));

                Dictionary_Weapons = new Dictionary<Part_Arms, Model.Weapon>();
                Dictionary_Weapons.Add(Part_Arms.Rifle, new Model.Weapon(5));
                Dictionary_Weapons.Add(Part_Arms.Sword, new Model.Weapon(1));
                Dictionary_Weapons.Add(Part_Arms.Axe, new Model.Weapon(1));
                Dictionary_Weapons.Add(Part_Arms.Sniper_Rifle, new Model.Weapon(6));
                Dictionary_Weapons.Add(Part_Arms.Pistol, new Model.Weapon(3));
                Dictionary_Weapons.Add(Part_Arms.RPG, new Model.Weapon(4));
                Dictionary_Weapons.Add(Part_Arms.Heal_Gun, new Model.Weapon(3));

                Dictionary_Bodies = new Dictionary<Part_Body, Model.Bodypart>();
                Dictionary_Bodies.Add(Part_Body.Medium, new Model.Bodypart("Medium Armor",
                    "The basic armor.", 3, 0, 25));
                Dictionary_Bodies.Add(Part_Body.Light, new Model.Bodypart("Light Armor",
                    "Light armor that gains move speed at the cost of protection.", 2, 1, 25));
                Dictionary_Bodies.Add(Part_Body.Heavy, new Model.Bodypart("Heavy Armor",
                    "Armor made from heavy materials that trade speed for protection.", 4, -1, 25));
                Dictionary_Bodies.Add(Part_Body.Bomber, new Model.Bodypart("Bomber Chest",
                    "A bunch of explosives stuffed in a tin can. Explodes when destroyed.", 2, 0, 25));
                Dictionary_Bodies.Add(Part_Body.Healer, new Model.Bodypart("Self-Repairing Core",
                    "Medium armor that repairs itself automatically ever round.", 3, 0, 35));

                Dictionary_Legs = new Dictionary<Part_Legs, Model.Bodypart>();
                Dictionary_Legs.Add(Part_Legs.Legs, new Model.Bodypart("Humanoid",
                    "Humanoid legs that are neither especially fast nor fragile.", 0, 3, 25));
                Dictionary_Legs.Add(Part_Legs.Tank_Tread, new Model.Bodypart("Tank Treads",
                    "Tank tread legs that are incredibly hard to damage but lack in speed.", 1, 2, 25));
                Dictionary_Legs.Add(Part_Legs.Hover, new Model.Bodypart("Hover",
                    "A hover-engine that allows for rapid movement at the cost of durability.", -1, 4, 25));

                Dictionary_Condition = new Dictionary<Cub.Condition, Cub.Model.Condition.Base>();
                Dictionary_Condition[Cub.Condition.Any] = new Cub.Model.Condition.Any();
                Dictionary_Condition[Cub.Condition.Adjacent_2] = new Cub.Model.Condition.Adjacent_2();
                Dictionary_Condition[Cub.Condition.Is_Soldier] = new Cub.Model.Condition.Is_Soldier();
                Dictionary_Condition[Cub.Condition.Is_Sniper] = new Cub.Model.Condition.Is_Sniper();
                Dictionary_Condition[Cub.Condition.Is_Rocket] = new Cub.Model.Condition.Is_Rocket();
                Dictionary_Condition[Cub.Condition.Is_Medic] = new Cub.Model.Condition.Is_Medic();
                Dictionary_Condition[Cub.Condition.Is_Knight] = new Cub.Model.Condition.Is_Knight();
                Dictionary_Condition[Cub.Condition.Is_Jerk] = new Cub.Model.Condition.Is_Jerk();
                Dictionary_Condition[Cub.Condition.Almost_Dead] = new Cub.Model.Condition.Almost_Dead();
                Dictionary_Condition[Cub.Condition.Is_Hurt] = new Cub.Model.Condition.Is_Hurt();
                Dictionary_Condition[Cub.Condition.I_Am_Alone] = new Cub.Model.Condition.I_Am_Alone();
                Dictionary_Condition[Cub.Condition.They_Are_Alone] = new Cub.Model.Condition.They_Are_Alone();
                Dictionary_Condition[Cub.Condition.Closest] = new Cub.Model.Condition.Closest();

                Dictionary_Action = new Dictionary<Cub.Action, Cub.Model.Action.Base>();
                Dictionary_Action[Cub.Action.Attack] = new Cub.Model.Action.Attack();
                Dictionary_Action[Cub.Action.Explore] = new Cub.Model.Action.Explore();
                Dictionary_Action[Cub.Action.Charge] = new Cub.Model.Action.Charge();
                Dictionary_Action[Cub.Action.Missile] = new Cub.Model.Action.Missile();
                Dictionary_Action[Cub.Action.Heal] = new Cub.Model.Action.Heal();
                Dictionary_Action[Cub.Action.Snipe] = new Cub.Model.Action.Snipe();
                Dictionary_Action[Cub.Action.Follow_Ally] = new Cub.Model.Action.Follow_Ally();
                Dictionary_Action[Cub.Action.Follow_Enemy] = new Cub.Model.Action.Follow_Enemy();

                Action_Strings = new Dictionary<string, Cub.Action>();
                foreach (Cub.Model.Action.Base act in Dictionary_Action.Values)
                    Action_Strings.Add(act.Name, act.ActionType);
                Condition_Strings = new Dictionary<string, Cub.Condition>();
                foreach (Cub.Model.Condition.Base con in Dictionary_Condition.Values)
                    Condition_Strings.Add(con.Name, con.ConditionType);

                Cub.Model.Library.Stage_Terrain = Cub.Tool.Xml.Deserialize(typeof(Cub.Terrain[][]), "Data/Stage_Terrain.xml") as Cub.Terrain[][];
                Cub.Model.Library.Stage_Unit = Cub.Tool.Xml.Deserialize(typeof(Cub.Class[][]), "Data/Stage_Unit.xml") as Cub.Class[][];

                Trigger = false;
            }
        }

        public static Cub.Model.Bodypart Get_Head(Cub.Part_Head head)
        {
            if (Dictionary_Heads.ContainsKey(head))
                return Dictionary_Heads[head];
            else
                return null;
        }
        public static Cub.Part_Head Get_Head(string head)
        {
            foreach (Cub.Part_Head h in Dictionary_Heads.Keys)
                if (Get_Head(h).Name == head)
                    return h;
            return Cub.Part_Head.Soldier;
        }
        public static List<Cub.Model.Bodypart> List_Heads()
        {
            List<Cub.Model.Bodypart> r = new List<Model.Bodypart>();
            foreach (Cub.Part_Head head in Dictionary_Heads.Keys)
                r.Add(Dictionary_Heads[head]);
            return r;
        }

        public static Cub.Model.Bodypart Get_Arms(Cub.Part_Arms arms)
        {
            if (Dictionary_Arms.ContainsKey(arms))
                return Dictionary_Arms[arms];
            else
                return null;
        }
        public static Cub.Part_Arms Get_Arms(string arms)
        {
            foreach (Cub.Part_Arms a in Dictionary_Arms.Keys)
                if (Get_Arms(a).Name == arms)
                    return a;
            return Cub.Part_Arms.Rifle;
        }
        public static List<Cub.Model.Bodypart> List_Arms()
        {
            List<Cub.Model.Bodypart> r = new List<Model.Bodypart>();
            foreach (Cub.Part_Arms arms in Dictionary_Arms.Keys)
                r.Add(Dictionary_Arms[arms]);
            return r;
        }

        public static Cub.Model.Bodypart Get_Body(Cub.Part_Body body)
        {
            if (Dictionary_Bodies.ContainsKey(body))
                return Dictionary_Bodies[body];
            else
                return null;
        }
        public static Cub.Part_Body Get_Body(string body)
        {
            foreach (Cub.Part_Body b in Dictionary_Bodies.Keys)
                if (Get_Body(b).Name == body)
                    return b;
            return Cub.Part_Body.Medium;
        }
        public static List<Cub.Model.Bodypart> List_Bodies()
        {
            List<Cub.Model.Bodypart> r = new List<Model.Bodypart>();
            foreach (Cub.Part_Body body in Dictionary_Bodies.Keys)
                r.Add(Dictionary_Bodies[body]);
            return r;
        }

        public static Cub.Model.Bodypart Get_Legs(Cub.Part_Legs legs)
        {
            if (Dictionary_Legs.ContainsKey(legs))
                return Dictionary_Legs[legs];
            else
                return null;
        }
        public static Cub.Part_Legs Get_Legs(string legs)
        {
            foreach (Cub.Part_Legs l in Dictionary_Legs.Keys)
                if (Get_Legs(l).Name == legs)
                    return l;
            return Cub.Part_Legs.Legs;
        }
        public static List<Cub.Model.Bodypart> List_Legs()
        {
            List<Cub.Model.Bodypart> r = new List<Model.Bodypart>();
            foreach (Cub.Part_Legs legs in Dictionary_Legs.Keys)
                r.Add(Dictionary_Legs[legs]);
            return r;
        }

        public static Cub.Model.Weapon Get_Weapon(Cub.Part_Arms arms)
        {
            if (Dictionary_Weapons.ContainsKey(arms))
                return Dictionary_Weapons[arms];
            else
                return null;
        }

        public static Cub.Model.Condition.Base Get_Condition(Cub.Condition _Condition)
        {
            if (Dictionary_Condition.ContainsKey(_Condition))
                return Dictionary_Condition[_Condition];
            else
                return null;
        }

        public static Cub.Model.Action.Base Get_Action(Cub.Action _Action)
        {
            if (Dictionary_Action.ContainsKey(_Action))
                return Dictionary_Action[_Action];
            else
                return null;
        }

        public static Cub.Model.Character_Info Get_Character_Info(Cub.Class _Class)
        {
            if (Dictionary_Class_Info.ContainsKey(_Class))
                return Dictionary_Class_Info[_Class];
            else
                return null;
        }

        public static List<Cub.Model.Action.Base> List_Actions()
        {
            return List_Actions(Cub.Class.None);
        }
        public static List<Cub.Model.Action.Base> List_Actions(Cub.Class c)
        {
            List<Cub.Model.Action.Base> r = new List<Action.Base>();
            foreach (Cub.Model.Action.Base act in Dictionary_Action.Values)
                if (!act.SpecialAbility || Get_Character_Info(c).List_Special_Ability.Contains(act.ActionType))
                    r.Add(act);
            return r;
        }

        public static List<Cub.Model.Condition.Base> List_Conditions()
        {
            return List_Conditions(Cub.Action.None);
        }
        public static List<Cub.Model.Condition.Base> List_Conditions(Cub.Action a)
        {
            List<Cub.Model.Condition.Base> r = new List<Condition.Base>();
            foreach (Cub.Model.Condition.Base con in Dictionary_Condition.Values)
                if (Cub.Model.Library.Get_Action(a).ValidConditions.Contains(con.ConditionGenre))
                    r.Add(con);
            return r;
        }

        public static List<Cub.Model.Character_Info> List_Classes()
        {
            List<Cub.Model.Character_Info> r = new List<Cub.Model.Character_Info>();
            foreach (Cub.Model.Character_Info cls in Dictionary_Class_Info.Values)
                r.Add(cls);
            return r;
        }

        public static Cub.Action String_Action(string str)
        {
            if (Action_Strings.ContainsKey(str))
                return Action_Strings[str];
            return Cub.Action.None;
        }

        public static Cub.Condition String_Condition(string str)
        {
            if (Condition_Strings.ContainsKey(str))
                return Condition_Strings[str];
            return Cub.Condition.None;
        }
    }


}
