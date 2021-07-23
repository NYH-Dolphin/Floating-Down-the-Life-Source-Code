using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCollectPanel : BasePanel
{
    private Text message;
    
    //初始化
    public override void OnInit()
    {
        skinPath = "GetCollectPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        message = skin.transform.Find("message").GetComponent<Text>();
        string msg = "解锁" + (string) args[0];
        message.text = msg;
        StartCoroutine(Disappear());
    }

    //关闭
    public override void OnClose()
    {
		
    }
    
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2f);
        Close();
    }
}
