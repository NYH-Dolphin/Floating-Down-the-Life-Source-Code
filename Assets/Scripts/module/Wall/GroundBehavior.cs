using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{

    public Flowchart flowchart;
    private bool back;
    public GamePanel panel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // 获得目前的解锁数量，触发不同的结局
        flowchart.SetIntegerVariable("collection", Collection.GetUnlockNum());
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
            // PlayerPrefs.DeleteAll();
        }
    }
}
