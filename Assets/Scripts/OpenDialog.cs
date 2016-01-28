using UnityEngine;
using System.Collections;
using System.Windows.Forms;
using System.IO;

public class OpenDialog : MonoBehaviour {

	// Use this for initialization
    private LoadingLevelParameter load;
    string songPath;
    string txtPath;

    void Start()
    {
        load = GameObject.FindGameObjectWithTag("LoadLevelParameterTag").GetComponent<LoadingLevelParameter>();
    }
	public void ShowDialog() {

        OpenFileDialog file = new OpenFileDialog();

        file.Filter = "Text files (.txt)|*.txt";
        file.FilterIndex = 1;
        file.Title = "Song Selection";
        file.ShowDialog();

        txtPath = file.FileName;
        string temp = txtPath.Substring(0, txtPath.LastIndexOf('.'));

        if (File.Exists(temp + ".mp3"))
        {
            songPath = temp + ".mp3";
            load.setCustomPath(txtPath);
            load.setCustomSongPath(songPath);
            load.setLoadLevelParameter(0);
        }
	
	}
	
}
