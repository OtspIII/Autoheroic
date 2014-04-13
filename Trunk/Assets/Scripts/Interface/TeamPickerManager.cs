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
        int TeamsOffset = 0;
        public GameObject UpMarker;
        public GameObject DownMarker;

        public void Setup(List<Cub.Model.TeamSave> teams)
        {
            Teams = teams;
            MarkTeamButtons();
        }

        void MarkTeamButtons()
        {
            int n = -1 + TeamsOffset;
            foreach (MenuChoiceController choice in Options)
            {
                TeamButtonController tbc = (TeamButtonController)choice;
                if (n == -1)
                    tbc.SetupAdder();
                else if (n >= Teams.Count || n < -1)
                    tbc.Setup(null);
                else
                    tbc.Setup(Teams[n]);
                n++;
            }
            if (TeamsOffset > 0)
                UpMarker.SetActive(true);
            else
                UpMarker.SetActive(false);
            if (TeamsOffset + Options.Count - 2 < Mathf.Max(6, Teams.Count - 1))
                DownMarker.SetActive(true);
            else
                DownMarker.SetActive(false);
        }

        protected override void ChangeSelection(int n)
        {
            int current = Options.IndexOf(Selected);
            current += n;
            if (current >= Options.Count)
            {
                TeamsOffset++;
                if (TeamsOffset + Options.Count - 2 >= Mathf.Max(6,Teams.Count))
                {
                    TeamsOffset--;
                }
                MarkTeamButtons();
                current = Options.Count - 1;
            }
            else if (current < 0)
            {
                TeamsOffset--;
                if (TeamsOffset < 0)
                    TeamsOffset = 0;
                MarkTeamButtons();
                current = 0;
            }
            //-15,-7
            Vector3 offset = Selected.gameObject.transform.position - SelectMarker.transform.position;
            Selected = Options[current];
            SelectMarker.transform.position = Selected.gameObject.transform.position - offset;
            OnSelectChange();
        }

        protected override void Click()
        {
            if (!((TeamButtonController)Selected).Adder)
            {
                //((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.05f, 0.9f, 0.05f, 0.8f);
                //Chosen = true;
                GM.EditTeam(SelectedTeam, this);
            }
            else
            {
                Debug.Log("ADD A TEAM");
            }
        }

        protected override void OnSelectChange()
        {
            ((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.9f, 0.05f, 0.05f, 0.8f);
            Chosen = false;
        }
    }
}