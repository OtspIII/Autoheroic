using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Kamera : MonoBehaviour
    {
        private static List<Vector3> _Path_Position { get; set; }
        private static List<Vector3> _Path_Rotation { get; set; }
        private static float _Path_Timespan { get; set; }

        public static GameObject _Camera { get; set; }

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
        }

        public static void Move(Vector3 _Position, Vector3 _Rotation, float _Timespan)
        {
            iTween.MoveTo(_Camera, iTween.Hash("position", _Position, "time", _Timespan, "easetype", iTween.EaseType.linear));
            iTween.RotateTo(_Camera, iTween.Hash("rotation", _Rotation, "time", _Timespan, "easetype", iTween.EaseType.linear));
        }

        public static void Walk()
        {
            iTween.MoveTo(_Camera, iTween.Hash("path", new Vector3[] { new Vector3(-6, 8, -6), new Vector3(14, 8, -6) }, "time", 180F, "easetype", iTween.EaseType.linear, "looktarget", new Vector3(5F, -0.5F, 3F), "looptype", iTween.LoopType.pingPong));
        }

        public static void Hoop(Vector3 _Target, float _Timespan)
        {
            iTween.LookTo(_Camera, _Target, _Timespan);
            //iTween.MoveTo(_Camera, new Vector3(-6, 8, -6), _Timespan);
        }

        public static void Restore()
        {
            iTween.MoveTo(_Camera, iTween.Hash("position", new Vector3(-6, 8, -6), "time", 0.5F, "looktarget", new Vector3(5F, -0.5F, 3F)));
        }
    }
}
