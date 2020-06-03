using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject field_obj;

    private field field_script;
    // Start is called before the first frame update
    void Start()
    {
        field_script = field_obj.GetComponent<field>();
        field_script.create_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
