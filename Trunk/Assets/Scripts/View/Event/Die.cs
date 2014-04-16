using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Die : Base
    {
        private const float Timespan = 5.0F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);

            C.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Die");

            Cub.View.Kamera.Follow(C.gameObject);

            Cube[] CL = C.GetComponentsInChildren<Cube>();

            foreach (Cube CO in CL)
            {
                CO.Fall(UnityEngine.Random.Range(0.0F, Timespan));
            }

            //Rigidbody[] RL = C.gameObject.transform.GetComponentsInChildren<Rigidbody>(true);
            //BoxCollider[] BL = C.gameObject.transform.GetComponentsInChildren<BoxCollider>(true);

            //foreach (Rigidbody R in RL)
            //{
            //    R.useGravity = true;
            //    R.AddForce(new Vector3(UnityEngine.Random.Range(-2, 2), 0, UnityEngine.Random.Range(-2, 2)), ForceMode.Impulse);
            //}

            //foreach (BoxCollider B in BL)
            //{
            //    B.enabled = true;
            //}
            
            C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Die));
            Runtime.Remove_Character((Guid)_Data[0]);

            Cub.View.NarratorController.DisplayText(Desc, Timespan);

            GameObject.Destroy(C.gameObject, Timespan);

            return Timespan;
        }
    }
}
