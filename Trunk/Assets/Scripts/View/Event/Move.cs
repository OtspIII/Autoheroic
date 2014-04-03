using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Move : Base
    {
        private const float Timespan = 1.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);
            int X = (int)_Data[1];
            int Z = (int)_Data[2];

            //Animator AF = C.gameObject.GetComponent<Animator>();            
            //AF.SetTrigger("Move");

            Animator A_Head = C.transform.FindChild("Head").GetComponent<Animator>();
            Animator A_Body = C.transform.FindChild("Body").GetComponent<Animator>();
            Animator A_Arms_Left = C.transform.FindChild("Arms_Left").GetComponent<Animator>();
            Animator A_Arms_Right = C.transform.FindChild("Arms_Right").GetComponent<Animator>();
            Animator A_Legs_Left = C.transform.FindChild("Legs_Left").GetComponent<Animator>();
            Animator A_Legs_Right = C.transform.FindChild("Legs_Right").GetComponent<Animator>();

            A_Head.SetTrigger("Move");
            A_Body.SetTrigger("Move");
            A_Arms_Left.SetTrigger("Move");
            A_Arms_Right.SetTrigger("Move");
            A_Legs_Left.SetTrigger("Move");
            A_Legs_Right.SetTrigger("Move");

            C.transform.LookAt(new Vector3(X, 0, Z));

            iTween.MoveTo(C.gameObject, iTween.Hash("position", new Vector3(X, 0, Z), "time", Timespan, "easetype", iTween.EaseType.linear));

            return Timespan;
        }
    }
}
