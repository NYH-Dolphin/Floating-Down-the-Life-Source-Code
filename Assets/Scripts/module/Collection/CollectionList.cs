using Fungus;
using UnityEngine.UIElements;

public class CollectionList
{
    public static int COLLECTIONCNT = 30;
    private static Collection[] AllCollections;
    private static int[] Unlocked;

    public static void Init()
    {
        AllCollections = new Collection[COLLECTIONCNT];
        Unlocked = new int[COLLECTIONCNT];
    }

}