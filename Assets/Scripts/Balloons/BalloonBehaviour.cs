using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BalloonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    static private JimmyBehaviour Jimmy = null;

    static public void setJimmyBehaviour(JimmyBehaviour j)
    {
        Jimmy = j;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("balloon: OnTriggerEnter2D");
    //     // collision.gameObject.transform.position = Vector3.zero;
    //     if (collision.gameObject.name.Contains("obstacle"))
    //     {
    //         DestroyThisBalloon();
    //     }
    // }


    // private void DestroyThisBalloon()
    // {
    //     // Watch out!! a collision with overlap objects (e.g., two objects at the same location 
    //     // will result in two OnTriggerEntger2D() calls!!
    //     
    //     if (sJimmy.LoseBalloon(gameObject))
    //     {
    //         Destroy(transform.gameObject);
    //         Debug.Log("Calling Destroy: " + name);
    //     }
    // }
}