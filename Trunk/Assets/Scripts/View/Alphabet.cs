using System;
using UnityEngine;

namespace Cub.View
{
    public static class Alphabet
    {
        private const float Timespan = 1F;

        public static GameObject Create(string _Sentence, Vector3 _Position, Vector3 _Rotation, Vector3 _Scale, Material _Material)
        {
            GameObject GO = new GameObject();
            GO.transform.position = _Position;
            GO.transform.rotation = Quaternion.Euler(_Rotation);
            GO.transform.localScale = _Scale;

            Vector3 _Pointer = Vector3.zero;

            foreach (char _C in _Sentence)
            {
                Cubon _Cubon = Library.Get_Alphabet(_C);

                GameObject _GO = new GameObject();
                _GO.transform.parent = GO.transform;
                _GO.transform.localPosition = _Pointer;
                _GO.transform.localRotation = Quaternion.identity;
                _GO.transform.localScale = new Vector3(1, 1, 1);

                foreach (Vector3 V in _Cubon.Position)
                {
                    GameObject _Cube = UnityEngine.GameObject.Instantiate(Library.Get_Cube()) as GameObject;
                    _Cube.renderer.material = _Material;

                    _Cube.transform.parent = _GO.transform;
                    _Cube.transform.localPosition = V;
                    _Cube.transform.localRotation = Quaternion.identity;
                    _Cube.transform.localScale = new Vector3(0.9F, 0.9F, 0.9F);
                }

                _Pointer -= new Vector3(4, 0, 0);
            }

            return GO;
        }
    }
}
