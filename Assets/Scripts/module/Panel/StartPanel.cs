using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private GameObject Jimmy;
    private Animator animator;
    private float speed = 200f;
    private GameObject black;
    private float a = 0f;
    private Button skip;
    private Boolean skipClicked = false;

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
        Jimmy = skin.transform.Find("Jimmy").gameObject;
        animator = Jimmy.GetComponent<Animator>();
        black = skin.transform.Find("black").gameObject;
        black.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
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
        if (Jimmy.transform.position.x < 70)
            Jimmy.transform.position += new Vector3(1, 0, 0) * (speed * Time.smoothDeltaTime);
        else if (Jimmy.transform.position.x < 300)
        {
            Jimmy.GetComponent<Rigidbody2D>().gravityScale = 30;
            Jimmy.transform.position += new Vector3(1, 1, 0) * (speed * Time.smoothDeltaTime);
        }
        animator.SetFloat("posX", Jimmy.transform.position.x);
        if (Jimmy.transform.position.y < -500 || skipClicked)
        {
            a += Time.smoothDeltaTime;
            black.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
        }
        if (a > 0.9)
            StartCoroutine(Disappear());
    }
    
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        PanelManager.Open<GamePanel>();
        Close();
    }
}