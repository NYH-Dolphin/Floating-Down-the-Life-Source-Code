using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    
    private GameObject Jimmy_small; // 游戏中会动的小 Jimmy
    private Animator animator;
    private float speed = 200f;
    
    private GameObject black;
    private float a = 0f;
    
    private Button skip;
    private bool skipClicked;

    private Flowchart flowchart;
    private bool notDestroy = true;

    //初始化
    public override void OnInit()
    {
        skinPath = "StartPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        skip = skin.transform.Find("skip").GetComponent<Button>();
        skip.onClick.AddListener(OnSkipClick);
        Jimmy_small = skin.transform.Find("Jimmy_small").gameObject;
        animator = Jimmy_small.transform.GetChild(0).GetComponent<Animator>();
        black = skin.transform.Find("black").gameObject;
        black.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
        flowchart = skin.transform.Find("Flowchart").GetComponent<Flowchart>();
        flowchart.SetStringVariable("language", PlayerPrefs.GetString("language", "EN"));

        // 高度记录
        HeightRecord.RefreshHeight();
        HeightRecord.Continue();
        WallBehavior.Continue();
        HeightRecord.Begin();
        WallBehavior.Begin();
        // Debug.Log("Total height is: " + HeightRecord.GetTotalHeight());
        
        
        // 如果不是第一次进行游戏，则立即跳过
        if(PlayerPrefs.GetInt("Initial", 0) == 1)
        {
            StartCoroutine(Disappear());
        }
    }

    private void OnSkipClick()
    {
        skipClicked = true;
    }

    //关闭
    public override void OnClose()
    {
    }

    private void Update()
    {
        bool move = flowchart.GetBooleanVariable("move");
        // Debug.Log("move is " + move);
        if (move)
        {
            if (Jimmy_small.transform.position.x < 70)
                Jimmy_small.transform.position += new Vector3(1, 0, 0) * (speed * Time.smoothDeltaTime);
            else if (Jimmy_small.transform.position.x < 300)
            {
                Jimmy_small.transform.GetComponent<Rigidbody2D>().gravityScale = 30;
                Jimmy_small.transform.position += new Vector3(1, 1, 0) * (speed * Time.smoothDeltaTime);
            }

            animator.SetFloat("posX", Jimmy_small.transform.position.x);
        }
        
        if (Jimmy_small.transform.position.y < -500 || skipClicked)
        {
            a += Time.smoothDeltaTime;
            black.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
        }

        if (a > 0.9 && notDestroy)
        {
            // Destroy(flowchart.gameObject);
            notDestroy = true;
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        if (PlayerPrefs.GetInt("Initial", 0) == 0)
        {
            PlayerPrefs.SetInt("Initial", 1);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            black.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }

        PanelManager.Open<GamePanel>();
        Close();
    }
}