﻿using UnityEngine;
using System.Collections.Generic;

public static class  PanelManager{
	//Layer
	public enum Layer{
		Panel,
		Tip,
	}
	//层级列表
	private static Dictionary<Layer, Transform> layers = new Dictionary<Layer, Transform>();
	//面板列表
	public static Dictionary<string, BasePanel> panels = new Dictionary<string, BasePanel>();
	//结构
	public static Transform root;
	public static Transform canvas;
	//初始化
	public static void Init(){
		root = GameObject.Find("Root").transform;
		canvas = root.Find("Canvas");
		Transform panel = canvas.Find("Panel");
		Transform tip = canvas.Find("Tip");
		layers.Add(Layer.Panel, canvas);
		layers.Add(Layer.Tip, canvas);
	}

	//打开面板
	public static void Open<T>(params object[] para) where T:BasePanel{
		//已经打开
		string name = typeof(T).ToString();
		if (panels.ContainsKey(name)){
			return;
		}
		//组件
		BasePanel panel = root.gameObject.AddComponent<T>();
		panel.OnInit();
		panel.Init();
		//父容器
		Transform layer = layers[panel.layer];
		panel.skin.transform.SetParent(layer, false);
		//列表
		panels.Add(name, panel);
		//OnShow
		panel.OnShow(para);
	}

	//关闭面板
	public static void Close(string name){
		BasePanel panel = panels[name];
		//没有打开
		if (panel == null){
			return;
		}
		//OnClose
		panel.OnClose();
		//列表
		panels.Remove(name);
		//销毁
		GameObject.Destroy(panel.skin);
		Component.Destroy(panel);
	}
}
