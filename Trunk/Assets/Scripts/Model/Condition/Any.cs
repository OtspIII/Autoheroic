using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Any : Base
    {
        public Any()
        {
            this.ConditionType = Cub.Condition.Any;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            return data;
        }
    }
}
