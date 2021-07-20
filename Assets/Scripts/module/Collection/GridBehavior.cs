using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private int ID;
    private Boolean Locked = true;

    private GameObject LockIcon;
    private GameObject CollectionIcon;

    private static int MAXCOLLCNT = 4;

   
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowCollectionDetails);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int id,Boolean isLocked)
    {
        if (id >= MAXCOLLCNT)
        {
            gameObject.transform.Find("lock").gameObject.SetActive(false);
            return;
        }

        this.ID = id;
        this.Locked = isLocked;
        String CollectionName = "coll" + (this.ID+1);
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
        if (this.Locked)
        {
            
        }

        Debug.Log(this.ID);
    }
}
