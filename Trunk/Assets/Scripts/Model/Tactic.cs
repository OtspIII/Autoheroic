using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cub.Tool
{
    public class Tactic
    {
        public Cub.Target T { get; private set; }
        public Cub.Condition C { get; private set; }
        public Cub.Action A { get; private set; }
        public List<object> Data { get; private set; }

        //XmlSerializer needs a non-parameter constructor for the class
        public Tactic() 
        {
            this.T = Cub.Target.None;
            this.C = Cub.Condition.None;
            this.A = Cub.Action.None;
        }

        public Tactic(Cub.Target _T, Cub.Condition _C, Cub.Action _A)
        {
            this.T = _T;
            this.C = _C;
            this.A = _A;
        }

        public Tactic(Cub.Target _T, Cub.Condition _C, Cub.Action _A, List<object> _D)
        {
            this.T = _T;
            this.C = _C;
            this.A = _A;
            this.Data = _D;
        }
    }
}