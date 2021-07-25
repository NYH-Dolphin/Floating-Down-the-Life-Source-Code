using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{
    public Flowchart flowchart;
    private bool back;
    private int collection;
    public AudioClip[] audios;
    private int index = 0;

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
        collection = flowchart.GetIntegerVariable("collection");

        if (back)
        {
            GamePanel gamePanel = (GamePanel) PanelManager.panels["GamePanel"];
            gamePanel.Close();
            flowchart.SetBooleanVariable("back", false);
            PanelManager.Open<BeginPanel>();
            GameObject.Find("Audio Source").GetComponent<MusicController>().PlayStartMusic();
            // PlayerPrefs.DeleteAll();
        }
    }
}