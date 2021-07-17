using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyBehaviour : MonoBehaviour
{

    private GameObject balloons;
    private float speed = 500; // 这里做匀速直线运动
    public Animator animator;  // Animator 组件


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }

    void Float()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 p = transform.position;
        // 按动 A 键 - 向左移动
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            // Debug.Log("LEFT");
            // 位置移动
            p.x += moveX * speed * Time.deltaTime;
            // Animation Controller 的参数状态更新
            animator.SetBool("floatLeft",true);
            // 按动 D 键 - 向右移动
        }else if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            // Debug.Log("RIGHT");
            p.x += moveX * speed * Time.deltaTime;
            animator.SetBool("floatLeft",false);
        }
        transform.position = p;
    }



}
