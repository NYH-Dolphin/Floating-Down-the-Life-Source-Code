using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    private List<GameObject> leftWall = new List<GameObject>();
    private List<GameObject> rightWall = new List<GameObject>();
    
    //初始化
    public override void OnInit()
    {
        skinPath = "GamePanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        int count = 0;
        for (int i = 705; i >= -895; i-=200)
        {
            count++;
            GameObject wallL = Instantiate(Resources.Load<GameObject>("Wall"), GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            wallL.transform.position = new Vector3(400, i, 0);
            leftWall.Add(wallL);
            GameObject wallR = Instantiate(Resources.Load<GameObject>("Wall"), GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            wallR.transform.position = new Vector3(-400, i, 0);
            rightWall.Add(wallR);
        }
        Debug.Log(count);
    }

    private void Update()
    {
        Vector3 lastP = leftWall.Last().transform.position;
        if (lastP.y >= -710)
        {
            GameObject wallL = Instantiate(Resources.Load<GameObject>("Wall"), GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            wallL.transform.position = new Vector3(400, -905, 0);
            leftWall.Add(wallL);
            GameObject wallR = Instantiate(Resources.Load<GameObject>("Wall"), GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            wallR.transform.position = new Vector3(-400, -905, 0);
            rightWall.Add(wallR);
        }

        GameObject leftFirst = leftWall.First();
        GameObject rightFirst = rightWall.First();
        if (leftFirst.transform.position.y > 950)
        {
            leftWall.Remove(leftFirst);
            Destroy(leftFirst);
            rightWall.Remove(rightFirst);
            Destroy(rightFirst);
        }
    }

    //关闭
    public override void OnClose()
    {
		
    }
}