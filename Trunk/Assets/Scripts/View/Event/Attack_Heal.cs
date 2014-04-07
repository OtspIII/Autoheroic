﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Heal : Base
    {
        private const float Timespan = 0.1F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Attack_Range");
            switch (C0.Stat.Arms)
            {
                case Part_Arms.Rifle:
                    {
                        C0.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Attack_Range_Rifle");
                        C0.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Attack_Range_Rifle");
                        break;
                    }
            }
            C0.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Attack_Range");

            C0.BroadcastMessage("Idle", Timespan + 0.5F, SendMessageOptions.DontRequireReceiver);
            
            C1.gameObject.particleSystem.Emit(10);

            Cub.View.NarratorController.DisplayText(Desc, Timespan + 0.5F);

            return Timespan;
        }
    }
}
