using System;
using UnityEngine;

namespace Cub.Scene
{
    public class Intro : MonoBehaviour
    {
        public void Awake()
        {
            Cub.View.Library.Initialization();
        }

        public void Start()
        {
            Material M = new Material(Cub.View.Library.Get_Material());
            M.color = new Color(1.0F, 0.8F, 0.0F);

            Cub.View.Alphabet.Create("WELCOME TO AUTOHEROIC", Vector3.zero, Vector3.zero, new Vector3(0.1F, 0.1F, 0.1F), M);
        }
    }
}
