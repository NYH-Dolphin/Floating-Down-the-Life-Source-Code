﻿using UnityEngine.UI;

public class BeginPanel : BasePanel
{
	public Button start;
	public Button rules;
	public Button collection;

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
		start.onClick.AddListener(OnBeginClick);
		rules.onClick.AddListener(OnRulesClick);
		collection.onClick.AddListener(OnCollectionClick);
	}

	//关闭
	public override void OnClose()
	{
		
	}
	
	//当按下开始按钮
	public void OnBeginClick()
	{
		PanelManager.Open<StartPanel>();
		Close();
	}
	
	public void OnRulesClick()
	{
		PanelManager.Open<RulesPanel>();
		Close();
	}

	public void OnCollectionClick()
	{
		PanelManager.Open<CollectionPanel>();
	}
}