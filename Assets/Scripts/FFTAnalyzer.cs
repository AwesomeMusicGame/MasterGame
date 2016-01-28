using UnityEngine;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using NAudio;
using NAudio.Wave;

public class FFTAnalyzer : MonoBehaviour
{

    public FFTWindow FFTMode;
    public int sampleCount;
    public Transform[] cubies;
    float[] samples;
    public AudioSource source;
    bool isReady = false;

    IEnumerator Start()
    {
        samples = new float[sampleCount];
        OpenFileDialog file = new OpenFileDialog();

        file.Filter = "Ogg Vorbis files (.ogg)|*.ogg|Wave files (.wav)|*.wav|Mp3 files (.mp3)|*.mp3";
        file.FilterIndex = 3;
        file.Title = "Song Selection";
        file.ShowDialog();

        char[] chars = new char[3] { file.FileName[file.FileName.Length - 3], file.FileName[file.FileName.Length - 2], file.FileName[file.FileName.Length - 1] };

        string ext = new string(chars);

        if (file.FileName[file.FileName.Length - 3] == "mp3"[0])
        {
            Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\MusicalDefense");
            Mp3ToWav(file.FileName, System.IO.Path.GetTempPath() + @"\MusicalDefense\currentsong.wav");
            ext = "wav";
        }
        else
        {
            Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\MusicalDefense");
            File.WriteAllBytes(System.IO.Path.GetTempPath() + @"\MusicalDefense\currentsong." + ext, File.ReadAllBytes(file.FileName));
        }

        WWW www = new WWW("file://" + System.IO.Path.GetTempPath() + @"\MusicalDefense\currentsong." + ext);
        AudioClip a = www.audioClip;

        while (!a.isReadyToPlay)
        {
            Debug.Log("still in loop");
            yield return www;
        }


        source.clip = a;
        source.Play();
        isReady = true;
    }

    public static void Mp3ToWav(string mp3File, string outputFile)
    {
        using (Mp3FileReader reader = new Mp3FileReader(mp3File))
        {
            WaveFileWriter.CreateWaveFile(outputFile, reader);
        }
    }
}
