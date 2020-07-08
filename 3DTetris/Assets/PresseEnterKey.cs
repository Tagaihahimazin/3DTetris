using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresseEnterKey : MonoBehaviour
{
    float alpha;
    float t = 0;
    bool flag = true;
    public float spead = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        alpha = gameObject.GetComponent<Image>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha == 1.0f)
        {
            flag = true;
            t = 0.0f;
        }
        else if(alpha == 0.0f)
        {
            flag = false;
            t = 0.0f;
        }
        t += Time.deltaTime * spead;
        if (flag == true)
        {
            alpha = Mathf.SmoothStep(1.0f, 0.0f, t);
        }
        else
        {
            alpha = Mathf.SmoothStep(0.0f, 1.0f, t);
        }
        gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
        
    }
}
