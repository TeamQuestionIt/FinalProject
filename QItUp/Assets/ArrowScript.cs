using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public enum currentMenu
    {
        menu_Main,
        menu_Control,
        menu_Pause
    }


    public currentMenu startingMenu = currentMenu.menu_Main;
    public int startingPosition = 0;
    GameObject positionContainer;

    //menu length trackers hardcoded for safty and time
    const int mainMenuItemCount = 4;
    const int controlMenuItemCount = 1;
    const int pauseMenuItemCount = 0;//changeLater

	// Use this for initialization
	void Start () {
        positionContainer = GameObject.Find("ArrowPositions");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
