using UnityEngine;
using System.Collections;

public class LoadingLevelParameter : MonoBehaviour {

	private int loadLevelParamerer = 0;
	public int tempp;
    private string customPath;
    private string customSongPath;
	public bool isHard = false;
	private int typeOfSongBackground = 0;  //0 space 1 2 3 4 other types

	public int sbsetter = 0;

	void Awake() {
		tempp = loadLevelParamerer;
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update()
	{
		switch(sbsetter)
		{
		case 0: 
			typeOfSongBackground = 0;
			break;
		case 1: 
			typeOfSongBackground = 1;
			break;
		case 2: 
			typeOfSongBackground = 2;
			break;
		case 3: 
			typeOfSongBackground = 3;
			break;
		case 4: 
			typeOfSongBackground = 4;
			break;
		}
		Debug.Log (sbsetter);
	}

	public void setSBSetter(int i)
	{
		sbsetter = i;
	}

	public void setDifficulty(bool d)
	{
		isHard = d;
		Debug.Log (d+ " "  + isHard);
	}

	public bool getDifficulty()
	{
		return isHard;
	}

	public int getLoadLevelParameter()
	{
		return loadLevelParamerer;
	}

    public void setCustomPath(string temp)
    {
        customPath = temp;
    }

    public string getCustomPath()
    {
        return customPath;
    }

    public void setCustomSongPath(string temp)
    {
        customSongPath = temp;
    }

    public string getCustomSongPath()
    {
        return customSongPath;
    }

	public void setLoadLevelParameter(int level)
	{
		tempp = level;
		loadLevelParamerer = level;
		Application.LoadLevel(1);
	}

	public void retryLevel()
	{
		Application.LoadLevel (1);
	}
}
