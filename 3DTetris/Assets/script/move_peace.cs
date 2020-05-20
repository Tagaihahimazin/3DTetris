using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class move_peace : MonoBehaviour
{
    private float timeElapsed;
    private bool move_flag = false;
    private Vector3 Rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Rotation = this.gameObject.transform.eulerAngles;
        if (move_flag == false)
        {
            if (timeElapsed >= 2)
            {
                //this.gameObject.transform.Translate(Vector3.down, );
                this.gameObject.transform.position += Vector3.down;
                timeElapsed = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A");
                //this.gameObject.transform.eulerAngles = Rotation + Vector3.forward * 90;
                this.gameObject.transform.Rotate(Vector3.forward * 90, Space.World);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("W");
                //this.gameObject.transform.eulerAngles = Rotation + Vector3.right * 90;
                this.gameObject.transform.Rotate(Vector3.right * 90, Space.World);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("S");
                //this.gameObject.transform.eulerAngles = Rotation + Vector3.forward * 90;
                this.gameObject.transform.Rotate(Vector3.left * 90, Space.World);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D");
                //this.gameObject.transform.eulerAngles = Rotation + Vector3.left * 90;
                this.gameObject.transform.Rotate(Vector3.back * 90, Space.World);
            }
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