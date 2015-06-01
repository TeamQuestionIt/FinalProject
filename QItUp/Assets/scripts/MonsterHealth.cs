using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{

    public Image healthBar;
    public Image border;

    private ImpAI impAIScript;
    private EyeAttack eyeAttackScript;





    // Use this for initialization
    void Start()
    {
        impAIScript = GetComponent<ImpAI>();
        eyeAttackScript = GetComponent<EyeAttack>();
    }

    // Update is called once per frame
    void Update()
    {




    }

    private void OnGUI()
    {
        if (healthBar != null)
        {

            // transform.position = Camera.main.WorldToScreenPoint(monsterPos.transform.position);
            if (null != impAIScript)
            {
                healthBar.fillAmount = (float)impAIScript.currentHitPoints / impAIScript.hitPoints;
            }
            else if(null != eyeAttackScript)
            {
                healthBar.fillAmount = (float)eyeAttackScript.currentHitPoints / eyeAttackScript.hitPoints;
            }



        }
    }

    public void DisableBars()
    {
        Destroy(healthBar);
        Destroy(border);
    }
}
