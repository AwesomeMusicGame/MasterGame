using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public Menu currentMenu;
	private Transform llptrans;

    public void Start()
    {
		llptrans = GameObject.FindGameObjectWithTag ("LoadLevelParameterTag").transform;
        ShowMenu(currentMenu);
		
	}

    public void ShowMenu(Menu menu)
    {
        if(currentMenu != null)
        {
            currentMenu.IsOpen = false;
        }
        
        currentMenu = menu;
        currentMenu.IsOpen = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public void LoadMainMenu()
	{
		Destroy (llptrans.gameObject);
		Application.LoadLevel (0);
	}
}
