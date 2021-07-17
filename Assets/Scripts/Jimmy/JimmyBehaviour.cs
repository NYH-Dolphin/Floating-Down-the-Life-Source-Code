using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyBehaviour : MonoBehaviour
{
    
    private ArrayList balloons = new ArrayList();
    private float speed = 500; // 这里做匀速直线运动
    public Animator animator;  // Animator 组件
   
    private Boolean isCollide = false;
    private const int InvalidTimeTick = 300;
    private int invalidTimeCount = 0;
    private Renderer jimmyRenderer = null;
    
    private enum floatingStatus
    {
        RIGHT,
        LEFT
    }

    private floatingStatus status = floatingStatus.LEFT;


    // Start is called before the first frame update
    void Start()
    {
        jimmyRenderer = gameObject.GetComponent<Renderer>();
        GameObject balloon = Instantiate(Resources.Load<GameObject>("Balloons/cyan_balloon"), gameObject.transform, true);
        balloon.transform.localPosition = new Vector3(1,3,0);
        balloons.Add(balloon);
        
        GameObject balloon2 = Instantiate(Resources.Load<GameObject>("Balloons/grassgreen_balloon"), gameObject.transform, true);
        balloon2.transform.localPosition = new Vector3(0.8f,1,0);
        balloons.Add(balloon2);
        
        GameObject balloon3 = Instantiate(Resources.Load<GameObject>("Balloons/purple_balloon"), gameObject.transform, true);
        balloon3.transform.localPosition = new Vector3(1.3f,3.1f,0);
        balloons.Add(balloon3);
        
        BalloonBehaviour.setJimmyBehaviour(this);
    }

    // Update is called once per frame
    void Update()
    {
        Float();
        if (isCollide)
        {
            Shake();
        }
    }

    private void Shake()
    {
        if (invalidTimeCount % 30 == 0)
        {
            jimmyRenderer.enabled = false;
        }
        else if(invalidTimeCount%30==15)
        {
            jimmyRenderer.enabled = true;
        }

        if (++invalidTimeCount == InvalidTimeTick)
        {
            jimmyRenderer.enabled = true;
            isCollide = false;
            invalidTimeCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Jimmy: OnTriggerEnter2D");
        if (collision.gameObject.name.Contains("obstacle")&&!isCollide)
        {
            isCollide = true;
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
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("LEFT");
            // 位置移动
            p.x += moveX * speed * Time.deltaTime;
            // Animation Controller 的参数状态更新
            animator.SetBool("floatLeft",true);

            if (status != floatingStatus.LEFT)
            {
                status = floatingStatus.LEFT;
                NotifyBalloons();
            }

            // 按动 D 键 - 向右移动
        }else if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("RIGHT");
            p.x += moveX * speed * Time.deltaTime;
            animator.SetBool("floatLeft",false);

            if (status != floatingStatus.RIGHT)
            {
                status = floatingStatus.RIGHT;
                NotifyBalloons();
            }
        }
        transform.position = p;
    }

    private void NotifyBalloons()
    {
        foreach (GameObject balloon in balloons)
        {
            balloon.transform.RotateAround(transform.position,transform.up, 180f);

        }
    }

    public Boolean LoseBalloon(GameObject balloon)
    {
        if (isCollide)
        {
            return false;
        }

        isCollide = true;
        balloons.Remove(balloon);
   
        CheckTerminate();
        return true;
    }

    public void CheckTerminate()
    {
        Debug.Log("terminate:"+balloons.Count);
        if (balloons.Count == 0)
        {
            Application.Quit();
            Debug.Log("Terminated");
        }

    }



}
