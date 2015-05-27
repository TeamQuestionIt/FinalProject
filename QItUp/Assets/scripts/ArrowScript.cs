using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public enum CurrentMenu
    {
        menu_Main,
        menu_Control,
        menu_Pause
    }


    public CurrentMenu startingMenu = CurrentMenu.menu_Main;
    CurrentMenu currentMenu;
    public int startingPosition = 0;
    int currentPosition;

    public float inputInterval = 0.5f;
    float waitingTime = 0;

    //menu length trackers hardcoded for safty and time
    const int mainMenuItemCount = 4;
    const int controlMenuItemCount = 1;
    const int pauseMenuItemCount = 0;//changeLater

    bool changePos = true;

	// Use this for initialization
	void Start () {
        currentMenu = startingMenu;
        currentPosition = startingPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (waitingTime > 0.0f)
        {
            //decrement input wait time
            waitingTime -= Time.deltaTime;

            //if they let go of the button then we can take annother input
            if (Input.GetAxis("Vertical") > -0.00001 && Input.GetAxis("Vertical") < 0.00001)//float eq for 0
            {
                waitingTime = 0;
            }
        }
        else
        {
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                currentPosition--;

                if (currentPosition < 0)
                {
                    //if we're out of bounds bring us back in
                    currentPosition = 0;
                }
                else
                {
                    //if the move was valid then tell the input to wait and the arrow to change its position
                    waitingTime = inputInterval;
                    changePos = true;
                }
            }
            else if (Input.GetAxis("Vertical") < 0.0f) 
            {
                currentPosition++;

                switch (currentMenu)
                {
                    case CurrentMenu.menu_Main:
                        //if we exceed the menu's item count put us back in
                        if (currentPosition >= mainMenuItemCount)
                        {
                            currentPosition = mainMenuItemCount - 1;
                        }
                        else
                        {
                            waitingTime = inputInterval;
                            changePos = true;
                        }
                        break;
                    case CurrentMenu.menu_Control:
                        if (currentPosition >= controlMenuItemCount)
                        {
                            currentPosition = controlMenuItemCount - 1;
                        }
                        else
                        {
                            waitingTime = inputInterval;
                            changePos = true;
                        }
                        break;
                    case CurrentMenu.menu_Pause:
                        if (currentPosition >= pauseMenuItemCount)
                        {
                            currentPosition = pauseMenuItemCount - 1;
                        }
                        else
                        {
                            waitingTime = inputInterval;
                            changePos = true;
                        }
                        break;
                    default:
                        Debug.Log("currentMenue has an Invalid Value");
                        break;
                }
            }
        }

        if (changePos)
        {
            //the positions are preset in GameObjects beforehand, they will always be named <MenuName>Pos<number> where the numbers start at 0 
            string positionName = "";
            switch (currentMenu)
            {
                case CurrentMenu.menu_Main:
                    positionName = "MainPos" + currentPosition;
                    break;
                case CurrentMenu.menu_Control:
                    positionName = "ControlPos" + currentPosition;
                    break;
                case CurrentMenu.menu_Pause:
                    positionName = "PausePos" + currentPosition;
                    break;
                default:
                    Debug.Log("currentMenue has an Invalid Value");
                    break;
            }

            GameObject positionObject = GameObject.Find(positionName);
            //we don't take the entire transform because the arrow might have scaling changes
            transform.position = positionObject.transform.position;
        }
	}
}
