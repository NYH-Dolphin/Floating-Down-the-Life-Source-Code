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
    private Boolean skipClicked = false;

    private Flowchart flowchart;
    private Boolean notDestroy = true;

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
        animator = Jimmy_small.GetComponent<Animator>();
        black = skin.transform.Find("black").gameObject;
        black.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
        flowchart = skin.transform.Find("Flowchart").GetComponent<Flowchart>();
        
        // 高度记录
        HeightRecord.RefreshHeight();
        HeightRecord.Continue();
        WallBehavior.Continue();
        HeightRecord.Begin();
        WallBehavior.Begin();
        // Debug.Log("Total height is: " + HeightRecord.GetTotalHeight());
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
        Debug.Log("move is " + move);
        if (move)
        {
            if (Jimmy_small.transform.position.x < 70)
                Jimmy_small.transform.position += new Vector3(1, 0, 0) * (speed * Time.smoothDeltaTime);
            else if (Jimmy_small.transform.position.x < 300)
            {
                Jimmy_small.GetComponent<Rigidbody2D>().gravityScale = 30;
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
        yield return new WaitForSeconds(1f);
        PanelManager.Open<GamePanel>();
        Close();
    }
}