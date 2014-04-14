using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cub.Interface
{
    public class TeamPickerManager : OptionsListController
    {
        bool AlreadySetup = false;
        public bool Chosen = false;
        public Cub.Model.TeamSave SelectedTeam { get { return ((TeamButtonController)Selected).Team; } }
        int TeamsOffset = 0;
        public GameObject UpMarker;
        public GameObject DownMarker;

        public void Setup()
        {
            if (AlreadySetup)
                return;
            AlreadySetup = true;
            MarkTeamButtons();
        }

        void Update()
        {
            if (!CurrentlyActive)
                return;
            if (Timer > 0)
                Timer -= Time.deltaTime;

            if (GetInput("Vertical") > 0.1f && Timer <= 0)
            {
                ChangeSelection(-1);
                Timer = SelectTimer;
            }
            else if (GetInput("Vertical") < -0.1f && Timer <= 0)
            {
                ChangeSelection(1);
                Timer = SelectTimer;
            }
            else if (Timer > 0 && Mathf.Abs(GetInput("Vertical")) < 0.1f)
                Timer = 0;
            MoreUpdate();
            if (GetInput("Click") > 0.5f)
            {
                if (!Clicking)
                {
                    Clicking = true;
                    Click();
                }
            }
            else if (GetInput("Escape") > 0.8f)
            {
                if (!Clicking)
                {
                    Clicking = true;
                    GM.MainMenu.gameObject.SetActive(true);
                    GM.LeftPicker.transform.localPosition =
                        new Vector3(-300, transform.localPosition.y, transform.localPosition.z);
                    GM.RightPicker.transform.localPosition =
                        new Vector3(300, transform.localPosition.y, transform.localPosition.z);
                    foreach (GameObject tile in GM.Blocks)
                        tile.transform.position += new Vector3(0, 20, 0);
                    GM.LeftPicker.CurrentlyActive = false;
                    GM.RightPicker.CurrentlyActive = false;
                }
            }
            else
                Clicking = false;
        }

        void MarkTeamButtons()
        {
            int n = -1 + TeamsOffset;
            foreach (MenuChoiceController choice in Options)
            {
                TeamButtonController tbc = (TeamButtonController)choice;
                if (n == -1)
                    tbc.SetupAdder();
                else if (n >= GM.Teams.Count || n < -1)
                    tbc.Setup(null);
                else
                    tbc.Setup(GM.Teams[n]);
                n++;
            }
            if (TeamsOffset > 0)
                UpMarker.SetActive(true);
            else
                UpMarker.SetActive(false);
            if (TeamsOffset + Options.Count - 2 < Mathf.Max(6, GM.Teams.Count - 1))
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
                if (TeamsOffset + Options.Count - 2 >= Mathf.Max(6, GM.Teams.Count))
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
                Model.TeamSave t = new Model.TeamSave("","");
                GM.Teams.Add(t);
                MarkTeamButtons();
                int slide = GM.Teams.IndexOf(t) - GM.Teams.IndexOf(SelectedTeam);
                for (int n = slide; n > 0;n--)
                    ChangeSelection(1);
                GM.EditTeam(t, this);
            }
        }

        protected override void OnSelectChange()
        {
            ((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.9f, 0.05f, 0.05f, 0.8f);
            Chosen = false;
        }
    }
}