using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Range : Base
    {
        public override float Process(List<object> _Data)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.transform.LookAt(C1.transform.position);

            C0.gameObject.GetComponent<Animator>().SetTrigger("Attack_Range");

            return 1.5F;
        }
    }
}
