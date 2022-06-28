using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public Fungus.Flowchart flowchart;
    public GameObject enterHint; // enter的进入提示
    protected GameObject Jimmy;

    private bool scale = false;
    public static bool real_stop = false;

    // Start is called before the first frame update
    protected void Start()
    {
        Jimmy = transform.parent.parent.Find("Jimmy(Clone)").gameObject;
        flowchart.SetStringVariable("language", PlayerPrefs.GetString("language", "EN"));
        enterHint = transform.Find("EnterImg").gameObject;
    }

    // Update is called once per frame
    protected void Update()
    {
        float distance = CalculateDistance();
        InBounds(distance);
        if (InConversation())
        {
            flowchart.SetIntegerVariable("inBound", 0); // 如果正在对话中的话 不能再按 Enter 进入对话
            WallBehavior.Pause();
            HeightRecord.Pause();
            JimmyBehaviour.Pause();
        }
        else if (!real_stop)
        {
            WallBehavior.Continue();
            HeightRecord.Continue();
            JimmyBehaviour.Continue();
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
            flowchart.SetIntegerVariable("inBound", 1);
            if (!scale)
            {
                StartCoroutine(OnTriggerEnterImg());
            }
        }
        else
        {
            flowchart.SetIntegerVariable("inBound", 0);
            if (scale)
            {
                StartCoroutine(OnCloseEnterImg());
            }
        }
    }

    // 正在与角色处于对话状态中
    protected bool InConversation()
    {
        return flowchart.GetBooleanVariable("conversation");
    }

    IEnumerator OnTriggerEnterImg()
    {
        iTween.ScaleTo(enterHint, iTween.Hash("time", 0.1f, "easetype", iTween.EaseType.easeInBounce, "scale", new Vector3(1,1,1)));
        scale = true;
        yield return null;
    }
    
    IEnumerator OnCloseEnterImg()
    {
        iTween.ScaleTo(enterHint, iTween.Hash("time", 0.1f, "easetype", iTween.EaseType.easeInBounce, "scale", new Vector3(0,0,0)));
        scale = false;
        yield return null;
    }


}