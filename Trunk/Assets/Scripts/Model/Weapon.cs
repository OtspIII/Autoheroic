using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class Weapon
    {
        public int MissChance;
        public int CritChance;
        public int MissDam;
        public int HitDam;
        public int CritDam;

        public int Range;

        public Weapon()
        {

        }

        public Weapon(int range)
        {
            Range = range;
        }

        virtual public List<Cub.View.Eventon> Make_Attack(Cub.Tool.Character who, List<object> data)
        {
            List<Cub.View.Eventon> r = new List<View.Eventon>();

            return r;
        }
    }
}
