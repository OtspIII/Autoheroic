using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Tool;

namespace Cub.Tool.Action
{
    public class Follow_Ally : Cub.Tool.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        //public Charge(List<object> _Info)
        //{
        //    this.Info = _Info;
        //}

        public Follow_Ally()
        {
            Name = "Follow Ally";
            Description = "I will follow/go to an ally";
            ActionType = Cub.Action.Follow_Ally;
        }

        public override List<object> Confirm(Character who)
        {
            List<object> data = new List<object>();
            foreach (Character friend in who.Stat.Team.Return_List_Character())
                if (friend != who)
                    data.Add(friend);
            if (data.Count == 0) return null;
            return data;
        }

        public override List<Cub.View.GameEvent> Body(Character who, List<object> data)
        {
            who.Stat.Cooldown += this.Turn_Cooldown;
            if (data.Count == 0) return new List<View.GameEvent>();
            Character target;
            if (data.Count == 1)
                target = (Character)data[0];
            else
                target = (Character)data[UnityEngine.Random.Range(0, data.Count)];

            List<Cub.Position2> path = Pathfinder.findPath(who.Stat.Position, target.Stat.Position);
            int TravelDistance = Math.Min(who.Info.Speed,path.Count) - 1;
            if (TravelDistance < 0) return new List<View.GameEvent>();
            who.SetLocation(path[TravelDistance]);
            Debug.Log("Follow: " + who.Name + " (" + who.Info.Class + ") >" + path[TravelDistance].ToString());
            List<Cub.View.GameEvent> r = new List<Cub.View.GameEvent>();
            for (int n = 0; n <= TravelDistance; n++){
                r.Add(new Cub.View.GameEvent(Cub.Event.Walk, who.Name + ": Following " + target.Name, new List<object>() { who.ID, path[n].X, path[n].Y }));
            }
            return r;
        }
    }
}
