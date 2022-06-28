using UnityEngine;
using UnityEngine.UI;

public class RulesPanel : BasePanel
{
    // private Button home;
    private Button next;
    private Button last;
    private Button close;
    private Text ruleText;

    private int ruleCount = 0;
    private readonly GameObject[] pics = new GameObject[4];

    private readonly string[] RulesCN =
    {
        "A D / ← →: 左右移动",
        "S / ↓: 长按加速下落，松开恢复",
        "当触碰到障碍物时会损失气球",
        "Enter/点击: 与角色交流，聆听Ta的故事"
    };
    
    private readonly string[] RulesEN =
    {
        "A D / ← →: Move around",
        "S / ↓: Long press to accelerate the fall, release to recover",
        "The balloon will be lost when player touches an obstacle",
        "Enter/Click: Talk to the characters and listen to their stories"
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
        // home = skin.transform.Find("home").GetComponent<Button>();
        next = skin.transform.Find("next").GetComponent<Button>();
        last = skin.transform.Find("last").GetComponent<Button>();
        close = skin.transform.Find("close").GetComponent<Button>();


        // home.onClick.AddListener(OnHomeClick);
        next.onClick.AddListener(OnNextClick);
        last.onClick.AddListener(OnLastClick);
        close.onClick.AddListener(OnCloseClick);
        pics[0] = skin.transform.Find("rule1").gameObject;
        pics[1] = skin.transform.Find("rule2").gameObject;
        pics[2] = skin.transform.Find("rule3").gameObject;
        pics[3] = skin.transform.Find("rule4").gameObject;

        pics[1].SetActive(false);
        pics[2].SetActive(false);
        pics[3].SetActive(false);

        ruleText = skin.transform.Find("ruleText").GetComponent<Text>();
        ruleText.text = PlayerPrefs.GetString("language", "EN") == "CN" ? RulesCN[ruleCount] : RulesEN[ruleCount];
    }

    private void OnCloseClick()
    {
        PanelManager.Open<BeginPanel>();
        Close();
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
        if (ruleCount < RulesCN.Length - 1)
        {
            pics[ruleCount].SetActive(false);
            ruleCount++;
            ruleText.text = PlayerPrefs.GetString("language", "EN") == "CN" ? RulesCN[ruleCount] : RulesEN[ruleCount];
            pics[ruleCount].SetActive(true);
        }
    }

    private void OnLastClick()
    {
        if (ruleCount > 0)
        {
            pics[ruleCount].SetActive(false);
            ruleCount--;
            ruleText.text = PlayerPrefs.GetString("language", "EN") == "CN" ? RulesCN[ruleCount] : RulesEN[ruleCount];
            pics[ruleCount].SetActive(true);
        }
    }
}