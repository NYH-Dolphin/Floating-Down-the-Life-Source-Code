using UnityEngine;

public class GameMain : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//初始化
		PanelManager.Init();
		//打开登陆面板
		Debug.Log("begin");
		PanelManager.Open<BeginPanel>();
		
	}


	// Update is called once per frame
	void Update () {
		
	}
}
