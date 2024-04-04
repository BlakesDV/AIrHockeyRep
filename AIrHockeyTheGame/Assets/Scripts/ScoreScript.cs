using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        AiScore, PlayerScore
    }

    public TMP_Text AiScoreTxt, PlayerScoreTxt;
    private int aiScore, playerScore;
    public UiManager uiManager;
    public int MaxScore;

    private int AiScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(true);
        }
    }

    private int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false);
        }
    }
    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
            AiScoreTxt.text = (++aiScore).ToString();
        else
            PlayerScoreTxt.text = (++playerScore).ToString();
    }

    public void ResetScores()
    {
        AiScore = PlayerScore = 0;
        AiScoreTxt.text = PlayerScoreTxt.text = "0";
    }
}
