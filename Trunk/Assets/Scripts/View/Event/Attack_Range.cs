using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Range : Base
    {
        private const float Timespan = 0.1F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.transform.LookAt(new Vector3(C1.transform.position.x, 0, C1.transform.position.z));

            C0.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Attack_Range");            
            C0.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Attack_Range");

            C0.BroadcastMessage("Idle", Timespan + 0.5F, SendMessageOptions.DontRequireReceiver);

            GameObject B = UnityEngine.Object.Instantiate(Library.Get_Bullet()) as GameObject;

            B.GetComponent<TrailRenderer>().material.color = Color.yellow;

            B.transform.position = C0.transform.FindChild("Arms_Right").position;

            iTween.MoveTo(B, iTween.Hash("position", C1.transform.position, "time", Timespan, "easetype", iTween.EaseType.linear));

            GameObject.Destroy(B, Timespan + 0.5F);

            Cub.View.Kamera.Follow(C0.gameObject);

            Cub.View.NarratorController.DisplayText(Desc, Timespan);
            C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Attack_Range));

            return Timespan;
        }
    }
}
