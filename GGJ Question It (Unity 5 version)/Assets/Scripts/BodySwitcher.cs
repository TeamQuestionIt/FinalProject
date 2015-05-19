using UnityEngine;
using System.Collections;

public class BodySwitcher : MonoBehaviour {

    //all assigned in the inspector
    public RuntimeAnimatorController tvController, clockController, camController, lampcontroller;
    public GameObject tvAttack, clockAttack, camAttack;

    public enum bodyType
    {
        tv, clock, camera, lamp,
    }

    public bodyType currentBody = bodyType.tv;
    private Animator anim;
    private PlayerAttack attack;


    public void Start()
    {
        anim = GetComponent<Animator>();
        attack = GetComponent<PlayerAttack>();
    }

    public void Update()
    {
        /*
         * Should be changed to however we actually switch between heads
         * */

        if(Input.GetKeyDown(KeyCode.Y))
            setBody(bodyType.tv);
        else if (Input.GetKeyDown(KeyCode.U))
            setBody(bodyType.camera);
        else if(Input.GetKeyDown(KeyCode.I))
            setBody(bodyType.clock);
        else if (Input.GetKeyDown(KeyCode.O))
            setBody(bodyType.lamp);
    }


    public void setBody(bodyType head)
    {
        switch(head){
            case bodyType.tv:
                anim.runtimeAnimatorController = tvController;
			attack.specialAttackPrefab = tvAttack;
                currentBody = bodyType.tv;
                break;
            case bodyType.clock:
                anim.runtimeAnimatorController = clockController;
			attack.specialAttackPrefab = clockAttack;
                currentBody = bodyType.clock;
                break;
            case bodyType.camera:
                anim.runtimeAnimatorController = camController;
			attack.specialAttackPrefab = camAttack;
                currentBody = bodyType.camera;
                break;
            case bodyType.lamp:
                anim.runtimeAnimatorController = lampcontroller;
			attack.specialAttackPrefab = null;
                currentBody = bodyType.lamp;
                break;
            default:
                Debug.Log("Something went wrong");
                break;
        }
        
    }
}
