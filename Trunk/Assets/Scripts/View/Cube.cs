using System;
using UnityEngine;

namespace Cub.View
{
    public class Cube : MonoBehaviour
    {
        public CubeType CubeType = CubeType.Black;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Plane")
            {
                iTween.ScaleTo(this.gameObject, Vector3.zero, 3.0F);
                Destroy(this.gameObject, 3.0F);
            }
        }

        public void SetMaterial(CubeType ct, bool TeamOne)
        {
            CubeType = ct;
            renderer.material = Cub.View.Library.Get_Cube(CubeType,TeamOne);
        }
    }
}
