using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public Fungus.Flowchart flowchart;
    protected GameObject Jimmy;

    public static bool real_stop = false;

    // Start is called before the first frame update
    protected void Start()
    {
        Jimmy = transform.parent.parent.Find("Jimmy(Clone)").gameObject;
    }

    // Update is called once per frame
    protected void Update()
    {
        float distance = CalculateDistance();
        InBounds(distance);
        if (InConversation())
        {
            flowchart.SetIntegerVariable("inBound", 0); // 如果正在对话中的话 不能再按 Enter 进入对话
            WallBehavior.Stop();
            HeightRecord.Pause();
            JimmyBehaviour.Stop();
        }
        else if (!real_stop)
        {
            WallBehavior.Move();
            HeightRecord.Continue();
            JimmyBehaviour.Move();
        }
    }


    // 计算角色与 Jimmy 之间的距离
    protected float CalculateDistance()
    {
        Vector2 p1 = Jimmy.transform.position;
        Vector2 p2 = transform.position;
        return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
    }

    // 在范围内 - 可以触发与 Jimmy 之间的会话
    protected void InBounds(float distance)
    {
        if (distance <= 200)
        {
            Debug.Log("Trigger Conversation");
            flowchart.SetIntegerVariable("inBound", 1);
        }
        else
            flowchart.SetIntegerVariable("inBound", 0);
    }

    // 正在与角色处于对话状态中
    protected bool InConversation()
    {
        return flowchart.GetBooleanVariable("conversation");
    }
}