using System;
using UnityEngine;

namespace Cub.View
{
    public class Part : MonoBehaviour
    {
        public void Idle(float _After)
        {
            Invoke("_Idle", _After);
            //Debug.Log("Bang!");
        }

        private void _Idle()
        {
            //Debug.Log("Bang!");
            this.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
