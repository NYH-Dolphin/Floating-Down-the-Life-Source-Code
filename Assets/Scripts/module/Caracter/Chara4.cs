/// <summary>
/// 本角色在剧情结束后会赠送气球给 Jimmy
/// </summary>
public class Chara4 : CharacterBehaviour
{
    
    private bool hasGifted = false; // 是否已经赠送过气球
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
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