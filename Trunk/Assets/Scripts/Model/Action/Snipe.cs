﻿using System;
using System.Collections.Generic;
using Cub.Tool;
using UnityEngine;

namespace Cub.Tool.Action
{
    class Snipe : Cub.Tool.Action.Base
    {
        public override string Name { get { return "Snipe"; } }

        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        public int Range = 6;
        public int Damage = 8;

        public Snipe()
        {
            ActionType = Cub.Action.Snipe;
        }

        public override List<object> Confirm(Character who)
        {
            if (who.ExhaustedActions.Contains(ActionType)) return null;
            List<object> data = new List<object>();
            bool anyone = false;
            foreach (Character enemy in who.FindEnemies())
                if (Pathfinder.Distance(who.Stat.Position, enemy.Stat.Position) <= Range)
                {
                    data.Add(enemy);
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
            who.ExhaustedActions.Add(ActionType);
            Debug.Log("Snipe: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ")");
            r.Add(new Cub.View.GameEvent(Cub.Event.Snipe, who.Name + " <SNIPE> " + target.Name, new List<object>() { who.ID, target.ID }));
            target.Damage(Damage, who, r);
            return r;
        }
    }
}