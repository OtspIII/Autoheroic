using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class Bodypart
    {
        public string Name;
        public string Description;
        
        public int Health;
        public int Speed;
        public int Cost;

        public List<Special_Effects> Effects = new List<Special_Effects>();
        public List<Action> Special_Abilities = new List<Action>();

        public Bodypart(string name, string desc, int hp, int sp, int cst)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Speed = sp;
            Cost = cst;
        }

        public Bodypart(string name, string desc, int hp, int sp, int cst, List<Special_Effects> eff)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Speed = sp;
            Cost = cst;
            Effects = eff;
        }

        public Bodypart(string name, string desc, int hp, int sp, int cst, List<Action> abil)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Speed = sp;
            Cost = cst;
            Special_Abilities = abil;
        }

        public Bodypart(string name, string desc, int hp, int sp, int cst, List<Special_Effects> eff, List<Action> abil)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Speed = sp;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
        }
    }
}
