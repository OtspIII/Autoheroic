﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Move : Base
    {
        public override float Process(List<object> _Data)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);
            int X = (int)_Data[1];
            int Z = (int)_Data[2];
            
            //Animator AF = C.gameObject.GetComponent<Animator>();            
            //AF.SetTrigger("Move");

            iTween.MoveTo(C.gameObject, new Vector3(X, 0, Z), 1.5F);
            
            return 0.5F;
        }
    }
}