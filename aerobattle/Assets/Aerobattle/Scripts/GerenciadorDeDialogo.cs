using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GerenciadorDeDialogo : MonoBehaviour
{
   [SerializeField] 
   private TextMeshProUGUI _nomeNPC;
   [SerializeField]
   private TextMeshProUGUI _texto;
   [SerializeField]
   private TextMeshProUGUI _btContinua;
   
   [SerializeField]
   private GameObject _caixaDialogo;
}
