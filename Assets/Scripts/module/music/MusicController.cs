using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    // public AudioSource audioSource;
    public Slider slider;
    public AudioClip[] audios;
    public static int index = 0;

    void Start()
    {
        GetComponent<AudioSource>().clip = audios[index];
        GetComponent<AudioSource>().Play();
        // audioSource.Play();
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

    public void Controlsound()
    {
        // audioSource.volume = slider.value;
        GetComponent<AudioSource>().volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}