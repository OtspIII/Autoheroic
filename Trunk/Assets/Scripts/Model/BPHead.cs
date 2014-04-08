using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPHead : Bodypart
    {
        
        public BPHead(string name, string desc, int cst)
        {
            Name = name;
            Description = desc;
            Cost = cst;
        }

        public BPHead(string name, string desc, int cst, List<Special_Effects> eff)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Effects = eff;
        }

        public BPHead(string name, string desc, int cst, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Special_Abilities = abil;
        }

        public BPHead(string name, string desc, int cst, List<Special_Effects> eff, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
        }
    }
}
