using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using UnityEngine;

public class create_piece: MonoBehaviour
{

    //public GameObject JminoPrefab;
    //public GameObject JminoObj;
    public GameObject[] pieces=new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        create_p();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void create_p()
    {
        GameObject element = pieces[Random.Range(0, pieces.Length)];
        Instantiate(element, new Vector3(3,10,3), Quaternion.identity);
    }
}
