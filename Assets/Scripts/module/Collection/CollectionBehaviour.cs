using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private Text collectionName = null;
    private Text collectionDescription = null;
    
    private int ID = -1;
    private Collection obj = null;
    void Start()
    {
        Button closeButton = transform.Find("Close").GetComponent<Button>();
        closeButton.onClick.AddListener(CloseCollection);

        collectionDescription = transform.Find("Description").GetComponent<Text>();
        collectionName = transform.Find("Name").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void CloseCollection()
    {
        Debug.Log("onclick");
        Destroy(transform.gameObject);
    }

    public void SetCollection(int id)
    {
        this.ID = id;
        obj = CollectionPanel.AllCollections[id];
        // this.collectionName.text = obj.GetName();
        // this.collectionDescription.text = obj.GetDescription();
    }

}
