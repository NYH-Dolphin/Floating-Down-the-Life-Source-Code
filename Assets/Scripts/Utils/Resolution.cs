using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(900,1600,false);
        Debug.Log("Set resolution");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
