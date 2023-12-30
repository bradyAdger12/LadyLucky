using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    public AudioSource walkingAudioSource;
    public AudioSource jumpingAudioSource;
    public AudioSource playerDeadAudioSource;
    public AudioSource playerHitAudioSource;
    public AudioSource playerKillingEnemySound;
    private PlayerMovement playerMovement;
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(inputManager.moveVector.x) > 0 && playerMovement.isGrounded) {
            if (!walkingAudioSource.isPlaying) {
                walkingAudioSource.Play();
            }
        } else {
            walkingAudioSource.Stop();
        }
    }

    public void PlayKillingEnemySound () {
        playerKillingEnemySound.Play();
    }

    public void PlayJumpSound () {
        jumpingAudioSource.Play();
    }

    public void PlayPlayerDeadAudio () {
        playerDeadAudioSource.Play();
    }

    public void PlayPlayerHitAudio () {
        playerHitAudioSource.Play();
    }
}
