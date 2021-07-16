using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    static private JimmyBehaviour sJimmy = null;

    static public void setJimmyBehaviour(JimmyBehaviour j)
    {
        sJimmy = j;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("balloon: OnTriggerEnter2D");
        // collision.gameObject.transform.position = Vector3.zero;
        if (!(collision.gameObject.name.Contains("balloon")||collision.gameObject.name.Contains("Jimmy")))
        {
            DestroyThisBalloon();
        }

        

    }


    private void DestroyThisBalloon()
    {
        // Watch out!! a collision with overlap objects (e.g., two objects at the same location 
        // will result in two OnTriggerEntger2D() calls!!
        
        Destroy(transform.gameObject);
        Debug.Log("Calling Destroy: " + name);
        sJimmy.loseBalloon(gameObject);
        
    }
}
