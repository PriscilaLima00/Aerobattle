using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogo
{
    [SerializeField] 
    private TextoDialogo[] _frase;

    [SerializeField]
    private string _nomeNpc;

    public TextoDialogo[] Getfrases()
    {
        return _frase;
    }

    public string GetNomeNPC()
    {
        return _nomeNpc;
    }
}




