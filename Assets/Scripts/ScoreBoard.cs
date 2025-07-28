using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int _score;
    [SerializeField] TMP_Text scoreText;

    public void IncreaseScore(int amountToIncrease)
    {
        _score += amountToIncrease;
        scoreText.text = "Score: " + _score.ToString();
        Debug.Log($"Score is now {_score}");
    }
}
