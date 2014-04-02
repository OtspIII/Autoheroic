using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Rocket : Base
    {
        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Vector2 impact = (Vector2)_Data[1];
            Vector3 target = new Vector3(impact.x, -0.35f, impact.y);

            C0.transform.LookAt(impact);

            //C0.gameObject.GetComponent<Animator>().SetTrigger("Attack_Rocket");

            GameObject B = UnityEngine.Object.Instantiate(Library.Get_Bullet()) as GameObject;

            TrailRenderer tr = (TrailRenderer)B.GetComponent("TrailRenderer");
            tr.material.color = Color.green;

            B.transform.position = C0.transform.position;

            iTween.MoveTo(B, iTween.Hash("position", target, "time", 0.5F, "easetype", iTween.EaseType.linear));

            Cub.View.NarratorController.DisplayText(Desc, 2.0f);

            GameObject.Destroy(B, 2.0F);

            return 0.5F;
        }
    }
}
