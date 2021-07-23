using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public AudioClip[] audios;
    public static int index = 0;

    void Start()
    {
        GetComponent<AudioSource>().clip = audios[index];
        GetComponent<AudioSource>().Play();
        // GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
        // slider.value = GetComponent<AudioSource>().volume;
    }


    // Update is called once per frame
    void Update()
    {
        transfer();
    }

    public void transfer()
    {
        if (GetComponent<AudioSource>().isPlaying)
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


        if (Input.GetKeyDown(KeyCode.Alpha3))
            GetComponent<AudioSource>().Pause();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
    }
}