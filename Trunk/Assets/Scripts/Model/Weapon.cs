using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cub.Model
{
    public class Weapon
    {
        public int MissChance;
        public int CritChance;
        public int HitDam;
        public int CritDam;

        public int Range;

        public Weapon()
        {

        }

        public Weapon(int range, int missC, int critC, int hitD, int critD)
        {
            Range = range;
            MissChance = missC;
            CritChance = critC;
            HitDam = hitD;
            CritDam = critD;
        }

        virtual public List<Cub.View.Eventon> Make_Attack(Cub.Model.Character who, Cub.Model.Character target, int roll)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            Cub.AttackResults result = AttackResults.Hit;
            int CritNum = 10 - CritChance;
            if (roll < MissChance)
                result = AttackResults.Miss;
            else if (roll >= CritNum)
                result = AttackResults.Crit;
            int dam = HitDam;
            if (result == AttackResults.Miss)
                dam = 0;
            else if (result == AttackResults.Crit)
                dam = CritDam;
            Debug.Log("Attack: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ") ["
                + MissChance.ToString() + "/" + CritChance.ToString() + "/" + roll.ToString() + "/" + result + "/" + dam.ToString() + "]");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Range, who.FindColorName() + " vs. " + target.FindColorName(), 
                new List<object>(){ who.ID, target.ID,result,dam }));
            if (dam > 0)
                target.Damage(dam, who, r, result);
            return r;
        }
    }
}
