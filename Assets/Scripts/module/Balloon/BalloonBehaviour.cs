using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private static JimmyBehaviour Jimmy;

    public static void setJimmyBehaviour(JimmyBehaviour j)
    {
        Jimmy = j;
    }
}