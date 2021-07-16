using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyBehaviour : MonoBehaviour
{

    private ArrayList balloons = new ArrayList();
    private float speed = 500; // 这里做匀速直线运动
    public Animator animator;  // Animator 组件
    private int balloonHolds = 1;


    // Start is called before the first frame update
    void Start()
    {
        GameObject balloon = Instantiate(Resources.Load<GameObject>("Balloons/cyan_balloon"), gameObject.transform, true);
        balloon.transform.localPosition = new Vector3(0,5,0);
        Debug.Log("J:start");
        Debug.Log("J"+gameObject.transform);
        balloons.Add(balloon);
        
        BalloonBehaviour.setJimmyBehaviour(this);
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Jimmy: OnTriggerEnter2D");
        if (!collision.gameObject.name.Contains("balloon"))
        {
            DestroyBalloon();
        }

        
    }

    private void DestroyBalloon()
    {
        if (balloons.Count != 0)
        {
            GameObject balloon = (GameObject)balloons[0];
            balloons.RemoveAt(0);
            Destroy(balloon);
        }
        CheckTerminate();
    }

    void Float()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 p = transform.position;
        // 按动 A 键 - 向左移动
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("LEFT");
            // 位置移动
            p.x += moveX * speed * Time.deltaTime;
            // Animation Controller 的参数状态更新
            animator.SetBool("floatLeft",true);

            // 按动 D 键 - 向右移动
        }else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("RIGHT");
            p.x += moveX * speed * Time.deltaTime;
            animator.SetBool("floatLeft",false);
        }
        transform.position = p;
    }

    public void loseBalloon(GameObject balloon)
    {
        balloonHolds--;
        balloons.Remove(balloon);
        Debug.Log("loosing"+balloonHolds);
        CheckTerminate();
    }

    public void CheckTerminate()
    {
        
    }



}
