using UnityEngine;
using UnityEngine.UI;

public class OverPanel : BasePanel
{
    
    private Button homeButton;
    private Button retryButton;

    //初始化
    public override void OnInit()
    {
        skinPath = "OverPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        homeButton = skin.transform.Find("background/home").GetComponent<Button>();
        retryButton = skin.transform.Find("background/retry").GetComponent<Button>();
        
        homeButton.onClick.AddListener(OnHomeClick);
        retryButton.onClick.AddListener(OnRetryClick);
    }

    //关闭
    public override void OnClose()
    {
		
    }

    private void OnHomeClick()
    {
        WallBehavior.Continue();
        HeightRecord.Continue();
        CharacterBehaviour.real_stop = false;
        PanelManager.panels["GamePanel"].Close();
        PanelManager.Open<BeginPanel>();
        Close();
    }
    
    private void OnRetryClick()
    {
        WallBehavior.Continue();
        HeightRecord.Continue();
        CharacterBehaviour.real_stop = false;
        PanelManager.panels["GamePanel"].Close();
        PanelManager.Open<StartPanel>();
        Close();
    }
    
}