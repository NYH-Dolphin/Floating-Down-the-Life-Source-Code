using System.Drawing;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private float speed = 100f;

    private readonly string[] _smallObstacles = {"obstacle_2", "obstacle_6", "obstacle_9"};
    private readonly string[] _midObstacles = {"obstacle_1", "obstacle_3", "obstacle_4", "obstacle_5"};
    private readonly string[] _largeObstacles = {"obstacle_7", "obstacle_8"};
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f, 1f) < 0.5)
        {
            Vector3 p = transform.position;
            if (p.x < 0)
            {
                string obsName = "";
                if (GetComponent<RectTransform>().rect.height < 300)
                    obsName = _smallObstacles[Random.Range(0, 3)];
                else if (GetComponent<RectTransform>().rect.height < 500)
                    obsName = _midObstacles[Random.Range(0, 4)];
                else
                    obsName = _largeObstacles[Random.Range(0, 2)];
                string path = "obstacles/left/" + obsName;
                GameObject obstacle = Instantiate(Resources.Load<GameObject>(path), transform, true);
                float halfWidth = obstacle.GetComponent<RectTransform>().rect.width / 2;
                obstacle.transform.localPosition = new Vector3(125 + halfWidth, 0, 0);
            }
            else
            {
                string obsName = "";
                if (GetComponent<RectTransform>().rect.height < 300)
                    obsName = _smallObstacles[Random.Range(0, 3)];
                else if (GetComponent<RectTransform>().rect.height < 500)
                    obsName = _midObstacles[Random.Range(0, 4)];
                else
                    obsName = _largeObstacles[Random.Range(0, 2)];
                string path = "obstacles/right/" + obsName;
                GameObject obstacle = Instantiate(Resources.Load<GameObject>(path), transform, true);
                float halfWidth = obstacle.GetComponent<RectTransform>().rect.width / 2;
                obstacle.transform.localPosition = new Vector3(-125 - halfWidth, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;
        p += (speed * Time.smoothDeltaTime) * new Vector3(0, 1, 0);
        transform.localPosition = p;
    }
    
}
