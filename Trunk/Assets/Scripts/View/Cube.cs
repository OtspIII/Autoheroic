using System;
using UnityEngine;

namespace Cub.View
{
    public class Cube : MonoBehaviour
    {
        private const float Timespan = 2.0F;

        public void Fall(float _After)
        {
            Invoke("_Fall", _After);
        }

        private void _Fall()
        {
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.AddComponent<BoxCollider>();

            this.transform.parent = null;

            this.rigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-5F, 5F), 0, UnityEngine.Random.Range(-5F, 5F)), ForceMode.Impulse);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                iTween.ScaleTo(this.gameObject, Vector3.zero, Timespan);
                Destroy(this.gameObject, Timespan);
            }
        }
    }
}
