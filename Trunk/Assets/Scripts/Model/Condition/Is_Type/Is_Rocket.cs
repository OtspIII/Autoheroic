using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Rocket : Is_Type
    {

        public Is_Rocket()
        {
            ConditionType = Cub.Condition.Is_Rocket;
            TypeOfUnit = Class.Rocket;
        }
    }
}
