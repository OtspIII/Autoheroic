using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Tool
{
    public static class Main
    {
        //public static Cub.View.SetupData SData { get; set; }
        public static List<Cub.Tool.Team> List_Team { get; set; }

        public static int Turn { get; private set; }

        public static void Initialization(Team teamOne, Team teamTwo)
        {
            Cub.Tool.Library.Initialization();

            Turn = 0;

            List_Team = new List<Team>{teamOne,teamTwo};
            //for (int i = 0; i < Cub.Model.Library.Stage_Terrain.Length; i++)
            //    for (int j = 0; j < Cub.Model.Library.Stage_Terrain[i].Length; j++)
            //        if (Cub.Model.Library.Stage_Unit[i][j] != Type.Class.None)
            //        {
            //            Cub.Model.Character C = new Cub.Model.Character(Cub.Model.Library.Stage_Unit[i][j], i, j);
            //            List_Team[0].Add_Character(C);
                        
            //        }
            //ForgeSetupData();
        }

        public static void StartGameplay(){
            foreach (Team t in List_Team)
                    foreach (Character c in t.Return_List_Character())
                        t.TotalValue += c.Value;                
        }

        public static List<Cub.View.Eventon> Go()
        {
            List<Cub.View.Eventon> GEL = new List<View.Eventon>();
            //List<Cub.Model.Character> CL = List_Team[0].Return_List_Character();
            foreach (Team team in Cub.Tool.Main.List_Team)
            {
                List<Cub.Tool.Character> CL = team.Return_List_Character();
                if (GameStillRunning())
                {
                    //We should build an action queue so not all actions take the same time. . .but not right now.
                    Turn++;

                    int Index = 0;
                    while (Index < CL.Count)
                    {
                        Cub.Tool.Character C = CL[Index];
                        C.Stat.Cooldown -= 1;
                        if (C.Stat.Cooldown <= 0)
                        {
                            List<Cub.View.Eventon> EL = C.Go();
                            GEL.AddRange(EL);
                        }
                        Index++;
                    }
                }
            }
            return GEL;
        }

        public static void Dispose(Cub.Tool.Character C, List<Cub.View.Eventon> events)
        {
            foreach (Team team in List_Team)
            {
                team.Remove_Character(C);
                if (team.Return_List_Character().Count == 0)
                {
                    GameEnds(events);
                }
            }
        }

        public static bool GameStillRunning()
        {
            foreach (Team team in List_Team)
                if (team.Return_List_Character().Count == 0)
                    return false;
            return true;
        }

        public static List<Character> AllCharacters()
        {
            List<Character> r = new List<Character>();
            foreach (Team team in List_Team)
                r.AddRange(team.Return_List_Character());
            return r;
        }

        public static void GameEnds(List<Cub.View.Eventon> events)
        {
            Debug.Log("Game Over");
            events.Add(new View.Eventon(Event.Win, "Game Over!", new List<object>()));
            int highScore = -999999999;
            List<Team> winningTeams = new List<Team>();
            foreach (Team team in List_Team)
            {
                if (team.Return_List_Character().Count > 0)
                    team.AddScore("Avoided Teamwipe", 50);
                int score = team.Score;
                Debug.Log("### " + team.Name + " ### (" + team.TotalValue + " Total Value)");
                foreach (Score st in team.ScoreThings)
                    Debug.Log("-" + st.Name + ": " + st.Value + " points");
                Debug.Log("---Total: " + score + " points---");
                if (highScore < score)
                {
                    winningTeams.Clear();
                }
                if (highScore <= score){
                    winningTeams.Add(team);
                    highScore = score;
                }
            }
            if (winningTeams.Count == 1)
                Debug.Log(winningTeams[0].Name + " Wins!");
            else
                Debug.Log("TIE!");
        }
    }
}
