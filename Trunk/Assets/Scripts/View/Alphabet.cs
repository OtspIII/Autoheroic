using System;
using UnityEngine;

namespace Cub.View
{
    public static class Alphabet
    {
        private const float Timespan = 1F;

        public static void Create(char _Char, Vector3 _Position, Vector3 _Rotation, Material _Material)
        {
            Cubon _Cubon = Library.Get_Alphabet(_Char);

            GameObject _GO = new GameObject();
            _GO.transform.position = _Position;
            _GO.transform.rotation = Quaternion.Euler(_Rotation);

            foreach (Vector3 V in _Cubon.Position)
            {
                GameObject _Cube = UnityEngine.GameObject.Instantiate(Library.Get_Cube()) as GameObject;
                _Cube.renderer.material = _Material;

                _Cube.transform.parent = _GO.transform;
                _Cube.transform.localPosition = V;
                _Cube.transform.localRotation = Quaternion.identity;
                _Cube.transform.localScale = new Vector3(0.9F, 0.9F, 0.9F);
            }
        }

        public static void Create(string _Sentence, Vector3 _Position, Vector3 _Rotation, Material _Material)
        {
            Vector3 _Pointer = _Position;

            foreach (char _C in _Sentence)
            {
                Create(_C, _Pointer, _Rotation, _Material);
                _Pointer -= new Vector3(4, 0, 0);
            }
        }
    }
}
