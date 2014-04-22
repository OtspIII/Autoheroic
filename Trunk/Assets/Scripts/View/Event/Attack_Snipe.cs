using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Snipe : Base
    {
        public const float Timespan = 0.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.transform.LookAt(C1.transform.position);

            GameObject W = UnityEngine.Object.Instantiate(Library.Get_Warhead()) as GameObject;

            W.transform.position = C0.transform.position;

            iTween.MoveTo(W, iTween.Hash("position", C1.transform.position, "time", Timespan, "easetype", iTween.EaseType.linear));

            //Cub.View.NarratorController.DisplayText(Desc, 2.0f);
            C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Attack_Snipe));

            Cub.View.Indicator.Generate(C0.Stat.Position, C1.Stat.Position);

            GameObject.Destroy(W, Timespan);

            return Timespan;
        }
    }
}
