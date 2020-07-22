using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minorotate : MonoBehaviour
{
    private Vector3 _Rotation;
    public float speed = 5.0f;
    public float rotAngle = 360f;
    private float variation;
    // Start is called before the first frame update
    void Start()
    {
        variation = rotAngle / speed;
    }

    // Update is called once per frame
    void Update()
    {
        _Rotation = gameObject.transform.localEulerAngles;
        // Debug.Log(_Rotation);
        gameObject.transform.localEulerAngles += new Vector3 (0.0f, variation * Time.deltaTime, 0.0f);
    }
}
