using UnityEngine;
using System.Collections;
using System.IO;

public class OpenFileBrowser : MonoBehaviour {
	//skins and textures
	public GUISkin skin;
	public Texture2D file,folder,back,drive;
    public bool show= false;
	
	//initialize file browser
	FileBrowser fb = new FileBrowser(new Rect(new Vector2(100,150),new Vector2(1100,450)));
	string output = "no file";
	// Use this for initialization
	void Start () {
		//setup file browser style
		fb.guiSkin = skin; //set the starting skin
		//set the various textures
        fb.searchPattern = "*.txt";
		fb.fileTexture = file; 
		fb.directoryTexture = folder;
		fb.backTexture = back;
		fb.driveTexture = drive;
		//show the search bar
		fb.showSearch = true;
		//search recursively (setting recursive search may cause a long delay)
		fb.searchRecursively = true;
	}
	
	void OnGUI(){
        if (show)
        {
            // Set one of the layouts - 0/1
            fb.setLayout(0);

            GUILayout.Label("Selected File: " + output);

            //draw and display output
            if (fb.draw())
            {
                
                //true is returned when a file has been selected
                //the output file is a member if the FileInfo class, if cancel was selected the value is null
                output = fb.outputFile.ToString();
                if(File.Exists(Path.GetFileNameWithoutExtension(fb.outputFile.ToString())+".mp3"))
                {
                    string txt = fb.outputFile.ToString();
                    string mp3 = Path.GetFileNameWithoutExtension(fb.outputFile.ToString()) + ".mp3";
                }
            }
        }
	}

    public void ShowMenu()
    {
        show = true;
    }
    public void HideMenu()
    {
        show = false;
    }
}
