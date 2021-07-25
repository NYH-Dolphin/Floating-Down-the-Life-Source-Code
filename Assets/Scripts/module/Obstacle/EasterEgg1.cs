using System.Collections;
using UnityEngine;

public class EasterEgg1 : MonoBehaviour
{
    private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Jimmy") && !hasCollided)
        {
            hasCollided = true;
            int count = PlayerPrefs.GetInt("easter_egg_1");
            count++;
            PlayerPrefs.SetInt("easter_egg_1", count);
            if (count == 5)
            {
                string collectionName = "collection_lock_9";
                PlayerPrefs.SetInt(collectionName, 1);
                StartCoroutine(GetCollection(9));
            }
        }
    }
    
    IEnumerator GetCollection(int id)
    {
        yield return new WaitForSeconds(0.8f);
        string collectionName = Collection.GetName(id);
        PanelManager.Open<GetCollectPanel>(collectionName);
    }
}
