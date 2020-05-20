using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field : MonoBehaviour
{
    public Material set_material;
    private int field_x = 5;
    private int field_y = 10;
    private int field_z = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (var z = 0; z < field_z; z++)
        {
            for (var y = 0; y < field_y; y++)
            {
                for (var x = 0; x < field_x; x++)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x, y, z);
                    //cube.AddComponent<Rigidbody>();
                    //Renderer renderer = GetComponent<Renderer>();
                    //cube.AddComponent<Renderer>();
                    cube.GetComponent<Renderer>().material = set_material;
                    cube.GetComponent<Collider>().isTrigger = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
