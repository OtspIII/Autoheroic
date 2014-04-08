using System;
using System.Collections.Generic;
using Cub.Model;
using UnityEngine;

namespace Cub.Model.Action
{
    class BlowUp : Cub.Model.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        public int Damage = 3;

        public BlowUp()
        {
            Name = "Blow Up";
            Description = "Self destruct";
            SpecialAbility = true;
            ActionType = Cub.Action.BlowUp;
        }

        public override List<Cub.View.Eventon> Body(Character who, List<object> data)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            
            Debug.Log("Missile: " + who.Name + " (" + who.Info.Class + ") > " + who.Name + " (" + who.Info.Class + ")");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Rocket, who.FindColorName() + " <MISSILE> " + who.FindColorName(),
                new List<object>() { who.ID, who.Stat.Position.ToVector2() }));
            foreach (Character guy in Main.AllCharacters())
                if (guy != who && Cub.Tool.Pathfinder.Distance(who.Stat.Position, guy.Stat.Position) <= 1.5f)
                    guy.Damage(Damage, who,r,Cub.AttackResults.Hit);
            return r;
        }
    }
}
