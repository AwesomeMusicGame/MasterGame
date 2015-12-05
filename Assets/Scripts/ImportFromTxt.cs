using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BeatTheMusic
{

    public class ImportFromTxt : MonoBehaviour
    {

        private Kolajnice kolajniceScript;
        private string path1 = @".\Assets\Songs\ThinkDude.txt";
        private string path2 = @".\Assets\Songs\Tea Blaster.txt";
        private string path3 = @".\Assets\Songs\Poor-o.txt";
        public string customPath;
        private string filename;
        private LoadingLevelParameter load;

        public int pickedSong;

        // Use this for initialization
        void Start()
        {
            kolajniceScript = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
            if (GameObject.FindGameObjectWithTag("LoadLevelParameterTag") != null)
                load = GameObject.FindGameObjectWithTag("LoadLevelParameterTag").GetComponent<LoadingLevelParameter>();
            pickedSong = kolajniceScript.MasterPickedSong;
            switch (pickedSong)
            {
                case 1:
                    filename = path1;
                    break;
                case 2:
                    filename = path2;
                    break;
                case 3:
                    filename = path3;
                    break;
                case 0:
                    filename = load.getCustomPath();
                    break;
            }

            ImportFromFile();
            kolajniceScript.SpawnMap();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void ImportFromFile()
        {
            List<float> tempList = new List<float>();
            try
            {
                //need to be fixed 
                StreamReader reader = new StreamReader(filename);
                string input = reader.ReadToEnd();
                if (input != string.Empty)
                {
                    string[] parts = input.Split('\n');
                    foreach (string part in parts)
                    {
                        float tempFloat;
                        float.TryParse(part, out tempFloat);
                        tempList.Add(tempFloat);
                        //                    Debug.Log(tempFloat + " parsed from: " + part);
                    }
                    //                kolajniceScript.DebugWriteAllBeats();

                    kolajniceScript.Beats = tempList;
                    reader.Close();
                }
                else
                {
                    Debug.Log("No lvl data loaded from the file...string is empty");
                }
            }
            catch (ArgumentNullException e)
            {
                Debug.Log("Null filename.\n" + e.ToString());
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Null Reference Exception.\n" + e.ToString());
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("No lvl data loaded from the file...file is missing\n" + e.ToString());
            }
        }
    }
}
