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
            Debug.Log(Kamera._Camera);
        }

        public static void Shake()
        {
            iTween.ShakePosition(_Camera, new Vector3(0.2F, 0.2F, 0.2F), 0.2F);
            Debug.Log("FDSA");
        }

        public static void Follow(GameObject GO)
        {
            iTween.MoveTo(_Camera, iTween.Hash("position", GO.transform.position + new Vector3(5, 5, 5), "time", 2.0F, "looktarget", GO.transform.position, "easetype", iTween.EaseType.linear));
        }
    }
}
