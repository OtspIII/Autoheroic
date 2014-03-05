using UnityEngine;
using System.Collections.Generic;

namespace Cub.Tool.Action
{
    public abstract class Base
    {
        public abstract string Name { get; }
        public Cub.Action ActionType { get; protected set; }

        public abstract int Turn_Casting { get; }
        public abstract int Turn_Cooldown { get; }

        //public List<object> Info { get; protected set; }

        public virtual List<object> Confirm(Character who)
        {
            return new List<object>();
        }

        public abstract List<Cub.View.GameEvent> Body(Character who, List<object> data);
    }
}