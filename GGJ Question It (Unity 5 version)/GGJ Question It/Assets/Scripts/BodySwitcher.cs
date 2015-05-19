using UnityEngine;
using System.Collections;

public class BodySwitcher : MonoBehaviour {

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
                attack.attackPrefab = tvAttack;
                currentBody = bodyType.tv;
                break;
            case bodyType.clock:
                anim.runtimeAnimatorController = clockController;
                attack.attackPrefab = clockAttack;
                currentBody = bodyType.clock;
                break;
            case bodyType.camera:
                anim.runtimeAnimatorController = camController;
                attack.attackPrefab = camAttack;
                currentBody = bodyType.camera;
                break;
            case bodyType.lamp:
                anim.runtimeAnimatorController = lampcontroller;
                attack.attackPrefab = null;
                currentBody = bodyType.lamp;
                break;
            default:
                Debug.Log("Something went wrong");
                break;
        }
        
    }
}
