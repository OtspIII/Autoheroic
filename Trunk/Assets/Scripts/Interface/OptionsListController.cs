using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Cub.Interface
{

    public class OptionsListController : MonoBehaviour
    {
        public bool CurrentlyActive = false;

        public GameObject SelectMarker;

        public List<GameObject> OptionsRaw;
        protected List<MenuChoiceController> Options = new List<MenuChoiceController>();

        protected MenuChoiceController Selected;

        float SelectTimer = 0.2f;
        float Timer;

        // Use this for initialization
        void Start()
        {
            foreach (GameObject obj in OptionsRaw)
                Options.Add((MenuChoiceController)obj.GetComponent("MenuChoiceController"));
            Selected = Options[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (!CurrentlyActive)
                return;
            if (Timer > 0)
                Timer -= Time.deltaTime;

            if (Input.GetAxis("Vertical") > 0.1f && Timer <= 0)
            {
                ChangeSelection(-1);
                Timer = SelectTimer;
            }
            else if (Input.GetAxis("Vertical") < -0.1f && Timer <= 0)
            {
                ChangeSelection(1);
                Timer = SelectTimer;
            }
            else if (Timer > 0 && Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f)
                Timer = 0;
            MoreUpdate();
            if (Input.GetAxis("Click") == 1)
            {
                Click();
            }
        }

        protected virtual void MoreUpdate()
        {

        }

        protected virtual void Click()
        {

        }

        //void AddButton(GameObject obj, MenuOptions mmo)
        //{
        //    Options.Add(mmo);
        //    OptionsDict.Add(mmo, obj);
        //}

        void ChangeSelection(int n)
        {
            int current = Options.IndexOf(Selected);
            current += n;
            if (current >= Options.Count)
                current = 0;
            else if (current < 0)
                current = Options.Count - 1;
            //-15,-7
            Vector3 offset = Selected.gameObject.transform.position - SelectMarker.transform.position;
            Selected = Options[current];
            SelectMarker.transform.position = Selected.gameObject.transform.position - offset;
            OnSelectChange();
        }

        protected virtual void OnSelectChange()
        {

        }
    }

    public enum MenuOptions
    {
        MMFight,
        MMEditTeam,
        MMQuit,
        PickerTeam
    }
}