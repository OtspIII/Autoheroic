using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool.Condition;

namespace Cub.Tool
{

    public class Character_Save
    {
        public string Name;
        public Bodypart_Head Head;
        public Bodypart_Arms Arms;
        public Bodypart_Body Body;
        public Bodypart_Legs Legs;
        public Position2 Position;
        //public int X;
        //public int Y;

        public Cub.Model.Bodypart Head_Part { get { return Cub.Tool.Library.Get_Head(Head); } }
        public Cub.Model.Bodypart Body_Part { get { return Cub.Tool.Library.Get_Body(Body); } }
        public Cub.Model.Bodypart Arms_Part { get { return Cub.Tool.Library.Get_Arms(Arms); } }
        public Cub.Model.Bodypart Legs_Part { get { return Cub.Tool.Library.Get_Legs(Legs); } }
        public Cub.Model.Weapon Weapon { get { return Cub.Tool.Library.Get_Weapon(Arms); } }
        public int Value { get { return Head_Part.Cost + Arms_Part.Cost + Body_Part.Cost + Legs_Part.Cost; } }
        public int Health { get { return Mathf.Max(1,Head_Part.Health + Arms_Part.Health + Body_Part.Health + Legs_Part.Health); } }
        public int Speed { get { return Mathf.Max(1,Head_Part.Speed + Arms_Part.Speed + Body_Part.Speed + Legs_Part.Speed); } }

        public Character_Save()
        {

        }

        public Character_Save(string name, Bodypart_Head head, Bodypart_Arms arms, Bodypart_Body body, Bodypart_Legs legs, int _X, int _Y)
        {
            Name = name;
            Head = head;
            Arms = arms;
            Body = body;
            Legs = legs;
            Position = new Position2(_X, _Y);
        }
    }

    public class Character_Info
    {
        public Bodypart_Head Head { get; set; }
        public Bodypart_Arms Arms { get; set; }
        public Bodypart_Body Body { get; set; }
        public Bodypart_Legs Legs { get; set; }
        public List<Special_Effects> Effects { get; set; }
        public Cub.Model.Weapon Weapon { get; set; }
        
        public Cub.Class Class { get; set; }
        public string Name { get; set; }
        public int Range { get; set; }
        public int MHP { get; set; }
        public int Speed { get; set; }
        public int Value { get; set; }
        //public List<Cub.Tool.Tactic> List_Tactic { get; private set; }
        public List<Cub.Action> List_Special_Ability { get; set; }
    }

    public class Character_Stat
    {
        public Cub.Position2 Position { get; set; }
        public int HP { get; set; }
        public int Cooldown { get; set; }
        private Cub.Tool.Team Team { get; set; }

        public Team GetTeam()
        {
            return this.Team;
        }

        public void SetTeam(Team t)
        {
            this.Team = t;
        }
    }

    public class Character
    {
        public string Name { get; private set; }
        public System.Guid ID { get; private set; }
        public Character_Info Info { get; private set; }
        public Character_Stat Stat { get; private set; }
        //public List<Cub.Tool.Tactic> Bought_Tactic = new List<Tactic>();
        //public List<Cub.Tool.Tactic> Free_Tactic = new List<Tactic>();
        public List<Cub.Tool.Tactic> Tactics = new List<Tactic>();
        public int Value { get { return FindValue(); } }
        public List<Cub.Action> ExhaustedActions = new List<Cub.Action>();

        public Character()
        {

        }

        public Character(Character_Save save)
        {
            Imprint(save);
        }

        public void Imprint(Character_Save save)
        {
            SetName(save.Name);
            this.ID = System.Guid.NewGuid();
            this.Stat = new Character_Stat();
            this.Info = BuildInfo(save.Head, save.Arms, save.Body, save.Legs);

            SetLocation(save.Position);

            this.Stat.Cooldown = 0;
        }

        public bool Damage(int Amount, Character source, List<Cub.View.Eventon> events)
        {
            this.Stat.HP -= Amount;
            if (this.Stat.HP <= 0)
            {
                Debug.Log("Die: " + this.Name + " (" + this.Info.Class + ")");
                events.Add(new View.Eventon(Event.Die, "R.I.P. " + Name, new List<object> { ID }));
                //Debug.Log(source.Name + " / " + source.Stat.Team);
                //Debug.Log(source.Stat.Team.Name);
                source.Stat.GetTeam().AddScore("Kills", Value);
                Main.Dispose(this,events);
                return true;
            }
            else
                events.Add(new View.Eventon(Event.Be_Attacked, Name + " <" + Amount.ToString() + " Damage>",
                new List<object> { ID, Amount }));
            return false;
        }

        public void Heal(int Amount, Character source, List<Cub.View.Eventon> events)
        {
            Amount = Mathf.Min(Amount, Info.MHP - Stat.HP);
            this.Stat.HP = Mathf.Min(this.Stat.HP + Amount,this.Info.MHP);
            events.Add(new View.Eventon(Event.Be_Healed, Name + " [" + Amount + " Heal]", new List<object> { ID, Amount }));
        }

