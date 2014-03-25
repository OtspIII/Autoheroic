using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Rocket : Base
    {
        public override float Process(List<object> _Data)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.transform.LookAt(C1.transform.position);

            C0.gameObject.GetComponent<Animator>().SetTrigger("Attack_Rocket");

            GameObject B = UnityEngine.Object.Instantiate(Library.Get_Bullet()) as GameObject;

            TrailRenderer tr = (TrailRenderer)B.GetComponent("TrailRenderer");
            tr.material.color = Color.green;

            B.transform.position = C0.transform.position;

            iTween.MoveTo(B, C1.transform.position, 1.0F);

            GameObject.Destroy(B, 2.0F);

            return 1.5F;
        }
    }
}
