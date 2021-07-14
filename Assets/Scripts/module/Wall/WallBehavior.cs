using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;
        p += (speed * Time.smoothDeltaTime) * new Vector3(0, 1, 0);
        transform.localPosition = p;
    }
    
}
