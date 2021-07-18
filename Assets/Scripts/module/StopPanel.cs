using UnityEngine;
using UnityEngine.UI;

public class StopPanel : BasePanel
{

    private Button continueButton;
    private Button homeButton;
    private Button retryButton;
    
    //初始化
    public override void OnInit()
    {
        skinPath = "StopPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        continueButton = skin.transform.Find("continue").GetComponent<Button>();
        homeButton = skin.transform.Find("home").GetComponent<Button>();
        retryButton = skin.transform.Find("retry").GetComponent<Button>();
        
        continueButton.onClick.AddListener(OnContinueClick);
        homeButton.onClick.AddListener(OnHomeClick);
        retryButton.onClick.AddListener(OnRetryClick);
    }

    //关闭
    public override void OnClose()
    {
		
    }

    private void OnContinueClick()
    {
        GamePanel gamePanel = (GamePanel) PanelManager.panels["GamePanel"];
        gamePanel.OnStopClick();
        Close();
    }
    
    private void OnHomeClick()
    {
        PanelManager.panels["GamePanel"].Close();
        PanelManager.Open<BeginPanel>();
        Close();
    }
    
    private void OnRetryClick()
    {
        // PanelManager.panels["GamePanel"].Close();
        // PanelManager.Open<GamePanel>();
        // Close();
    }
    
}
