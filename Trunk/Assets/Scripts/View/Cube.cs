using System;
using UnityEngine;

namespace Cub.View
{
    public class Cube : MonoBehaviour
    {
        private const float Timespan = 2.0F;

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
