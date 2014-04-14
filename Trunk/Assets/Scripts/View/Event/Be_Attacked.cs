using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Be_Attacked : Base
    {
        private const float Timespan = 1.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);

            C.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Be_Attacked");

            C.BroadcastMessage("Idle", Timespan, SendMessageOptions.DontRequireReceiver);

            //We should redo all of this, or just turn it on when we can figure out how to optimize it better----------------------------------------

            //int A = (int)_Data[1] * 5;

            //Rigidbody[] RL = C.gameObject.transform.GetComponentsInChildren<Rigidbody>(true);

            //while (A > 0)
            //{
            //    GameObject GO = RL[UnityEngine.Random.Range(0, RL.Length)].gameObject;

            //    GO.rigidbody.useGravity = true;
            //    GO.rigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-5F, 5F), 0, UnityEngine.Random.Range(-5F, 5F)), ForceMode.Impulse);
            //    GO.GetComponent<BoxCollider>().enabled = true;
                
            //    //GOL[I].GetComponent<TrailRenderer>().enabled = true;

            //    A--;
            //}

            return Timespan;
        }
    }
}
