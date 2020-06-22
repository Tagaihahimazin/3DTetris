using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject score_obj;
    public GameObject high_score_obj;
    int score,highscore;
    
    // Start is called before the first frame update
    void Start()
    {
        score = field.score;
        highscore = field.highscore;
        if (score > highscore)
        {
            highscore = score;
        }
        score_obj.GetComponent<Text>().text = score.ToString();
        high_score_obj.GetComponent<Text>().text = highscore.ToString();
        field.highscore = highscore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("tagaitest");
        }
    }
}
