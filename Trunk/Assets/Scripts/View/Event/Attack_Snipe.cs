using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Snipe:Base
    {
        public override float Process(List<object> _Data)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);

            C.gameObject.GetComponent<Animator>().SetTrigger("Attack_Snipe");

            return 1.5F;
        }
    }
}
