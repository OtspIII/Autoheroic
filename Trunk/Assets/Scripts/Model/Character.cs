using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool.Condition;

namespace Cub.Tool
{
    public class Character_Info
    {
        public Cub.Class Class { get; private set; }
        public string Name { get; private set; }
        public int Range { get; private set; }
        public int MHP { get; private set; }
        public int Speed { get; private set; }
        public int Value { get; private set; }
        public List<Cub.Tool.Tactic> List_Tactic { get; private set; }
        public List<Cub.Action> List_Special_Ability { get; private set; }
    }

    public class Character_Stat
    {
        public Cub.Position2 Position { get; set; }
        public int HP { get; set; }
        public int Cooldown { get; set; }
        public Cub.Tool.Team Team { get; set; }
    }

    public class Character
    {
        public string Name { get; private set; }
        public System.Guid ID { get; private set; }
        public Character_Info Info { get; private set; }
        public Character_Stat Stat { get; private set; }
        public List<Cub.Tool.Tactic> Bought_Tactic = new List<Tactic>();
        public List<Cub.Tool.Tactic> Free_Tactic = new List<Tactic>();
        public List<Cub.Tool.Tactic> Tactics { get { return FindTactics(); } }
        public int Value { get { return FindValue(); } }
        public List<Cub.Action> ExhaustedActions = new List<Cub.Action>();

        public Character()
        {

        }

        public Character(Cub.Class _Class, int _X, int _Y)
        {
            SetName("John Smith");
            this.ID = System.Guid.NewGuid();
            this.Stat = new Character_Stat();

            //switch (_Class)
            //{
            //    case Type.Class.None:
            //        {
            //            this.Info = Library.Character_Info_None;
            //            break;
            //        }
            //    case Type.Class.Archer:
            //        {
            //            this.Info = Library.Character_Info_Archer;
            //            break;
            //        }
            //    case Type.Class.Knight:
            //        {
            //            this.Info = Library.Character_Info_Knight;
            //            break;
            //        }
            //    case Type.Class.Mage:
            //        {
            //            this.Info = Library.Character_Info_Mage;
            //            break;
            //        }
            //}
            SetClass(_Class);

            SetLocation(_X, _Y);

            this.Stat.Cooldown = 0;
        }

