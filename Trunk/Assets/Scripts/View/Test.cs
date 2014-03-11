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

        public void Start()
        {
            GameObject Knight = Instantiate(Cub.View.Library.Get_Character(), Vector3.zero, Quaternion.identity) as GameObject;
            Knight.name = "Knight";
            Cub.View.Character C = Knight.GetComponent<Cub.View.Character>();
            C.Initialize_Stat(Guid.NewGuid(), Class.Knight, 5, 5, new Position2(0, 0));
            C.Initialize_Model();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Destroy(GameObject.Find("Knight"));

                Cub.View.Library.Yo();
                Cub.View.Library.Initialization();

                this.Start();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject Knight = GameObject.Find("Knight");
                Animator A = Knight.GetComponent<Animator>();
                
            }
        }
    }
}
