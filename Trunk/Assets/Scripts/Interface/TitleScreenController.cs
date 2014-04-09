using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Cub.Interface.MainMenu
{

    public class TitleScreenController : OptionsListController
    {
        public MasterGameController MasterController;

        protected override void MoreUpdate()
        {

        }

        protected override void Click()
        {
            switch (Selected.Option)
            {
                case MenuOptions.MMFight:
                    MasterController.GotoFightScreen();
                    break;

                case MenuOptions.MMEditTeam:
                    MasterController.GotoEditScreen();
                    break;

                case MenuOptions.MMQuit:
                    Application.Quit();
                    break;
            }
        }
    }
}