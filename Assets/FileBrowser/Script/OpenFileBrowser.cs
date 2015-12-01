using UnityEngine;
using System.Collections;
using System.IO;

namespace BeatTheMusic
{

    public class OpenFileBrowser : MonoBehaviour
    {

        //skins and textures
        public GUISkin skin;
        public Texture2D file, folder, back, drive;
        public bool show = false;
        public MusicPlayer musicPlayer;
        public ImportFromTxt importTxt;
        private LoadingLevelParameter load;

        //initialize file browser
        FileBrowser fb = new FileBrowser(new Rect(new Vector2(100, 150), new Vector2(1100, 450)));
        string output = "no file";
        string songPath;
        string txtPath;
        // Use this for initialization
        void Start()
        {
            load = GameObject.FindGameObjectWithTag("LoadLevelParameterTag").GetComponent<LoadingLevelParameter>();

            //importTxt = GameObject.FindGameObjectWithTag("ImportFromTxtObject").GetComponent<ImportFromTxt>();
            //musicPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MusicPlayer>();

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

        void OnGUI()
        {
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
                    string temp = fb.outputFile.ToString();
                    txtPath = temp.Substring(0, temp.LastIndexOf('.'));

                    if (File.Exists(txtPath + ".mp3"))
                    {
                        songPath = txtPath + ".ogg";
                        Debug.Log(txtPath + ".mp3");
                        string txt = fb.outputFile.ToString();                      
                        load.setCustomPath(txt);
                        load.setCustomSongPath(songPath);
                        load.setLoadLevelParameter(0);
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
}
