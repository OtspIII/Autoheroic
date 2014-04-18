using System;
using UnityEngine;

namespace Cub.View
{
    public class Kamera : MonoBehaviour
    {
        private static GameObject _Camera { get; set; }

        public void Start()
        {
            Kamera._Camera = GameObject.Find("Main Camera");

            //Kamera._Camera.transform.rotation = Quaternion.Euler(30, 225, 0);
        }

        public static void Shake()
        {
            iTween.ShakePosition(_Camera, new Vector3(0.2F, 0.2F, 0.2F), 0.2F);
        }

        public static void Follow(GameObject GO)
        {
            _Camera.transform.parent = GO.transform;

            _Camera.transform.localPosition = new Vector3(30, 100, 30);
            _Camera.transform.localRotation = Quaternion.Euler(60, 225, 0);

            

            //iTween.MoveTo(_Camera, iTween.Hash("position", GO.transform.position + new Vector3(2, 2, 2), "time", 1.0F, "easetype", iTween.EaseType.linear));
        }
    }
}
