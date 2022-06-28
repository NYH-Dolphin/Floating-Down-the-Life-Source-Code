using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SettingPanel : BasePanel
{
    private Button close;
    private Button delete;
    private Slider sd;
    private Dropdown dd;


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
        
        dd = skin.transform.Find("Language").GetComponent<Dropdown>();
        dd.onValueChanged.AddListener(OnLanguageChange);
        dd.value = PlayerPrefs.GetString("language") == "CN" ? 0 : 1;
    }

    private void OnDeleteClick()
    {
        PlayerPrefs.DeleteAll();
    }

    //关闭
    public override void OnClose()
    {
    }
    
    
    /// <summary>
    /// 当改变语言的时候
    /// </summary>
    /// <param name="v"></param>
    private void OnLanguageChange(int v)
    {
        if (v == 0) 
            Language.SetChineseLanguage();
        else
            Language.SetEnglishLanguage();
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