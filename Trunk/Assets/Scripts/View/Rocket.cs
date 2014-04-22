﻿using System;
using UnityEngine;

namespace Cub.View
{
    public class Rocket : MonoBehaviour
    {
        private Vector3 T { get; set; }

        public void Pump(Vector3 Target)
        {
            this.transform.LookAt(this.transform.position + new Vector3(0, 10, 0));

            iTween.MoveTo(this.gameObject, iTween.Hash("position", this.transform.position + new Vector3(0, 10, 0), "time", Cub.View.Event.Attack_Rocket.Timespan / 2, "easetype", iTween.EaseType.linear));
                
            this.T = Target;

            Invoke("Drop", Cub.View.Event.Attack_Rocket.Timespan / 2);
        }

        public void Drop()
        {
            this.transform.LookAt(T);

            iTween.MoveTo(this.gameObject, iTween.Hash("position", T, "time", Cub.View.Event.Attack_Rocket.Timespan / 2, "easetype", iTween.EaseType.linear));

            Invoke("Splash", Cub.View.Event.Attack_Rocket.Timespan / 2);
        }

        public void Splash()
        {
            Vector3 V = new Vector3(this.transform.position.x, 0, this.transform.position.z);

            Instantiate(Library.Get_Explosion(), V, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.forward, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.back, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.left, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.right, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
