﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Soldier : Is_Type
    {

        public Is_Soldier()
        {
            ConditionType = Cub.Condition.Adjacent_2;
            TypeOfUnit = Class.Soldier;
        }
    }
}