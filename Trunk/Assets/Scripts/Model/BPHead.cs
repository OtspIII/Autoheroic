using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPHead : Bodypart
    {

        public Cub.Part_Head E;

        public BPHead(string name, string desc, int cst, Cub.Part_Head e)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            E = e;
        }

        public BPHead(string name, string desc, int cst, Cub.Part_Head e, List<Special_Effects> eff)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Effects = eff;
            E = e;
        }

        public BPHead(string name, string desc, int cst, Cub.Part_Head e, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Special_Abilities = abil;
            E = e;
        }

        public BPHead(string name, string desc, int cst, Cub.Part_Head e, List<Special_Effects> eff, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            E = e;
        }
    }
}
