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
            SpDescription = "";
            Cost = cst;
            E = e;
        }

        public BPHead(string name, string desc, int cst, Cub.Part_Head e, List<Special_Effects> eff, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Effects = eff;
            E = e;
        }

        public BPHead(string name, string desc, int cst, Cub.Part_Head e, List<Cub.Action> abil, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Special_Abilities = abil;
            E = e;
        }

        public BPHead(string name, string desc, int cst, Cub.Part_Head e, List<Special_Effects> eff, List<Cub.Action> abil, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            E = e;
        }
    }
}
