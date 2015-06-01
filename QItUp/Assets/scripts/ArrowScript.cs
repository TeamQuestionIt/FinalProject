using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public enum CurrentMenu
    {
        menu_Main,
        menu_Control,
        menu_Score,
        menu_Pause
    }

    public GameObject MainMenuParent;
    public GameObject ControlMenuParent;
    public GameObject ScoreMenuParent;
    public GameObject PauseMenuParent;

    public CurrentMenu startingMenu = CurrentMenu.menu_Main;
    CurrentMenu currentMenu;
    public int startingPosition = 0;
    int currentPosition;

    public float inputInterval = 0.5f;
    float waitingTime = 0;
    float firetime = 0;

    //menu length trackers hardcoded for safty and time
    const int mainMenuItemCount = 4;
    const int controlMenuItemCount = 1;
    const int scoreMenuItemCount = 1;
    const int pauseMenuItemCount = 2;//changeLater

    bool changePos = true;

	// Use this for initialization
	void Start () {
        SwitchMenu(startingMenu);
        currentPosition = startingPosition;

        
	}
	
	// Update is called once per frame
	void Update () {
        if (waitingTime > 0.0f)
        {
            //decrement input wait time
            waitingTime -= Time.deltaTime;

            //if they let go of the button then we can take annother input
            if (Input.GetAxisRaw("Vertical") > -0.00001 && Input.GetAxisRaw("Vertical") < 0.00001)//float eq for 0 and if select button unpressed
            {
                waitingTime = 0;
            }
        }
        else
        {

            if (Input.GetAxisRaw("Vertical") > 0.0f)
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
            else if (Input.GetAxisRaw("Vertical") < 0.0f) 
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
                    case CurrentMenu.menu_Score:
                        if (currentPosition >= scoreMenuItemCount)
                        {
                            currentPosition = scoreMenuItemCount - 1;
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

        if (firetime > 0.0f)
        {
            firetime -= Time.deltaTime;
            if (Input.GetButtonUp("Fire1")) {
                firetime = 0;
            }

        } 
        else if (Input.GetButton("Fire1"))
        {
            DoAction();
            firetime = inputInterval;

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
                case CurrentMenu.menu_Score:
                    positionName = "ScorePos" + currentPosition;
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

    void DoAction()
    {
        switch (currentMenu)
        {
            case CurrentMenu.menu_Main:
                switch (currentPosition)
                {
                    case 0:
                        //make player and score manager if needed
                        //reset score manager if needed
                        //Application.loadedLevel(/*first level*/"");
                        break;
                    case 1:
                        SwitchMenu(CurrentMenu.menu_Control);
                        break;
                    case 2:
                        SwitchMenu(CurrentMenu.menu_Score);
                        break;
                    case 3:
                        Debug.Log("Quitting Application");
                        Application.Quit();
                        break;
                    default:
                        Debug.Log("Main Menu has invalid position");
                        break;
                }
                break;
            case CurrentMenu.menu_Control:
                switch (currentPosition)
                {
                    case 0:
                        SwitchMenu(CurrentMenu.menu_Main);
                        currentPosition = 1;
                        break;
                    default:
                        Debug.Log("Control menu has invalid position");
                        break;
                }
                break;

            case CurrentMenu.menu_Pause:
                switch (currentPosition)
                {
                    case 0:
                        Time.timeScale = 1;
                        Object.Destroy(PauseMenuParent);
                        break;
                    case 1:
                        //scene change main menu
                        break;
                    default:
                        Debug.Log("pause menu has invalid position");
                        break;
                }
                break;
            case CurrentMenu.menu_Score:
                switch (currentPosition)
                {
                    case 0:
                        SwitchMenu(CurrentMenu.menu_Main);
                        currentPosition = 2;
                        break;
                    default:
                        Debug.Log("Score menu has invalid position");
                        break;
                }
                break;
            default:
                Debug.Log("currentMenue has an Invalid Value");
                break;
        }
    }

    public void SwitchMenu(CurrentMenu newMenu)
    {
        currentMenu = newMenu;

        if (MainMenuParent != null)
        {
            MainMenuParent.SetActive(false);
        }
        if (ControlMenuParent != null)
        {
            ControlMenuParent.SetActive(false);
        }
        if (PauseMenuParent != null)
        {
            PauseMenuParent.SetActive(false);
        }
        if (ScoreMenuParent != null)
        {
            ScoreMenuParent.SetActive(false);
        }

        switch (newMenu)
        {
            case CurrentMenu.menu_Main:
                MainMenuParent.SetActive(true);
                break;

            case CurrentMenu.menu_Control:
                ControlMenuParent.SetActive(true);
                break;

            case CurrentMenu.menu_Pause:
                PauseMenuParent.SetActive(true);
                break;

            case CurrentMenu.menu_Score:
                ScoreMenuParent.SetActive(true);
                break;

            default:
                Debug.Log("Invalid starting state!");
                break;
        }

        currentPosition = 0;
        changePos = true;
    }
}
