using System;
using UnityEngine;

namespace Cub.View
{
    public class Rocket : MonoBehaviour
    {
        private Vector3 T { get; set; }

        private bool Falling { get; set; }

        public void Pump(Vector3 Target)
        {
            this.transform.LookAt(this.transform.position + new Vector3(0, 10, 0));

            iTween.MoveTo(this.gameObject, this.transform.position + new Vector3(0, 10, 0), 2.0F);

            this.T = Target;

            this.Falling = false;

            Invoke("Drop", 2.0F);
        }

        public void Drop()
        {
            this.transform.LookAt(T);

            this.Falling = true;

            iTween.MoveTo(this.gameObject, T + new Vector3(0, -2, 0), 2.0F);
        }

        public void Update()
        {
            if (this.transform.position.y <= 0.5F && this.Falling)
            {
                Splash();
                Falling = false;
            }
        }

        public void Splash()
        {
            Cube[] CLO = this.gameObject.transform.GetComponentsInChildren<Cube>();

            foreach (Cube CO in CLO)
            {
                CO.transform.parent = null;

                CO.gameObject.AddComponent<Rigidbody>();
                CO.gameObject.AddComponent<BoxCollider>();

                CO.rigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-5F, 5F), 0, UnityEngine.Random.Range(-5F, 5F)), ForceMode.Impulse);
            }

            Instantiate(Library.Get_Explosion(), this.transform.position, Quaternion.identity);
        }
    }
}
