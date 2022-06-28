using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button start;
    public Button rules;
    public Button collection;

    public Button settings;

    //初始化
    public override void OnInit()
    {
        skinPath = "BeginPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        //寻找组件
        start = skin.transform.Find("start").GetComponent<Button>();
        rules = skin.transform.Find("rules").GetComponent<Button>();
        collection = skin.transform.Find("collection").GetComponent<Button>();
        settings = skin.transform.Find("settings").GetComponent<Button>();

        start.onClick.AddListener(OnBeginClick);
        rules.onClick.AddListener(OnRulesClick);
        collection.onClick.AddListener(OnCollectionClick);
        settings.onClick.AddListener(OnSettingClick);
    }

    private void OnSettingClick()
    {
        PanelManager.Open<SettingPanel>();
        Close();
    }

    //关闭
    public override void OnClose()
    {
    }

    //当按下开始按钮
    private void OnBeginClick()
    {
        PanelManager.Open<StartPanel>();
        Close();
    }

    private void OnRulesClick()
    {
        PanelManager.Open<RulesPanel>();
        Close();
    }

    private void OnCollectionClick()
    {
        PanelManager.Open<CollectionPanel>();
    }
}