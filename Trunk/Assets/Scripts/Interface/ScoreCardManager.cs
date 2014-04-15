using UnityEngine;
using System.Collections;

public class ScoreCardManager : MonoBehaviour {

    ScoreNameController TeamOneNamer;
    ScoreNameController TeamTwoNamer;
    ScoreScrController TeamOneScores;
    ScoreScrController TeamTwoScores;
    UILabel WinnerName;

	// Use this for initialization
	void Awake () {
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team One Name":
                    TeamOneNamer = (ScoreNameController)child.gameObject.GetComponent("ScoreNameController");
                    break;
                case "Team Two Name":
                    TeamTwoNamer = (ScoreNameController)child.gameObject.GetComponent("ScoreNameController");
                    break;
                case "Team One Score":
                    TeamOneScores = (ScoreScrController)child.gameObject.GetComponent("ScoreScrController");
                    break;
                case "Team Two Score":
                    TeamTwoScores = (ScoreScrController)child.gameObject.GetComponent("ScoreScrController");
                    break;
                case "Winner Name":
                    WinnerName = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}

    public void Imprint(Cub.Model.Team t1, Cub.Model.Team t2)
    {
        TeamOneNamer.Imprint(t1, "FF0000");
        TeamOneScores.Imprint(t1);
        TeamTwoNamer.Imprint(t2,"00FF00");
        TeamTwoScores.Imprint(t2);
        int S1 = t1.Score;
        int S2 = t2.Score;
        if (S1 > S2)
            WinnerName.text = "[FF0000]" + t1.Name + "[-] Wins!";
        else if (S1 < S2)
            WinnerName.text = "[00FF00]" + t2.Name + "[-] Wins!";
        else
            WinnerName.text = "Tie!";
    }
}
