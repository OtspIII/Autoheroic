using System;
using UnityEngine;

namespace Cub.View
{
    public class Cube : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Plane")
            {
                iTween.ScaleTo(this.gameObject, Vector3.zero, 3.0F);
                Destroy(this.gameObject, 3.0F);
            }
        }
    }
}
