using UnityEngine;
using System.Collections;

public class LoadingLevelParameter : MonoBehaviour {

	private int loadLevelParamerer = 0;
	public int tempp;

	void Awake() {
		tempp = loadLevelParamerer;
		DontDestroyOnLoad(transform.gameObject);
	}

	public int getLoadLevelParameter()
	{
		return loadLevelParamerer;
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
