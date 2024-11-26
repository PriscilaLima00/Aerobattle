using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudoiManager : MonoBehaviour
{
    public static AudoiManager instance;

    public AudioSource musicSource, SfxSource;
    public AudioClip ClipColetavel;

    public bool isJogadorMenu;
    public bool isJogadorMorreu;
    public bool isGamePause;

    public float musicTimePosition;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0 && !isGamePause)
        {
            isGamePause = true;
            musicTimePosition = musicSource.time;
            PararMusica();
        }
        else if (Time.timeScale > 0 && isGamePause)
        {
            isGamePause = false;
            if (!isJogadorMenu && !isJogadorMorreu) 
            {
                TocarMusica();
                musicSource.time = musicTimePosition;
            }
        }    
    }

    void TocarEfeitoSonoro(string nomeDoClip)
    {
        switch (nomeDoClip)
        {
            case "coletavel":
                SfxSource.PlayOneShot(ClipColetavel);
                break;
            default:
                Debug.LogError($"efeito sonoro{nomeDoClip} não encontrado");
                break;
        }
    }

    private void OnEnable()
    {
        AudioObserver.PlayMusicEvent += TocarMusica;
        AudioObserver.StopMusicEvent += PararMusica;
        AudioObserver.PlaySfxEvent += TocarEfeitoSonoro;
    }

    private void OnDisable()
    {
        AudioObserver.PlayMusicEvent -= TocarMusica;
        AudioObserver.StopMusicEvent -= PararMusica;
        AudioObserver.PlaySfxEvent -= TocarEfeitoSonoro;
    }

    void TocarMusica()
    {
        if (!isJogadorMenu && !isJogadorMorreu && !isGamePause)
        {
            musicSource.Play();
        }
        
    }

    void PararMusica()
    {
        musicSource.Stop();
    }

    public void JogadorMorreu()
    {
        isJogadorMorreu = false;
        if (!isJogadorMenu && !isGamePause)
        {
            TocarMusica();
        }
    }

    public void EntrarNoMenu()
    {
        isJogadorMenu = true;
        PararMusica();
    }

    public void SairDoMenu()
    {
        if (!isGamePause && !isJogadorMorreu)
        {
            TocarMusica();
        }
    }
}
