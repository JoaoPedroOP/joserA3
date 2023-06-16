using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    public AudioSource runningWater;
    public AudioMixerGroup audioMixer;
    private AudioMixer mixer;

    void Start()
    {
        this.mixer = Resources.Load("MainMixer") as AudioMixer;
        var groupMixer = "Master";
        var outputMixer = mixer.FindMatchingGroups(groupMixer)[0];
        this.btnClick.Stop();
        this.btnClick.outputAudioMixerGroup = outputMixer;
        this.selectionClick.Stop();
        this.selectionClick.outputAudioMixerGroup = outputMixer;
        this.spawnSound.Stop();
        this.spawnSound.outputAudioMixerGroup = outputMixer;
        this.bees.Stop();
        this.bees.outputAudioMixerGroup = outputMixer;
        this.rain.Stop();
        this.rain.outputAudioMixerGroup = outputMixer;
        this.correctAnswer.Stop();
        this.correctAnswer.outputAudioMixerGroup = outputMixer;
        this.wrongAnswer.Stop();
        this.wrongAnswer.outputAudioMixerGroup = outputMixer;
        this.runningWater.Stop();
        this.runningWater.outputAudioMixerGroup = outputMixer;
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

    public void PlayRunningWater()
    {
        this.runningWater.Play();
    }
}
