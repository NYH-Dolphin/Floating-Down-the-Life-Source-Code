using UnityEngine;

/// <summary>
/// 本角色会多次谈话在剧情结束后会赠送气球给 Jimmy
/// </summary>
public class MultAndBalloonCharacter : CharacterBehaviour
{
    
    
    private bool hasGifted = false; // 是否已经赠送过气球
    // private static int meetTime = 1;
    
    
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
            WallBehavior.Pause();
            HeightRecord.Pause();
            JimmyBehaviour.Pause();
            hasConversation = true;
        }
        else
        {
            WallBehavior.Continue();
            HeightRecord.Continue();
            JimmyBehaviour.Continue();
        }
        GiveBalloonCheck();
    }
    
    // 检测是否给予 Jimmy 一个气球
    private void GiveBalloonCheck()
    {
        if (!hasGifted && flowchart.GetBooleanVariable("gifted"))
        {
            hasGifted = true;
            Jimmy.GetComponent<JimmyBehaviour>().AddBalloon(1);
        }
    }

}