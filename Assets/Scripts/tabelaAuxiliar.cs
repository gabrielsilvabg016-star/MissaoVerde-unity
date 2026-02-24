using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class tabelaAuxiliar : MonoBehaviour
{
    public GameObject cnvs;
    public Button botao;
    public Transform painel;
    private GameObject objLixo1;
    private GameObject objLixo2;
    private GameObject objLixo3;
    void Start() 
    {
        objLixo1 = painel.GetChild(0).gameObject;
        objLixo2 = painel.GetChild(1).gameObject;
        objLixo3 = painel.GetChild(2).gameObject;

        if (cnvs != null)
        {
            cnvs.SetActive(false); //seta a imagem como escondida
        }
        botao.onClick.AddListener(MostrarImagem);
    }

    public void MostrarImagem()
    {
        if(cnvs.activeSelf == false) //imagem escondida
        {
            objLixo1.SetActive(false);
            objLixo2.SetActive(false);
            objLixo3.SetActive(false);
            cnvs.SetActive(true); //mostra a imagem
            Debug.Log("teste imagem escondida");
            
        }
        else if (cnvs.activeSelf == true) //imagem mostrada
        {
            objLixo1.SetActive(true);
            objLixo2.SetActive(true);
            objLixo3.SetActive(true);
            cnvs.SetActive(false); //esconde a imagem
            Debug.Log("teste imagem mostrada");
        }
    }
}