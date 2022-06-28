using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private int ID = -1;
    private bool Locked = true;

    private GameObject LockIcon;
    private GameObject CollectionIcon;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowCollectionDetails);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int id)
    {
        string key = "collection_lock_" + id;
        if (id >= Collection.GetCollectionCount())
        {
            gameObject.transform.Find("lock").gameObject.SetActive(false);
            return;
        }
        ID = id;
        Locked = PlayerPrefs.GetInt(key) == 0;//TODO 这里改成了 false 可以直接查看所有物品
        string collectionName = "coll" + ID;
        LockIcon = gameObject.transform.Find("lock").gameObject;
        CollectionIcon = gameObject.transform.Find(collectionName).gameObject;
        ChangeIconVisibility();
    }

    private void ChangeIconVisibility()
    {
        LockIcon.SetActive(Locked);
        CollectionIcon.SetActive(!Locked);
    }

    private void ShowCollectionDetails()
    {
        if (!Locked)
        {
            GameObject detail = Instantiate(Resources.Load<GameObject>("collection/CollectionDetail"),
                GameObject.Find("Root/Canvas/CollectionPanel(Clone)").transform, true);
            detail.transform.localPosition = new Vector3(0, 100, 0);
            detail.transform.GetComponent<CollectionBehaviour>().SetCollection(ID);
        }
    }
}
