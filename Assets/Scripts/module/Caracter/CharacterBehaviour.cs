using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public Fungus.Flowchart flowchart;
    protected GameObject Jimmy;

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
            WallBehavior.Stop();
            HeightRecord.Pause();
        }
        else
        {
            WallBehavior.Move();
            HeightRecord.Continue();
        }

        
    }


    // 计算角色与 Jimmy 之间的距离
    private float CalculateDistance()
    {
        Vector2 p1 = Jimmy.transform.position;
        Vector2 p2 = transform.position;
        return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
    }

    // 在范围内 - 可以触发与 Jimmy 之间的会话
    private void InBounds(float distance)
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
    private bool InConversation()
    {
        return flowchart.GetBooleanVariable("conversation");
    }
}