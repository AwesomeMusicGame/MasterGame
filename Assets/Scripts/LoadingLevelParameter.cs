using UnityEngine;
using System.Collections;

public class LoadingLevelParameter : MonoBehaviour {

	private int loadLevelParamerer = 0;
	public int tempp;
    private string customTxtPath;
    private string customSongPath;

	void Awake() {
		tempp = loadLevelParamerer;
		DontDestroyOnLoad(transform.gameObject);
	}

	public int getLoadLevelParameter()
	{
		return loadLevelParamerer;
	}

    public void setCustomPath(string temp)
    {
        customTxtPath = temp;
    }

    public string getCustomPath()
    {
        return customTxtPath;
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
