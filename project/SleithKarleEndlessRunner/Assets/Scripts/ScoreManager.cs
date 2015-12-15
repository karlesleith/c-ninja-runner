using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCounter;

    public float pointsPerSecond;

    public bool scoreIncreasing;

	// Use this for initialization
	void Start () {
	    if(PlayerPrefs.HasKey("Hi-Score"))
        {
            hiScoreCounter = PlayerPrefs.GetFloat("Hi-Score");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if (scoreCount > hiScoreCounter)
        {
            hiScoreCounter = scoreCount;
            PlayerPrefs.SetFloat("Hi-Score", hiScoreCounter);
        }

        scoreText.text = "Score: " + Mathf.Round (scoreCount);
        hiScoreText.text= "Hi-Score: " + Mathf.Round (hiScoreCounter);
	}
}
