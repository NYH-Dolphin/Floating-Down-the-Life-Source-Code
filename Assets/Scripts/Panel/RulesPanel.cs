using UnityEngine;
using UnityEngine.UI;

public class RulesPanel : BasePanel
{

    private Button home;
    private Button next;
    private Button last;
    private Text ruleText;

    private int ruleCount = 0;
    private readonly GameObject[] pics = new GameObject[3];
    private readonly string[] rules =
    {
        "A D/ ← →: Manipulate Jimmy to move last and right",
        "Crash: Jimmy will lose one balloon if collide with an obstacle",
        "Enter/Click: Talk with some one to hear from his/her story"
    };

    //初始化
    public override void OnInit()
    {
        skinPath = "RulesPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        home = skin.transform.Find("home").GetComponent<Button>();
        next = skin.transform.Find("next").GetComponent<Button>();
        last = skin.transform.Find("last").GetComponent<Button>();
        home.onClick.AddListener(OnHomeClick);
        next.onClick.AddListener(OnNextClick);
        last.onClick.AddListener(OnLastClick);

        pics[0] = skin.transform.Find("rule1").gameObject;
        pics[1] = skin.transform.Find("rule2").gameObject;
        pics[2] = skin.transform.Find("rule3").gameObject;
        
        pics[1].SetActive(false);
        pics[2].SetActive(false);

        ruleText = skin.transform.Find("ruleText/rule").GetComponent<Text>();
        ruleText.text = rules[ruleCount];
    }

    //关闭
    public override void OnClose()
    {
		
    }

    private void OnHomeClick()
    {
        PanelManager.Open<BeginPanel>();
        Close();
    }
    
    private void OnNextClick()
    {
        if (ruleCount < 2)
        {
            pics[ruleCount].SetActive(false);
            ruleCount++;
            ruleText.text = rules[ruleCount];
            pics[ruleCount].SetActive(true);
        }
    }
    
    private void OnLastClick()
    {
        if (ruleCount > 0)
        {
            pics[ruleCount].SetActive(false);
            ruleCount--;
            ruleText.text = rules[ruleCount];
            pics[ruleCount].SetActive(true);
        }
    }
    
}
