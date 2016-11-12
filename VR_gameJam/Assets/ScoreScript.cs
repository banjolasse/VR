using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Text myScoreText;
    [SerializeField]
    int myScore;
       
	void Start ()
    {
        myScore = 0;
        myScoreText = GetComponent<Text>();
	}
	
	void Update ()
    {
        myScoreText.text = myScore.ToString();
	}

    void AddScore(int aScore)
    {
        myScore += aScore;
    }
}