        public bool Damage(int Amount, Character source, List<Cub.View.Eventon> events)
        {
            this.Stat.HP -= Amount;
            events.Add(new View.Eventon(Event.Be_Attacked, Name + " <" + Amount.ToString() + " Damage>", 
                new List<object> { ID, Amount}));
            if (this.Stat.HP <= 0)
            {
                Debug.Log("Die: " + this.Name + " (" + this.Info.Class + ")");
                events.Add(new View.Eventon(Event.Die, "R.I.P. " + Name, new List<object> { ID }));
                //Debug.Log(source.Name + " / " + source.Stat.Team);
                //Debug.Log(source.Stat.Team.Name);
                source.Stat.Team.AddScore("Kills", Value);
                Main.Dispose(this,events);
                return true;
            }
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

        //public List<Cub.View.GameEvent> Goo()
        //{
        //    //Debug.Log("Starting Thought Process " + this.Tactics);
        //    foreach (Tactic T in this.Tactics)
        //    {
        //        List<Cub.Model.Character> CL = new List<Character>();
        //        List<Cub.Model.Character> Valid = new List<Character>();
        //        switch (T.T)
        //        {
        //            case Cub.Target.Enemy:
        //                {
        //                    CL.AddRange(FindEnemies());
        //                    //foreach (Team team in Cub.Model.Main.List_Team){
        //                    //    if (team != Stat.Team)
        //                    //        CL.AddRange(team.Return_List_Character());
        //                    //}
        //                    //CL.AddRange(team.Return_List_Character());
        //                    //CL.Remove(this);

        //                    int Index = 0;

        //                    while (Index < CL.Count)
        //                    {
        //                        Cub.Model.Character CT = CL[Index];

        //                        bool FitTheConditionsFlag = true;

        //                        foreach (Cub.Condition C in T.C)
        //                        {
        //                            switch (C)
        //                            {
        //                                case Cub.Condition.In_Range:
        //                                    {
        //                                        //if (Mathf.Abs(this.Stat.Position_X - CT.Stat.Position_X) + Mathf.Abs(this.Stat.Position_Y - CT.Stat.Position_Y) > this.Info.Range)
        //                                        //Debug.Log(Position + " / " + CT.Position + " / " + Pathfinder.Distance(Position, CT.Position) + " / " + Info.Range);
        //                                        if (Pathfinder.Distance(Position,CT.Position) > this.Info.Range)
        //                                        {
        //                                            FitTheConditionsFlag = false;
        //                                        }
        //                                        break;
        //                                    }
        //                            } 
        //                        }
        //                        if (FitTheConditionsFlag)
        //                            Valid.Add(CT);
        //                        Index++;
        //                    }

        //                    //If the tactic passes the test
        //                    if (Valid.Count > 0)
        //                    {
        //                        Character who = Valid[Random.RandomRange(0,Valid.Count)];
        //                        switch (T.A)
        //                        {
        //                            case Cub.Action.Attack:
        //                                {
        //                                    //Debug.Log(Info.Class + " : " + Position + " / " + who.Info.Class + ": "
        //                                    //    + who.Position + " / " + Pathfinder.Distance(Position, who.Position) 
        //                                    //    + " / " + Info.Range);
        //                                    Action.Attack AO = new Action.Attack(new List<object>() { this, who });
        //                                    return AO.Body();
        //                                    //break;
        //                                }
        //                        }
        //                    }

        //                    break;
        //                }
        //            case Cub.Target.Self:
        //                {
        //                    bool FitTheConditionsFlag = true;

        //                    foreach (Cub.Condition C in T.C)
        //                    {
        //                        switch (C)
        //                        {
        //                            case Cub.Condition.Any:
        //                                {
        //                                    break;
        //                                }
        //                        }
        //                    }

        //                    //If the tactic passes the test
        //                    if (FitTheConditionsFlag)
        //                    {
        //                        switch (T.A)
        //                        {
        //                            case Cub.Action.Explore:
        //                                {
        //                                    Action.Explore EO = new Action.Explore(new List<object>() { this });
        //                                    return EO.Body();
        //                                    break;
        //                                }
        //                            case Cub.Action.Charge:
        //                                {
        //                                    Action.Charge EO = new Action.Charge(new List<object>() { this });
        //                                    return EO.Body();
        //                                    break;
        //                                }
        //                        }
        //                    }

        //                    break;
        //                }
        //        }
        //    }

        //    return null;
        //}

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetClass(string _Class)
        {
            switch (_Class)
            {
                case "Soldier":
                    {
                        SetClass(Cub.Class.Soldier);
                        return;
                    }
                case "Knight":
                    {
                        SetClass(Cub.Class.Knight);
                        return;
                    }
                case "Rocket":
                    {
                        SetClass(Cub.Class.Rocket);
                        return;
                    }
                case "Jerk":
                    {
                        SetClass(Cub.Class.Jerk);
                        return;
                    }
                case "Sniper":
                    {
                        SetClass(Cub.Class.Sniper);
                        return;
                    }
                case "Medic":
                    {
                        SetClass(Cub.Class.Medic);
                        return;
                    }
            }
            SetClass(Cub.Class.None);
        }
        public void SetClass(Cub.Class _Class)
        {
            this.Info = Library.Get_Character_Info(_Class);
            this.Stat.HP = this.Info.MHP;
            foreach (Cub.Tool.Tactic tac in this.Info.List_Tactic)
            {
                Tactic t = new Tactic(tac.C, tac.A, tac.Data);
                t.Free = true;
                this.Free_Tactic.Add(t);
            }
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
                if (t != Stat.Team)
                    r.AddRange(t.Return_List_Character());
            return r;
        }

        public void BuyTactic(Cub.Condition condition, Cub.Action action)
        {
            Bought_Tactic.Add(new Tactic(condition, action));
        }

        List<Cub.Tool.Tactic> FindTactics()
        {
            List<Cub.Tool.Tactic> r = new List<Tactic>();
            r.AddRange(Bought_Tactic);
            r.AddRange(Free_Tactic);
            return r;
        }

        int FindValue()
        {
            return Info.Value;
        }
    }

}