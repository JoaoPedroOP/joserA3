using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public static SFXPlaying Instance = null;

    public AudioSource btnClick;

    public AudioSource selectionClick;

    public AudioSource spawnSound;

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
}
