using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] audios;
    private static int index = 0;

    void Start()
    {
        GetComponent<AudioSource>().clip = audios[index];
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
        GetComponent<AudioSource>().Play();
    }


    // Update is called once per frame
    void Update()
    {
        transfer();
    }

    private void transfer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index++;
            index %= audios.Length;

            GetComponent<AudioSource>().clip = audios[index];
            GetComponent<AudioSource>().Play();
            Debug.Log(GetComponent<AudioSource>().clip.name);
            Debug.Log(index);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (index == 0)
                index = audios.Length - 1;
            else
            {
                index--;
            }

            GetComponent<AudioSource>().clip = audios[index];
            GetComponent<AudioSource>().Play();
            Debug.Log(GetComponent<AudioSource>().clip.name);
        }
    }
}