﻿using System;
using System.Collections.Generic;

namespace Cub.Tool
{
    public class TeamSave
    {
        public string Name;
        public string Owner_Name;
        public List<Cub.Tool.Character_Save> Chars;
        public int TotalValue { get { return FindTotalValue(); } }

        public TeamSave()
        {

        }

        public void Add_Character(Character_Save c)
        {
            Chars.Add(c);
        }

        public void Remove_Character(Character_Save c)
        {
            Chars.Remove(c);
        }

        public int FindTotalValue()
        {
            int r = 0;
            foreach (Character_Save cs in Chars)
                r += cs.Value;
            return r;
        }

        public Team Extract_Team()
        {
            Team r = new Team();
            r.Imprint(this);
            return r;
        }
    }
    
    public class Team
    {
        public System.Guid ID { get; private set; }
        public string Name { get; set; }
        public string Owner_Name { get; set; }
        public List<Cub.Tool.Character> List_Character { get; set; }
        public List<Cub.Score> ScoreThings { get; private set; }
        public int TotalValue { get { return FindTValue(); } }
        public int Score { get { return FindScore(); } }

        public Team()
        {
            Name = "-Empty-";
            Owner_Name = "";
            this.ID = System.Guid.NewGuid();
            this.List_Character = new List<Character>();
            ScoreThings = new List<Score>();
        }

        public void Imprint(TeamSave parent){
            Name = parent.Name;
            Owner_Name = parent.Owner_Name;
            this.ID = System.Guid.NewGuid();
            foreach (Character_Save cs in parent.Chars)
            {
                Character c = new Character();
                c.Imprint(cs);
                Add_Character(c);
            }
        }

        public void Add_Character(Cub.Tool.Character _C)
        {
            this.List_Character.Add(_C);
            _C.Stat.SetTeam(this);
        }

        public void Remove_Character(Cub.Tool.Character _C)
        {
            if (Contains_Character(_C))
            {
                this.List_Character.Remove(_C);
                //_C.Stat.Team = null;
            }
        }

        public bool Contains_Character(Cub.Tool.Character _C)
        {
            return this.List_Character.Contains(_C);
        }

        public List<Cub.Tool.Character> Return_List_Character()
        {
            List<Cub.Tool.Character> CL = new List<Character>();
            CL.AddRange(this.List_Character);
            return CL;
        }

        public void SetName(string n, string on)
        {
            Name = n;
            Owner_Name = on;
        }

        public void AddScore(string name, int value)
        {
            foreach (Score st in ScoreThings)
                if (st.Name == name)
                {
                    st.Value += value;
                    return;
                }
            ScoreThings.Add(new Score(name, value));
        }

        public int FindScore()
        {
            int r = 0;
            foreach (Score st in ScoreThings)
                r += st.Value;
            return r;
        }

        public void MakeUnique()
        {
            ID = System.Guid.NewGuid();
            foreach (Character who in List_Character)
            {
                who.MakeUnique();
            }
        }

        int FindTValue()
        {
            int r = 0;
            foreach (Character who in List_Character)
                r += who.Value;
            return r;
        }
    }

}
