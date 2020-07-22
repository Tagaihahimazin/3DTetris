using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
	[SerializeField] GameObject startPanel;
	[SerializeField] GameObject selectPanel;
	
    // Start is called before the first frame update
    void Start()
    {
        ShowStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
			ShowSelect();
            Debug.Log("Space key was pressed.");
        }
    }
	
	void ShowStart()
	{
		startPanel.SetActive(true);
        selectPanel.SetActive(false);
	}
	
	void ShowSelect()
	{
		startPanel.SetActive(false);
        selectPanel.SetActive(true);
	}
}
