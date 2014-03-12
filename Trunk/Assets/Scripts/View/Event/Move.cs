using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event_Processor
{
    public class Move : Base
    {
        public override float Process(List<object> _Data)
        {
            Cub.View.Character Who = GameObject.Find("Runtime").GetComponent<Cub.View.Runtime>().Get_Character((Guid)_Data[0]);
            int X = (int)_Data[1];
            int Z = (int)_Data[2];
            
            Animator AF = Who.gameObject.GetComponent<Animator>();
            
            AF.SetTrigger("Move");

            iTween.MoveTo(Who.gameObject, new Vector3(X, 0, Z), 10.0F);
            
            return 0.0F;
        }
    }
}
