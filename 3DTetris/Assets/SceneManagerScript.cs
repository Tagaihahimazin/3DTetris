using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
	[SerializeField] GameObject startPanel;
	[SerializeField] GameObject selectPanel;
    public Sprite[] spriteimages = new Sprite[4];
    public List<int[,]> stageinfo = new List<int[,]>();

    private int currentScenes;
    private int currentStage;

    // Scenesの状態
    private const int _START = 0;
    private const int _SELECT = 1;

    // fieldの状態
    private const int _NON = 0;
    private const int _BLOCK = 1;
    private const int _KABE = 2;
    private const int _FLOOR = 3;
    private const int _GEN = 4;

    // Start is called before the first frame update
    void Start()
    {
        StageGen();
        ShowStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScenes == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ShowSelect();
                UnityEngine.Debug.Log("Return key was pressed.");
            }
        }
        if (currentScenes == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentStage--;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentStage++;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                int[,] stagedata = stageinfo[currentStage];
                UnityEngine.Debug.Log("return");
                UnityEngine.Debug.Log(stagedata);
            }
        }
    }

	void ShowStart()
	{
        currentScenes = _START;
		startPanel.SetActive(true);
        selectPanel.SetActive(false);
	}
	
	void ShowSelect()
	{
        currentScenes = _SELECT;
        currentStage = 0;
		startPanel.SetActive(false);
        selectPanel.SetActive(true);
	}

    void StageGen()
    {
        stageinfo.Add(new int[7, 7]{{0,0,2,2,2,0,0},
                                    {0,0,2,2,2,0,0},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {0,0,2,2,2,0,0},
                                    {0,0,2,2,2,0,0} });

        stageinfo.Add(new int[7, 7]{{2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2},
                                    {2,2,2,2,2,2,2} });
    }
}
