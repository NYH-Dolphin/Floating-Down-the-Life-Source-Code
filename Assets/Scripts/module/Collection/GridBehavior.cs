using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private int ID = -1;
    private Boolean Locked = true;

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

    public void Init(int id, Boolean isLocked)
    {
        if (id >= Collection.GetCollection())
        {
            gameObject.transform.Find("lock").gameObject.SetActive(false);
            return;
        }

        this.ID = id;
        this.Locked = isLocked;
        String CollectionName = "coll" + this.ID;
        LockIcon = gameObject.transform.Find("lock").gameObject;
        CollectionIcon = gameObject.transform.Find(CollectionName).gameObject;
        ChangeIconVisibility();
    }

    public void SetIconVisibility(Boolean locked)
    {
        this.Locked = locked;
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
            detail.transform.GetComponent<CollectionBehaviour>().SetCollection(this.ID);
        }
    }
}
