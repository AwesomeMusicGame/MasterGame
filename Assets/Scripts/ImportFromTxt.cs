using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImportFromTxt : MonoBehaviour
{

    private Kolajnice kolajniceScript;
    private string filename = @".\Assets\Stone Magnet.txt";

	// Use this for initialization
	void Start () {
        kolajniceScript = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();

        ImportFromFile();
        kolajniceScript.SpawnMap();
	}
	
	// Update is called once per frame
	void Update () {
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
