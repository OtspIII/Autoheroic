using System;
using UnityEngine;

namespace Cub.View
{
    public class Kamera : MonoBehaviour
    {
        public void Shake()
        {
            iTween.ShakePosition(this.gameObject, new Vector3(0.2F, 0.2F, 0.2F), 0.2F);
            Debug.Log("FDSA");
        }

        public void Follow(GameObject GO)
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", GO.transform.position + new Vector3(5, 5, 5), "time", 2.0F, "looktarget", GO.transform.position, "easetype", iTween.EaseType.linear));
        }
    }
}
