using Cub.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public abstract class Base
    {
        public Cub.Condition ConditionType;

        public Base()
        {

        }

        public abstract List<object> Confirm(Character who, List<object> data);
    }
}
