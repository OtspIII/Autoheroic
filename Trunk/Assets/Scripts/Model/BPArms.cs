﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPArms : Bodypart
    {

        public Cub.Part_Arms E;

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            E = e;
        }

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e, List<Special_Effects> eff)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Effects = eff;
            E = e;
        }

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e, List<Cub.Action> abil)
        {
            Name = name;
            Description = desc;
            Cost = cst;
            Special_Abilities = abil;
            E = e;
        }

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e, List<Special_Effects> eff, List<Cub.Action> abil)
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
