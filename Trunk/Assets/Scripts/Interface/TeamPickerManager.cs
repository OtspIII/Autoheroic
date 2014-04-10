using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cub.Interface
{
    public class TeamPickerManager : OptionsListController
    {

        List<Cub.Model.TeamSave> Teams;
        public bool Chosen = false;
        public Cub.Model.TeamSave SelectedTeam { get { return ((TeamButtonController)Selected).Team; } }

        public void Setup(List<Cub.Model.TeamSave> teams){
            Teams = teams;
            int n = -1;
            foreach (MenuChoiceController choice in Options)
            {
                TeamButtonController tbc = (TeamButtonController)choice;
                if (n == -1)
                    tbc.SetupAdder();
                else if (n >= Teams.Count)
                    tbc.Setup(null);
                else
                    tbc.Setup(Teams[n]);
                n++;
            }
        }

        protected override void Click()
        {
            ((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.05f, 0.9f, 0.05f, 0.8f);
            Chosen = true;
        }

        protected override void OnSelectChange()
        {
            ((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.9f, 0.05f, 0.05f, 0.8f);
            Chosen = false;
        }
    }
}