using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Cub.Interface
{

    public class TitleScreenController : MonoBehaviour
        //: OptionsListController
    {
        public MasterGameController MasterController;

        void Update()
        {
            if (GetInput("Click") > 0.5f || GetInput("Ready") > 0.5f)
                MasterController.GotoFightScreen();
            //if (GetInput("Escape") > 0.5f)
        }

        protected float GetInput(string axis)
        {
            float r = 0;
            r += Input.GetAxis(axis + " P1");
            r += Input.GetAxis(axis + " P2");
            return r;
        }

        //protected override void MoreUpdate()
        //{

        //}

        //protected override void Click()
        //{
        //    switch (Selected.Option)
        //    {
        //        case MenuOptions.MMFight:
        //            MasterController.GotoFightScreen();
        //            break;

        //        case MenuOptions.MMEditTeam:
        //            MasterController.GotoEditScreen();
        //            break;

        //        case MenuOptions.MMQuit:
        //            Application.Quit();
        //            break;
        //    }
        //}
    }
}