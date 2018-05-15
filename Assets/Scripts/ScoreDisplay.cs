using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public static float score;
    public GameObject ScoreText;
    public GameObject MessageText;

    private float time;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time < 2)
            time += Time.deltaTime;
        else
        {
            DisplayScore(score);
        }
    }

    public void DisplayScore(float score)
    {
        ScoreText.GetComponent<Text>().text = "Your Score is " + score;
        
    }
}
