using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Runtime : MonoBehaviour
    {
        private Dictionary<Guid, Cub.View.Character> Dictionary_Character { get; set; }
        
        private Queue<Eventon> Queue_Eventon { get; set; }

        public void Awake()
        {
            Dictionary_Character = new Dictionary<Guid, Cub.View.Character>();            
            Queue_Eventon = new Queue<Eventon>();
        }

        public void Add_Character(Cub.Tool.Character _Character)
        {
            GameObject GO = GameObject.Instantiate(Cub.View.Library.Get_Character(), _Character.Stat.Position.ToVector3(), Quaternion.identity) as GameObject;
            Cub.View.Character C = GO.GetComponent<Cub.View.Character>();
            C.Initialize_Stat(_Character.ID, _Character.Info.Class, _Character.Info.MHP, _Character.Stat.HP, _Character.Stat.Position);
            C.Initialize_Model();
            Dictionary_Character[_Character.ID] = C;
        }

        public void Remove_Character(Guid _ID)
        {
            Dictionary_Character.Remove(_ID);
        }

        public Cub.View.Character Get_Character(Guid _ID)
        {
            return Dictionary_Character[_ID];
        }

        public void Add_Eventon(Eventon _Eventon)
        {
            Queue_Eventon.Enqueue(_Eventon);
        }

        public void Add_Eventon(List<Eventon> _Eventon)
        {
            foreach (Eventon E in _Eventon)
            {
                Queue_Eventon.Enqueue(E);
            }
        }

        public void Run_Eventon()
        {
            if (Queue_Eventon.Count > 0)
            {
                Debug.Log("Hey");

                Eventon E = Queue_Eventon.Dequeue();

                float Delay = Cub.View.Library.Get_Event_Processor(E.Type).Process(E.Data);

                Invoke("Run_Eventon", Delay);
            }
            else
            {
               
            }
        }

    }
}
