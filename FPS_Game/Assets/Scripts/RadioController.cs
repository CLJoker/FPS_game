using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour {

    [SerializeField] private AudioClip radio_go;
    [SerializeField] private AudioClip radio_fallback;
    [SerializeField] private AudioClip radio_cheer;
    [SerializeField] private AudioClip radio_clear;
    private AudioSource audio;


	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1) && audio.isPlaying == false)
        {
            audio.clip = radio_go;
            audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && audio.isPlaying == false)
        {
            audio.clip = radio_fallback;
            audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && audio.isPlaying == false)
        {
            audio.clip = radio_cheer;
            audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && audio.isPlaying == false)
        {
            audio.clip = radio_clear;
            audio.Play();
        }
    }
}
