using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class ControllerSetas : MonoBehaviour
{
    
    private static ControllerSetas Instancia;

    private GameObject lixoSelect;
    public GameObject painelObjetos;
    public GameObject painelLixeiras;
    public GameObject proxNivel;
    public Boolean substituirAposParear;
    private AudioSource audioSource;
    public AudioClip somAcerto;
    public AudioClip somErro;

    void Awake()
    {
        Instancia = this;
    }
    void Start()
    {
        for(int x = 0; x<painelObjetos.transform.childCount; x++)
        {
            GameObject filho = painelObjetos.transform.GetChild(x).gameObject;
            Button btn = filho.GetComponent<Button>();

            btn.onClick.AddListener(() => ClicarLixo(filho));
        }

        for(int x = 0; x<painelLixeiras.transform.childCount; x++)
        {
            GameObject filho = painelLixeiras.transform.GetChild(x).gameObject;
            Button btn = filho.GetComponent<Button>();

            btn.onClick.AddListener(() => ClicarLixeira(filho));
        }

        audioSource = GetComponent<AudioSource>();
    }

    void ClicarLixo(GameObject lixo)
    {
        Image img = lixo.GetComponent<Image>();
        lixoSelect = lixo;
        Debug.Log("Clicou em " + lixoSelect.name + " imagem: "+img.sprite.name);
    }

    void ClicarLixeira(GameObject lixeira)
    {
        Image img = lixeira.GetComponent<Image>();
        lixeira.name = "lixeira";
        Debug.Log("Clicou em " + lixeira.name+ " imagem: "+img.sprite.name);
        Pareamento(lixeira);
        Debug.Log(painelObjetos.transform.childCount);
        if(painelObjetos.transform.childCount <= 1)
        {
            AtivarBotao();
        }
    }

    void Pareamento(GameObject lixeira)
    {
        Image imgLixo = lixoSelect.GetComponent<Image>();
        Image imgLixeira = lixeira.GetComponent<Image>();

        string nomeLixo = imgLixo.sprite.name;
        string nomeLixeira = imgLixeira.sprite.name;

        nomeLixo = nomeLixo.Split('_')[1];
        nomeLixeira = nomeLixeira.Split('_')[1];

        if(nomeLixo == nomeLixeira)
        {
            Debug.Log("jogou fora: "+ lixoSelect.name);
            if(substituirAposParear)
            {
                Sprite[] arraySprites = Resources.LoadAll<Sprite>("fases/reciclagemFase1/objetoPareado"); //so alterar esse caminho para alterar a imagem pareada;
                Debug.Log("entrou no if de objetoPareado");
                Debug.Log(arraySprites.Length);
                foreach(Sprite novo in arraySprites)
                    {
                        string novoSprite = novo.name.Split('_')[1];
                        if(nomeLixeira == novoSprite)
                        {
                            imgLixeira.sprite = novo;
                            break;
                        }
                    }           
            }
            audioSource.PlayOneShot(somAcerto);
            Destroy(lixoSelect);
            lixoSelect = null;
        }
        else
        {
            audioSource.PlayOneShot(somErro);
            Debug.Log("Seleção errada!");
        }
    }

    void AtivarBotao()
    {
        Debug.Log("entrou na função ativar botão");
        proxNivel.SetActive(true);
    }
}
