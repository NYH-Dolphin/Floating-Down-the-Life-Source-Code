using UnityEngine;
using UnityEngine.UI;

public class StopPanel : BasePanel
{

    private Button continueButton;
    private Button homeButton;
    private Button retryButton;
    private Button collectionButton;
    
    //初始化
    public override void OnInit()
    {
        skinPath = "StopPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        continueButton = skin.transform.Find("background/continue").GetComponent<Button>();
        homeButton = skin.transform.Find("background/home").GetComponent<Button>();
        retryButton = skin.transform.Find("background/retry").GetComponent<Button>();
        collectionButton = skin.transform.Find("background/collection").GetComponent<Button>();
        
        continueButton.onClick.AddListener(OnContinueClick);
        homeButton.onClick.AddListener(OnHomeClick);
        retryButton.onClick.AddListener(OnRetryClick);
        collectionButton.onClick.AddListener(OnCollectionClick);
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
        GamePanel gamePanel = (GamePanel) PanelManager.panels["GamePanel"];
        gamePanel.OnStopClick();
        gamePanel.Close();
        PanelManager.Open<BeginPanel>();
        Close();
    }
    
    private void OnRetryClick()
    {
        GamePanel gamePanel = (GamePanel) PanelManager.panels["GamePanel"];
        gamePanel.OnStopClick();
        gamePanel.Close();
        PanelManager.Open<StartPanel>();
        Close();
    }
    
    private void OnCollectionClick()
    {
        PanelManager.Open<CollectionPanel>();
    }
    
}
