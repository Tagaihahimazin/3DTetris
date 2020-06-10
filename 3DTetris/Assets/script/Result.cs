using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject score_obj;
    public GameObject high_score_obj;
    // Start is called before the first frame update
    void Start()
    {
        score_obj.GetComponent<Text>().text = "3000";
        high_score_obj.GetComponent<Text>().text = "3500";
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
