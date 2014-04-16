﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cub.Model.Weapons
{
    class Heal_Gun : Cub.Model.Weapon
    {
        public Heal_Gun(int range, int dam)
        {
            Range = range;
            HitDam = dam;
        }

        public override List<View.Eventon> Make_Attack(Character who, Character target, int roll)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            Debug.Log("Attack: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ")");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Harm, who.FindColorName() + " vs. " + target.FindColorName(),
                new List<object>() { who.ID, target.ID }));
            target.Damage(HitDam, who, r, Attack_Result.Hit);
            return r;
        }
    }
}
