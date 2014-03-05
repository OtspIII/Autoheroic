﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class They_Are_Alone : Base
    {
        protected Class TypeOfUnit;

        public They_Are_Alone()
        {
            ConditionType = Cub.Condition.They_Are_Alone;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            if (who.FindEnemies().Count <= 1)
                return data;
            return null;
        }
    }
}