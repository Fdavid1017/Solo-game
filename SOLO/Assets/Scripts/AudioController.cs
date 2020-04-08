using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource cardAudio;
    public AudioSource clickAudio;
    public AudioSource endgameAudio;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (Resources.FindObjectsOfTypeAll<AudioController>().Length > 1)
        {
            Debug.Log(Resources.FindObjectsOfTypeAll<AudioController>().Length);
            Destroy(gameObject);
        }
    }

    public void PlayCardAudio() { cardAudio.Play(); }
    public void PlayClick() { clickAudio.Play(); }
    public void PlayEndgameAudio() { endgameAudio.Play(); }
}
