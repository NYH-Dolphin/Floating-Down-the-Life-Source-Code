using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumnController : MonoBehaviour
{
    // Start is called before the first frame update
    // public AudioSource audioSource;
    public Slider slider;
    // public AudioClip[] audios;

    void Start()
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().clip =
            GameObject.Find("Audio Source").GetComponent<MusicController>().audios[MusicController.index];
        // GetComponent<AudioSource>().clip = audios[index];
        //
        //
        // GetComponent<AudioSource>().Play();

        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
        slider.value = GameObject.Find("Audio Source").GetComponent<AudioSource>().volume;
        slider.onValueChanged.AddListener(Controlsound);
    }

    private void Controlsound(float arg0)
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }


    // Update is called once per frame
    void Update()
    {
    }
}