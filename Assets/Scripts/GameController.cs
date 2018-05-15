using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject backGround;
    public GameObject scoreText;
    public GameObject timeText;
    public GameObject messageText;
    public GameObject playerInstance; 

    public int stageNum;
    public float minuteLeft; //残り時間


    private float startWaitTime = 3;
    private float endWaitTime = 3;
    public float score;
    public int currentStageNum;
    private bool isPlaying;
    private bool isGameOver;
    private bool isGoal;


    // Use this for initialization
    void Start()
    {
        isGameOver = false;
        isGoal = false;
        score = 0;
        StartCoroutine("GameLoop");
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(StageStarting());

        yield return StartCoroutine(Playing());

        yield return StartCoroutine(Ending());

       
        //if (currentStageNum == stageNum)
        //{
        //    AllClear();
        //}

        StartCoroutine(GameLoop());

    }
    private IEnumerator StageStarting()
    {
        currentStageNum++;
        isGameOver = false;
        isGoal = false;
        messageText.GetComponent<Text>().text = "Stage " + currentStageNum;

        playerInstance = Instantiate(player, new Vector3(-9.0f, 0, 0), Quaternion.identity) as GameObject;
        playerInstance.transform.Rotate(Vector3.forward, 270);
        CameraController cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        cam.player = playerInstance;

        yield return new WaitForSeconds(startWaitTime);
    }
    private IEnumerator Playing()
    {
        score = 0;
        messageText.GetComponent<Text>().text = "";
        scoreText.GetComponent<Text>().text = "Score : " + score;
        GameObject.FindWithTag("Player").SendMessage("Init");
        isPlaying = true;
        minuteLeft = 60;
        score = 0;
        while (!isGameOver && !isGoal)
        {

            if (isPlaying)
            {
                if (minuteLeft > 0)
                {
                    AddTime(-Time.deltaTime);
                    timeText.GetComponent<Text>().text = "Time : " + (int)minuteLeft;
                }
                else
                {
                    playerInstance.GetComponent<PlayerMover>().Explosion();
                }
            }
            yield return null;
        }
        //yield return null;
    }
    private IEnumerator Ending()
    {
        isPlaying = false;
        if (isGoal)
        {
            messageText.GetComponent<Text>().text = "Stage " + currentStageNum + " is Cleared!!";
        }
        playerInstance = null;
        yield return new WaitForSeconds(endWaitTime);
    }

    

    public void AddScore(float deltaScore)
    {
        score += deltaScore;
        scoreText.GetComponent<Text>().text = "Score : " + score;
    }

    public void AddTime(float deltaTime)
    {
        minuteLeft += deltaTime;
    }

    public void GameOver()
    {
        isGameOver = true;
        messageText.GetComponent<Text>().text = "GAME OVER";
        currentStageNum--;
    }

    public void Goal()
    {
        isGoal = true;
        AddScore(30);
        Destroy(playerInstance.gameObject);
        //Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL("テキスト #hashtag"));
    }

    private void AllClear()
    {
        messageText.GetComponent<Text>().text = "ALL CLEAR";
    }
}
