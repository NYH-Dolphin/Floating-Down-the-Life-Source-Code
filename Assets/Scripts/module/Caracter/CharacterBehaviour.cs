using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public bool firstMeet = true;
    public Fungus.Flowchart flowchart;
    private int meetTime = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    // 计算角色与 Jimmy 之间的距离
    public float CalculateDistance(Vector2 p1, Vector2 p2)
    {
        float i = Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x)
                             + (p1.y - p2.y) * (p1.y - p2.y));

        return i;
    }

    // 在范围内 - 可以触发与 Jimmy 之间的会话
    public bool InBounds(float distance)
    {
        if (distance <= 200)
        {
            Debug.Log("Trigger Conversation");
            flowchart.SetIntegerVariable("inBound", 1);
            return true;
        }
        flowchart.SetIntegerVariable("inBound", 0);
        return false;
    }


    // 与角色开启谈话
    public void StartConversation()
    {
        flowchart.SetBooleanVariable("conversation", true);
    }

    // 正在与角色处于对话状态中
    public bool InConversation()
    {
        return flowchart.GetBooleanVariable("conversation");
    }
}