using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPLegs : Bodypart
    {
        public int Speed;
        public bool Blockable;

        public BPLegs(string name, string desc, int sp, int cst)
        {
            Name = name;
            Description = desc;
            Speed = sp;
            Cost = cst;
            Blockable = true;
        }

        public BPLegs(string name, string desc, int sp, int cst, List<Special_Effects> eff, bool block)
        {
            Name = name;
            Description = desc;
            Speed = sp;
            Cost = cst;
            Effects = eff;
            Blockable = block;
        }

        public BPLegs(string name, string desc, int sp, int cst, List<Cub.Action> abil, bool block)
        {
            Name = name;
            Description = desc;
            Speed = sp;
            Cost = cst;
            Special_Abilities = abil;
            Blockable = block;
        }

        public BPLegs(string name, string desc, int sp, int cst, List<Special_Effects> eff, List<Cub.Action> abil, bool block)
        {
            Name = name;
            Description = desc;
            Speed = sp;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            Blockable = block;
        }
    }
}
