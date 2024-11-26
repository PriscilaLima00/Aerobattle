using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoSonoro : MonoBehaviour
{
    public static EfeitoSonoro instance;
    public AudioSource somDaExplosão, somDoLaser, somDeColeta, somDoImpacto;
 
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
