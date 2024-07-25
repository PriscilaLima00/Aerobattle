using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoControle : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject dialogoObj;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("configurações")] 
    public float typingSpeed;

    public void Speech(Sprite p, string text, string actorName)
    {
       dialogoObj.SetActive(true);
       profile.sprite = p;
       speechText.text = text;
       actorNameText.text = actorName;
    }
}
