using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource successSound;
    [SerializeField] private AudioSource failureSound;
    [SerializeField] private AudioSource swipeSound;
    [SerializeField] private AudioSource tapSound;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        UIScript.OnTapDetected += UIScript_OnTapDetected;
        GameManager.OnPuzzleAssembled += GameManager_OnPuzzleAssembled;
        SwipeDetection.OnSwipeDetected += SwipeDetection_OnSwipeDetected;
        CollisionHandler.OnCollisionOccurred += CollisionHandler_OnCollisionOccurred;
    }

    private void UIScript_OnTapDetected(object sender, EventArgs e)
    {
        tapSound.Play();
    }

    private void GameManager_OnPuzzleAssembled(object sender, EventArgs e)
    {
        successSound.Play();
    }

    private void SwipeDetection_OnSwipeDetected(object sender, SwipeEventArgs e)
    {
        swipeSound.Play();
    }

    private void CollisionHandler_OnCollisionOccurred(object sender, EventArgs e)
    {
        failureSound.Play();
    }
}