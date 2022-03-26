using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    float time, startTime;
    int score;
    bool isEnd;
    Text timerText, scoreText;
    public GameObject gameoverLayer;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        scoreText = GameObject.Find("Canvas/Score").GetComponent<Text>();
        //gameoverLayer = GameObject.Find("Canvas/Over");
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            time = Time.time - startTime;
            int seconds = (int)(time % 60);
            int minutes = (int)(time / 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void AddScore(int num)
    {
        if (num == 3)
        {
            num *= 2;
        } else if (num >= 4)
        {
            num *= 3;
        }
        score += num;
        scoreText.text = score.ToString();
    }

    public void ShowGameover()
    {
        gameoverLayer.SetActive(true);
        isEnd = true;
    }

    public float GetGameSpeed()
    {
        float delta = Time.time - startTime;
        if (delta < 60 * 5)
        {
            return 1.0f;
        } else if (delta < 60 * 10)
        {
            return 0.8f;
        } else if (delta < 60 * 15)
        {
            return 0.5f;
        } else
        {
            return 0.2f;
        }
        
    }
}
