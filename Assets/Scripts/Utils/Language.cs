using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class Language : MonoBehaviour
    {
        public string CN; // 中文
        public string EN; // 英文

        private static Dictionary<GameObject, string> CNDic = new Dictionary<GameObject, string>();
        private static Dictionary<GameObject, string> ENDic = new Dictionary<GameObject, string>();

        /// <summary>
        /// 注册语言
        /// </summary>
        private void Start()
        {
            var o = gameObject;
            CNDic[o] = CN;
            ENDic[o] = EN;
            if (PlayerPrefs.GetString("language", "EN") == "CN")
            {
                if (o.transform.TryGetComponent(out Text text))
                {
                    text.text = CN;
                }
            }
            else
            {
                if (o.transform.TryGetComponent(out Text text))
                {
                    text.text = EN;
                }
            }
        }

        private void OnDestroy()
        {
            var o = gameObject;
            CNDic.Remove(o);
            ENDic.Remove(o);
        }
        

        /// <summary>
        /// 统一设置成中文
        /// </summary>
        public static void SetChineseLanguage()
        {
            Debug.Log("Language CN");
            foreach (var obj in CNDic.Keys)
            {
                if (obj.transform.TryGetComponent(out Text text))
                {
                    text.text = CNDic[obj];
                }
            }
            PlayerPrefs.SetString("language", "CN");
        }
        
        /// <summary>
        /// 统一设置成英文
        /// </summary>
        public static void SetEnglishLanguage()
        {
            foreach (var obj in CNDic.Keys)
            {
                if (obj.transform.TryGetComponent(out Text text))
                {
                    text.text = ENDic[obj];
                }
            }
            PlayerPrefs.SetString("language", "EN");
        }
    }
}