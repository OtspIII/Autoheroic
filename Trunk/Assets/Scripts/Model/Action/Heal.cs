using System;
using System.Collections.Generic;
using Cub.Tool;
using UnityEngine;

namespace Cub.Tool.Action
{
    class Heal : Cub.Tool.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        public int Range = 3;
        public int HealAmt = 1;

        public Heal()
        {
            Name = "Heal";
            Description = "I will heal an ally for 1HP";
            ActionType = Cub.Action.Heal;
        }

        public override List<object> Confirm(Character who)
        {
            if (who.ExhaustedActions.Contains(ActionType)) return null;
            List<object> data = new List<object>();
            bool anyone = false;
            foreach (Character friend in who.Stat.Team.Return_List_Character())
                if (Pathfinder.Distance(who.Stat.Position, friend.Stat.Position) <= Range)
                {
                    data.Add(friend);
                    anyone = true;
                }
            if (!anyone)
                return null;
            return data;
        }

        public override List<Cub.View.GameEvent> Body(Character who, List<object> data)
        {
            List<Cub.View.GameEvent> r = new List<Cub.View.GameEvent>();
            if (data.Count == 0) return new List<View.GameEvent>();
            Character target = null;
            if (data.Count == 1)
                target = (data[0] as Cub.Tool.Character);
            else
                target = (data[UnityEngine.Random.Range(0, data.Count)] as Cub.Tool.Character);
            who.Stat.Cooldown += this.Turn_Cooldown;
            Debug.Log("Heal: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ")");
            r.Add(new Cub.View.GameEvent(Cub.Event.Heal, who.Name + " <HEAL> " + target.Name, new List<object>() { who.ID, target.ID }));
            target.Heal(HealAmt, who, r);
            return r;
        }
    }
}
