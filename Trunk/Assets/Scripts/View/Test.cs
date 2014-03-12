using System;
using UnityEngine;

namespace Cub.View
{
    public class Test : MonoBehaviour
    {
        public void Awake()
        {
            Cub.Tool.Library.Initialization();
            Cub.View.Library.Initialization();

            Cub.View.Runtime RT = GameObject.Find("Runtime").GetComponent<Cub.View.Runtime>();
        }

        public void Start()
        {
            Tool.Character C1 = new Tool.Character(Class.Knight, 0, 0);
            Tool.Character C2 = new Tool.Character(Class.Knight, 0, 0);
            
            Cub.View.Runtime RT = GameObject.Find("Runtime").GetComponent<Cub.View.Runtime>();

            RT.Add_Character(C1);
            RT.Add_Character(C2);

            RT.Add_Eventon(new Cub.View.Eventon(Cub.Event.Move, "", new System.Collections.Generic.List<object>() { C1.ID, 4, 4 }));
            RT.Add_Eventon(new Cub.View.Eventon(Cub.Event.Move, "", new System.Collections.Generic.List<object>() { C2.ID, 1, 5 }));
            RT.Run_Eventon();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Destroy(GameObject.Find("Knight"));

                Cub.View.Library.Unlock();
                Cub.View.Library.Initialization();

                this.Start();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject Knight = GameObject.Find("Knight");
                Animator A = Knight.GetComponent<Animator>();
                A.SetTrigger("Move");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject Knight = GameObject.Find("Knight");
                Animator A = Knight.GetComponent<Animator>();
                A.SetTrigger("Idle");
            }
        }
    }
}
