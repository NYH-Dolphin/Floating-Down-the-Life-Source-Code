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
        
        // 初始化声音
        if (PlayerPrefs.GetInt("VolumeInitial", 0) == 0)
        {
            GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = 1;
            PlayerPrefs.SetInt("VolumeInitial", 1);
            PlayerPrefs.SetFloat("Volume", 1);
        }

        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume =  PlayerPrefs.GetFloat("Volume");
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