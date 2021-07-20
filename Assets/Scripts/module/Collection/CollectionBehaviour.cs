using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Text collectionName;
    public Text collectionDescription;
    public Button closeButton;
    public Image[] collections;
    
    private int ID = 0;
    private Collection obj = null;
    void Start()
    {
        closeButton.onClick.AddListener(CloseCollection);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void CloseCollection()
    {
        Debug.Log("onclick");
        Destroy(gameObject);
    }

    public void SetCollection(int id)
    {
        collections[ID].gameObject.SetActive(false);
        ID = id;
        collections[ID].gameObject.SetActive(true);
        obj = CollectionPanel.AllCollections[id];
        collectionName.text = obj.GetName();
        collectionDescription.text = obj.GetDescription();
    }

}
