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
        }

        public void Start()
        {
            Tool.Character C1 = new Tool.Character(Class.Knight, 0, 0);

            Runtime.Add_Character(C1);

            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Move, "", new System.Collections.Generic.List<object>() { C1.ID, 2, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Move, "", new System.Collections.Generic.List<object>() { C1.ID, 1, 1 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Die, "", new System.Collections.Generic.List<object>() { C1.ID }));

            GameObject.Find("Runtime").GetComponent<Cub.View.Runtime>().Run_Eventon();
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
