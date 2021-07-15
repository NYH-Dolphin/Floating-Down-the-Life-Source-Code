using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
	public Button start;

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
		Debug.Log("start");
		start = skin.transform.Find("start").GetComponent<Button>();
		start.onClick.AddListener(OnBeginClick);
		Debug.Log(start);
	}

	//关闭
	public override void OnClose()
	{
		
	}
	
	//当按下开始按钮
	public void OnBeginClick()
	{
		Debug.Log("click");
		PanelManager.Open<GamePanel>();
		Close();
	}
}