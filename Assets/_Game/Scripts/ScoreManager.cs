using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text multiplierText;

    private long score;
    
    private int multiplier = 1;
    private float timeSinceLastMultiplierIncrease;
    private int multiplierIncreaseAmount;

    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void AddScore(float num) {
        IncreaseMultiplier();
        score += (long)num*multiplier;
        scoreText.text = score.ToString("n0"); //number with commas 0 decimal places
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
    }

    private void IncreaseMultiplier() {
        if(Time.time - timeSinceLastMultiplierIncrease < 1) 
        {
            multiplierIncreaseAmount *= 2;
            multiplier += multiplierIncreaseAmount;
            
        } else {
            multiplier += 1;
            timeSinceLastMultiplierIncrease = Time.time;
            multiplierIncreaseAmount = 1;
        }
        if(multiplier > 9000 || multiplier < 0 ) {
            multiplierIncreaseAmount = 1;
            multiplier = 9001;
        } 
        multiplierText.text = "X"+multiplier.ToString();
    }
}
