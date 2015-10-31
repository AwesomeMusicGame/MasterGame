using UnityEngine;
using System.Collections;

public class LoadingLevelParameter : MonoBehaviour {

	private int loadLevelParamerer = 0;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public int getLoadLevelParameter()
	{
		return loadLevelParamerer;
	}

	public void setLoadLevelParameter(int temp)
	{
		loadLevelParamerer = temp;
		Application.LoadLevel(1);
	}
}
