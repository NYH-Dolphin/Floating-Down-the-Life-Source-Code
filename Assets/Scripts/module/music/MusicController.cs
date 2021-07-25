using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public AudioClip[] audios;
    private static int index = 0;

    void Start()
    {
        GetComponent<AudioSource>().clip = audios[index];
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

    public void PlayEndMusic()
    {
        int num;
        if (Collection.GetUnlockNum() < 3)
            num = 2;
        else if (Collection.GetUnlockNum() < 6)
            num = 3;
        else
            num = 4;
        GetComponent<AudioSource>().clip = audios[num];
        GetComponent<AudioSource>().Play();
    }
    
    public void PlayStartMusic()
    {
        GetComponent<AudioSource>().clip = audios[index];
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        transfer();
    }

    private void transfer()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                index++;
                index %= 2;

                GetComponent<AudioSource>().clip = audios[index];
                GetComponent<AudioSource>().Play();
                Debug.Log(GetComponent<AudioSource>().clip.name);
                Debug.Log(index);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (index == 0)
                    index = 1;
                else
                {
                    index--;
                }

                GetComponent<AudioSource>().clip = audios[index];
                GetComponent<AudioSource>().Play();
                Debug.Log(GetComponent<AudioSource>().clip.name);
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
            GetComponent<AudioSource>().Pause();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
    }
}