using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    private Kolajnice kolajnice;

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;

    public int pickedSong = 1;

    private AudioSource audio;

    // Use this for initialization
    void Start() {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
        audio = GetComponent<AudioSource>();
        pickedSong = kolajnice.MasterPickedSong;
        switch (pickedSong)
        {
            case 1:
                audio.clip = song1;
                break;
            case 2:
                audio.clip = song2;
                break;
            case 3:
                audio.clip = song3;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!audio.isPlaying && Time.time >= kolajnice.countInTime)
        {
            audio.Play();
        }
	}
}
