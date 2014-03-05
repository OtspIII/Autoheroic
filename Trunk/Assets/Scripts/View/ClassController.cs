using UnityEngine;
using System.Collections;
using Cub;

namespace Cub.View
{
    public class ClassController : MonoBehaviour
    {

        public string Name;
		public System.Guid Id;
        public Cub.Class Class;
        GameObject deathSpray;
        public GameObject DeathSpray { get { return deathSpray; } }
        public GameObject DeathSprayClass;
		public string Projectile;
        public string Team;

        // Use this for initialization
        void Start()
        {
            deathSpray = (GameObject)Instantiate(DeathSprayClass, new Vector3(9999, 9999, 9199), Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ImprintFrom(CharController who)
        {
            Name = who.Name;
            Id = who.Id;
            Team = who.Team;
        }

        public void DoAnimation(Cub.Animation act)
        {
            switch (act)
            {
                case Cub.Animation.Walk:
                    {
                        //			particleSystem.startColor = Color.blue;
                        //			particleSystem.Emit(10);
                        break;
                    }
                case Cub.Animation.Attack:
                    {
                        particleSystem.startColor = Color.white;
                        particleSystem.Emit(10);
                        break;
                    }
                case Cub.Animation.TakeDamage:
                    {
                        particleSystem.startColor = Color.magenta;
                        particleSystem.Emit(5);
                        break;
                    }
                case Cub.Animation.Heal:
                    {
                        particleSystem.startColor = Color.yellow;
                        particleSystem.Emit(10);
                        break;
                    }
                case Cub.Animation.BeHealed:
                    {
                        particleSystem.startColor = Color.green;
                        particleSystem.Emit(5);
                        break;
                    }
                case Cub.Animation.Cheer:
                    {
                        particleSystem.startColor = Color.blue;
                        particleSystem.Emit(10);
                        break;
                    }
            }
        }
    }
}