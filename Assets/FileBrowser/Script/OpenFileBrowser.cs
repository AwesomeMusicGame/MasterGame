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

        //initialize file browser
        FileBrowser fb = new FileBrowser(new Rect(new Vector2(100, 150), new Vector2(1100, 450)));
        string output = "no file";
        // Use this for initialization
        void Start()
        {
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
                    Debug.Log(fb.outputFile.ToString());
                    if (File.Exists(Path.GetFileNameWithoutExtension(fb.outputFile.ToString()) + ".mp3"))
                    {
                        output = fb.outputFile.ToString();
                        string txt = fb.outputFile.ToString();
                        importTxt.customPath = txt;
                        string mp3 = Path.GetFileNameWithoutExtension(fb.outputFile.ToString()) + ".mp3";
                        //musicPlayer.customSong = 
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
