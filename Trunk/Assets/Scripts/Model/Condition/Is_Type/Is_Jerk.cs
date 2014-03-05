using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Jerk : Is_Type
    {

        public Is_Jerk()
        {
            ConditionType = Cub.Condition.Is_Jerk;
            TypeOfUnit = Class.Jerk;
        }
    }
}
