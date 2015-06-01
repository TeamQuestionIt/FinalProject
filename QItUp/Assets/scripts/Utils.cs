using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

    public IEnumerator Flash(float flashTime)
    {
        float timer = 0;
        float step = .1f;
        float currentStep = 0;
        int direction = 1;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        while (timer < flashTime)
        {
            renderer.color = Color.Lerp(Color.red, Color.green, currentStep);

            if (currentStep > 1)
            {
                direction = -1;
            }
            else if (currentStep < 0)
            {
                direction = 1;
            }
            currentStep = currentStep + (step * direction);
            timer += Time.deltaTime;
            // yield return new WaitForSeconds(.1f);
            yield return null;
        }
        renderer.color = Color.white;
        yield return null;
    }
}
