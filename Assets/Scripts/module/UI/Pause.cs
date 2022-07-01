using System;
using UnityEngine;
using UnityEngine.UI;

namespace module.UI
{
    public class Pause : MonoBehaviour
    {
        private void Update()
        {
            gameObject.GetComponent<Button>().enabled = !JimmyBehaviour.inConversation;
        }
    }
}