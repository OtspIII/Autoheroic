using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPBody : Bodypart
    {
        public int Health;
        
        public BPBody(string name, string desc, int hp, int cst)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Cost = cst;
        }

        public BPBody(string name, string desc, int hp, int cst, List<Special_Effects> eff)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Cost = cst;
            Effects = eff;
        }

        public BPBody(string name, string desc, int hp, int cst, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Cost = cst;
            Special_Abilities = abil;
        }

        public BPBody(string name, string desc, int hp, int cst, List<Special_Effects> eff, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Health = hp;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
        }
    }
}
