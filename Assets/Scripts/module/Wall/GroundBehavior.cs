using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{

    public Flowchart flowchart;
    private bool back;
    
    private const int characterNum = 15;


    // Start is called before the first frame update
    void Start()
    {
        // 获得目前的解锁数量，触发不同的结局
        flowchart.SetIntegerVariable("collection", Collection.GetUnlockNum());
        
        // 重置所有角色的 meetTime
        for (int i = 1; i <= characterNum; i++)
        {
            string meetTime = "meetTime" + i;
            flowchart.SetIntegerVariable(meetTime, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        back = flowchart.GetBooleanVariable("back");
        if (back)
        {
            GamePanel gamePanel = (GamePanel) PanelManager.panels["GamePanel"];
            gamePanel.Close();
            flowchart.SetBooleanVariable("back", false);
            PanelManager.Open<BeginPanel>();
            
            GameObject.Find("Audio Source").GetComponent<MusicController>().PlayStartMusic();
            // 删除所有collection
            // PlayerPrefs.DeleteAll();
        }
    }
}
