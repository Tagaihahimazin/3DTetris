using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public static int[,] stagedata;

    // setter
    public static void setStageData(int[,] data)
    {
        stagedata = data;
    }

    // getter
    public static int[,] getStageData()
    {
        return stagedata;
    }


    void Start()
    {
        DontDestroyOnLoad(this);
    }
}