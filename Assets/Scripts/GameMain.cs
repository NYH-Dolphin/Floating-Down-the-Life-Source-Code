using UnityEngine;
using Utils;

public class GameMain : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//初始化
		PanelManager.Init();
		//打开登陆面板
		PanelManager.Open<BeginPanel>();
		Collection.Init();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
