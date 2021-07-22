using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    private Button close;
    private Button delete;
    private Slider sd;


    //初始化
    public override void OnInit()
    {
        skinPath = "SettingPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        close = skin.transform.Find("close").GetComponent<Button>();
        delete = skin.transform.Find("delete").GetComponent<Button>();
        sd = skin.transform.Find("Slider").GetComponent<Slider>();
        close.onClick.AddListener(OnCloseClick);
        delete.onClick.AddListener(OnDeleteClick);
        sd.onValueChanged.AddListener(ControlSound);
        sd.value = PlayerPrefs.GetFloat("Volume");
    }

    private void OnDeleteClick()
    {
        PlayerPrefs.DeleteAll();
    }

    //关闭
    public override void OnClose()
    {
    }


    private void OnCloseClick()
    {
        PanelManager.Open<BeginPanel>();
        Close();
    }
    
    private void ControlSound(float arg0)
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = sd.value;
        PlayerPrefs.SetFloat("Volume", sd.value);
    }
}