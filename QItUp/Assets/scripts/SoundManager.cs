using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] clips;

    public enum Clip
    {
        Jump,
        Land,
        Ouch,
        LeftFoot,
        RightFoot
    }

    void Start()
    {
        source.volume = 1;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Play(Clip clip)
    {
        source.clip = clips[(int)clip];
        source.Play();
    }
}
