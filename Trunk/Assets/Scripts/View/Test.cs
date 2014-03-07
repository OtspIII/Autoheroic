using System;
using UnityEngine;

namespace Cub.View
{
    public class Test : MonoBehaviour
    {
        public void Awake()
        {
            Cub.View.Library.Initialization();
        }

        public void Update()
        {
            if(Input.anyKeyDown)
            {
                foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Cube"))
                {
                    Destroy(GO);
                    
                }
                Debug.Log("D");
                Library.Yo();
                Library.Initialization();
                GameObject.Find("Knight").GetComponent<Character>().Start();
            }
        }

        public void Start()
        {
            
        }
    }
}
