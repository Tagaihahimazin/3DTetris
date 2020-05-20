using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class move_peace : MonoBehaviour
{
    private float timeElapsed;
    private bool move_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 2 && move_flag == false)
        {
            this.gameObject.transform.Translate(Vector3.down);
            timeElapsed = 0.0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        move_flag = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(collision.gameObject.name + "Enter"); // ログを表示する
    }
    void OnCollisionStay(Collision collision)
    {
        //move_flag = true;
        //Debug.Log(collision.gameObject.name + "Stay"); // ログを表示する
    }
    void OnCollisionExit(Collision collision)
    {
        //move_flag = true;
        //Debug.Log(collision.gameObject.name + "Exit"); // ログを表示する
    }
}