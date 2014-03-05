using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Sniper : Is_Type
    {

        public Is_Sniper()
        {
            ConditionType = Cub.Condition.Is_Sniper;
            TypeOfUnit = Class.Sniper;
        }
    }
}
