using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.View
{
    public class Team
    {
        public System.Guid ID { get; set; }
        private List<Cub.View.Character> List_Character { get; set; }

        public void Add_Character(Cub.View.Character _Character)
        {
            if (!this.List_Character.Contains(_Character))
            {
                this.List_Character.Add(_Character);
            }
        }

        public void Remove_Character(Cub.View.Character _Character)
        {
            if (this.List_Character.Contains(_Character))
            {
                this.List_Character.Remove(_Character);
            }
        }
    }
}
