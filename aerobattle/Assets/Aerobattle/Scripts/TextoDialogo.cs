using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TextoDialogo
{
    [SerializeField]
    private string _frase;

    [SerializeField] private string _btContinuar;

    public string GetFrase()
    {
        return _frase;
    }

    public string GetBotaoContinuar()
    {
        return _btContinuar;
    }
}
