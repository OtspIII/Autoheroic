using System;
using UnityEngine;

namespace Cub.View
{
    public class Part : MonoBehaviour
    {
        public void Idle()
        {
            this.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
