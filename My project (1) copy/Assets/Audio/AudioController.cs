using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController: MonoBehaviour
{
    // The AudioSource component we'll play audio through
    [SerializeField] AudioSource audioSource;

    // References to Audio Clip assets (sound files like .wav or .ogg)
    // [SerializeField] AudioClip firstClip;
    // [SerializeField] AudioClip secondClip;
    [SerializeField] AudioClip bgm;



    public void Start()
    {
        // Subscribe to GameManager events
        CombatState.Instance.OnBattleLost.AddListener(OnGameOverRecieved);
        CombatState.Instance.OnBattleWon.AddListener(OnGameWonRecieved);
        audioSource.clip = bgm;
        audioSource.Play();
        audioSource.loop = true;
    }

    private void OnGameWonRecieved()
    {
        // // Switch the clip the Audio Source will play, then play it
        // audioSource.clip = firstClip;
        // audioSource.Play();
    }

    private void OnGameOverRecieved()
    {
        // // Switch the clip the Audio Source will play, then play it
        // audioSource.clip = secondClip;
        // audioSource.Play();
    }

    private void StopPlaying()
    {
        // Stop the Audio Source if it is playing
        audioSource.Stop();
    }
}
