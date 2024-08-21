using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudoiManager : MonoBehaviour
{
    public static AudoiManager instance;

    public AudioSource musicSource, SfxSource;
    public AudioClip ClipColetavel, ClipMovimentação;
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

    void TocarEfeitoSonoro(string nomeDoClip)
    {
        switch (nomeDoClip)
        {
            case "coletavel":
                SfxSource.PlayOneShot(ClipColetavel);
                break;
            default:
                Debug.LogError($"efeito sonoro não encontrado");
                break;
        }
    }
}
