﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class I_Am_Alone : Base
    {
        protected Class TypeOfUnit;

        public I_Am_Alone()
        {
            ConditionType = Cub.Condition.I_Am_Alone;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            if (who.Stat.Team.Return_List_Character().Count <= 1)
                return data;
            return null;
        }
    }
}