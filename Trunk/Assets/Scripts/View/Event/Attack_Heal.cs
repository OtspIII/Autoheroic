﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Heal : Base
    {
        public override float Process(List<object> _Data)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.gameObject.GetComponent<Animator>().SetTrigger("Attack_Range");
            
            C1.gameObject.particleSystem.Emit(10);

            return 1.5F;
        }
    }
}