        public List<Cub.View.Eventon> Go()
        {
            foreach (Tactic T in this.Tactics)
            {
                if (T.A == Cub.Action.None) continue;
                Cub.Tool.Action.Base a = Library.Get_Action(T.A);
                if (a == null) Debug.Log("ERROR: " + T.A);
                List<object> Data = a.Confirm(this);
                if (Data == null) continue;
                if (T.C != Cub.Condition.None)
                {
                    Condition.Base c = Library.Get_Condition(T.C);
                    if (c == null) Debug.Log("ERROR: " + T.C);
                    Data = c.Confirm(this, Data);
                    if (Data == null) continue;
                }
                return a.Body(this, Data);
            }
            return new List<View.Eventon>();
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public Character_Info BuildInfo(Bodypart_Head head, Bodypart_Arms arms, Bodypart_Body body, Bodypart_Legs legs)
        {
            Character_Info info = new Character_Info();
            info.Head = head;
            info.Arms = arms;
            info.Body = body;
            info.Legs = legs;
            info.Weapon = Cub.Tool.Library.Get_Weapon(arms);

            List<Cub.Model.Bodypart> parts = new List<Model.Bodypart>();
            parts.Add(Cub.Tool.Library.Get_Head(head));
            parts.Add(Cub.Tool.Library.Get_Arms(arms));
            parts.Add(Cub.Tool.Library.Get_Body(body));
            parts.Add(Cub.Tool.Library.Get_Legs(legs));

            int hp = 0;
            int sp = 0;
            int value = 0;
            List<Special_Effects> effects = new List<Special_Effects>();
            List<Cub.Action> acts = new List<Cub.Action>();

            foreach (Cub.Model.Bodypart bp in parts)
            {
                hp += bp.Health;
                sp += bp.Speed;
                value += bp.Cost;
                foreach (Special_Effects se in bp.Effects)
                    if (!effects.Contains(se))
                        effects.Add(se);
                foreach (Cub.Action a in bp.Special_Abilities)
                    if (!acts.Contains(a))
                        acts.Add(a);
            }

            info.Value = value;
            info.MHP = Mathf.Max(1,hp);
            info.Speed = Mathf.Max(1,sp);
            info.List_Special_Ability = acts;
            info.Effects = effects;
            info.Range = info.Weapon.Range;

            //This is temporary.##
            info.Class = Class.Soldier;

            this.Stat.HP = info.MHP;

            this.Tactics = BuildAIProfile(head);
            
            return info;
        }

        public void SetLocation(Cub.Position2 where)
        {
            this.SetLocation(where.X, where.Y);
        }

        public void SetLocation(int x, int y)
        {
            this.Stat.Position = new Position2(x, y);
        }

        public List<Character> FindEnemies()
        {
			List<Character> r = new List<Character>();
            foreach (Team t in Cub.Tool.Main.List_Team)
                if (t != Stat.GetTeam())
                    r.AddRange(t.Return_List_Character());
            return r;
        }

        public Tactic BuyTactic(Cub.Condition condition, Cub.Action action)
        {
            Tactic tac = new Tactic(condition, action);
            Tactics.Add(tac);
            return tac;
        }

        int FindValue()
        {
            int r = Info.Value;
            foreach (Tactic tac in Tactics)
                if (!tac.Free)
                    r += 10;
            return r;
        }

        public List<Tactic> BuildAIProfile(Cub.Bodypart_Head head)
        {
            List<Tactic> r = new List<Tactic>();
            switch (head)
            {
                case Cub.Bodypart_Head.Soldier:
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Follow_Enemy));
                    break;

                case Cub.Bodypart_Head.Idiot:
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Explore));
                    break;

                case Cub.Bodypart_Head.Protector:
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    r.Add(new Tactic(Cub.Condition.Closest, Cub.Action.Follow_Ally));
                    break;

                case Cub.Bodypart_Head.Hunter:
                    r.Add(new Tactic(Cub.Condition.Almost_Dead, Cub.Action.Attack));
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    r.Add(new Tactic(Cub.Condition.Almost_Dead, Cub.Action.Follow_Enemy));
                    r.Add(new Tactic(Cub.Condition.Any, Cub.Action.Follow_Enemy));
                    break;
            }
            //foreach (Tactic t in r)
            //    t.Free = true;
            return r;
        }

        public void MakeUnique()
        {
            ID = System.Guid.NewGuid();
        }

        public string FindColorName()
        {
            string r = this.Name;
            string color = "FF0000";
            if (Cub.Tool.Main.List_Team[0] != Stat.GetTeam())
                color = "00FF00";
            return "[" + color + "]" + r + "[-]";
        }
    }

}