using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //move_flag = true;
        Debug.Log(collision.gameObject.name + "Enter"); // ログを表示する
    }
    void OnCollisionStay(Collision collision)
    {
        //move_flag = true;
        Debug.Log(collision.gameObject.name + "Enter"); // ログを表示する
    }
    void OnCollisionExit(Collision collision)
    {
        //move_flag = true;
        Debug.Log(collision.gameObject.name + "Enter"); // ログを表示する
    }

}
