using UnityEngine;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using NAudio;
using NAudio.Wave;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    private Kolajnice kolajnice;
    private GameObject character;

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    private AudioClip customSong;

    public int pickedSong = 3;

    public AudioSource audio;

    private LoadingLevelParameter load;

    // Use this for initialization
    void Start() {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
        character = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("LoadLevelParameterTag") != null)
            load = GameObject.FindGameObjectWithTag("LoadLevelParameterTag").GetComponent<LoadingLevelParameter>();
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
            case 0:
                //Debug.Log(load.getCustomSongPath());
                StartCoroutine(loadMp3(load.getCustomSongPath()));              
                break;
        }
    }

    IEnumerator loadMp3(string path)
    {
        //samples = new float[sampleCount];

        char[] chars = new char[3] { path[path.Length - 3], path[path.Length - 2], path[path.Length - 1] };

        string ext = new string(chars);

        if (path[path.Length - 3] == "mp3"[0])
        {
            Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\MusicalDefense");
            Mp3ToWav(path, System.IO.Path.GetTempPath() + @"\MusicalDefense\currentsong.wav");
            ext = "wav";
        }
        else
        {
            Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\MusicalDefense");
            File.WriteAllBytes(System.IO.Path.GetTempPath() + @"\MusicalDefense\currentsong." + ext, File.ReadAllBytes(path));
        }

        WWW www = new WWW("file://" + System.IO.Path.GetTempPath() + @"\MusicalDefense\currentsong." + ext);
        AudioClip a = www.audioClip;

        while (!a.isReadyToPlay)
        {
            Debug.Log("still in loop");
            yield return www;
        }

        customSong = a;
        audio.clip = customSong;
        yield return new WaitForSeconds(kolajnice.countInTime);
        //kolajnice. += kolajnice.countInTime;
        audio.Play();
        //isReady = true;
    }

    public static void Mp3ToWav(string mp3File, string outputFile)
    {
        using (Mp3FileReader reader = new Mp3FileReader(mp3File))
        {
            WaveFileWriter.CreateWaveFile(outputFile, reader);
        }
    }
	public float getPlayTime() {
		
        //if (audio.isPlaying) {
			return audio.time; 
        //} else {
        //    return 0;
        //}
	}

	public AudioSource GetAudioSource()
	{
		return audio;
	}

	// Update is called once per frame
	void Update () {
        if (!audio.isPlaying && kolajnice.elapsedTime >= kolajnice.countInTime)
        {
            audio.Play();
        }
        if (character.transform.localPosition.y < 0)
        {
            audio.pitch = Mathf.Lerp(1, 0, character.transform.localPosition.y / -8);
            if (character.transform.localPosition.y < -8)
            {
                kolajnice.GameOver = true;
            }
        }
        else if (kolajnice.GameOver)
        {
            if (audio.pitch > 0)
                audio.pitch -= Time.deltaTime / 2;
        }
        else
        {
            audio.pitch = 1;
        }
        if (audio.pitch <= 0)
        {
            audio.Stop();
        }
	}
}
