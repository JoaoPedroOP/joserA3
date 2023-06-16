using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public static SFXPlaying Instance = null;

    public AudioSource btnClick;
    public AudioSource selectionClick;
    public AudioSource spawnSound;
    public AudioSource bees;
    public AudioSource rain;
    public AudioSource correctAnswer;
    public AudioSource wrongAnswer;

    void Start()
    {
        this.btnClick.Stop();
        this.selectionClick.Stop();
        this.spawnSound.Stop();
        this.bees.Stop();
        this.rain.Stop();
        this.correctAnswer.Stop();
        this.wrongAnswer.Stop();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBtnClick()
    {
        this.btnClick.Play();
    }

    public void PlaySelectionClick()
    {
        this.selectionClick.Play();
    }

    public void PlaySpawn()
    {
        this.spawnSound.Play();
    }

    public void PlayBees() 
    {
        this.bees.Play();
    }

    public void PlayRain()
    {
        this.rain.Play();
    }

    public void PlayCorrectAnswer()
    {
        this.correctAnswer.Play();
    }

    public void PlayWrongAnswer()
    {
        this.wrongAnswer.Play();
    }
}
