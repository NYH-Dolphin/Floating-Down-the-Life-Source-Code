using UnityEngine;
/// <summary>
/// 本角色会多次谈话在剧情结束后会赠送收藏品给 Jimmy
/// </summary>
public class MultAndGiftCharacter : CharacterBehaviour
{
    private bool hasGifted = false; // 是否已经赠送过礼物
    private static int meetTime = 1;
    private bool hasConversation = false; // 是否与该角色的这个实例有过对话
    
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        float distance = CalculateDistance();
        InBounds(distance);
        if (InConversation())
        {
            flowchart.SetIntegerVariable("inBound", 0); // 如果正在对话中的话 不能再按 Enter 进入对话
            WallBehavior.Stop();
            HeightRecord.Pause();
            JimmyBehaviour.Stop();
            hasConversation = true;
        }
        else
        {
            WallBehavior.Move();
            HeightRecord.Continue();
            JimmyBehaviour.Move();
        }
        GiveGiftCheck();
    }
    
    // 检测是否给予 Jimmy 一个礼物
    private void GiveGiftCheck()
    {
        // 礼物 id
        // giftID 初始设置成 -1
        int id = flowchart.GetIntegerVariable("giftID");
        if (!hasGifted && id != -1)
        {
            hasGifted = true;
            string collectionName = "collection_lock_" + id;
            PlayerPrefs.SetInt(collectionName, 1);
        }
    }
    
    // 在范围内 - 可以触发与 Jimmy 之间的会话
    private void InBounds(float distance)
    {
        if (distance <= 200)
        {
            // flowchart 预先 +1
            flowchart.SetIntegerVariable("meetTime", meetTime);

            Debug.Log("Trigger Conversation");
            flowchart.SetIntegerVariable("inBound", 1);
        }
        else
        {
            flowchart.SetIntegerVariable("inBound", 0);
            
            // 有过谈话，但是离开了
            if (hasConversation)
            {
                int flowChartMeetTime = flowchart.GetIntegerVariable("meetTime");
                meetTime = meetTime == flowChartMeetTime ? meetTime + 1 : meetTime;
                hasConversation = false;
            }
            // 没有谈话过且远离了
            else
            {
                int flowchartMeetTime = flowchart.GetIntegerVariable("meetTime");
                // 之前进入了 200 内，flowChartMeetTime 预先 +1 了
                // 减去 1
                if (flowchartMeetTime == meetTime)
                {
                    flowchartMeetTime--;
                    flowchart.SetIntegerVariable("meetTime", flowchartMeetTime);
                }
            }
        }

    }
}