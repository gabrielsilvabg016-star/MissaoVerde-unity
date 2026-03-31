using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SistemaSelecao : MonoBehaviour
{
    private List<GameObject>objetos = new List<GameObject>();
    //private List<GameObject>sombras = new List<GameObject>();
    public GameObject painelObjeto;
    public GameObject painelSombra;

    private int indice = 0;

    private GameObject objetoSelecionado = null;
    private GameObject sombraSelecionada = null;

    private bool selecionandoObjetos = true;

    void Start()
    {
        
        Debug.Log("Filhos painelObjeto: " + painelObjeto.transform.childCount);
        //Debug.Log("Filhos painelSombra: " + painelSombra.transform.childCount);

        for(int x = 0; x<painelObjeto.transform.childCount;x++)
        {
            objetos.Add(painelObjeto.transform.GetChild(x).gameObject);
        }
        //for(int x = 0; x<painelSombra.transform.childCount;x++)
        //{
            //sombras.Add(painelSombra.transform.GetChild(x).gameObject);
        //}

        Debug.Log("Objetos: " + objetos.Count);
        //Debug.Log("Sombras: " + sombras.Count);
    }

    void Update()
    {
        AtualizarIndice();
        if(Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //Debug.Log("indice: "+indice);
            if(selecionandoObjetos)
            {
                objetoSelecionado = objetos[indice];
                Debug.Log("Objeto selecionado: "+objetoSelecionado.name);

                selecionandoObjetos = false;
                indice = 0;
            }
            /*else
            {
                sombraSelecionada = sombras[indice];
                Debug.Log("Sombra selecionada: "+sombraSelecionada.name);

                verificarPareamento();

                objetoSelecionado = null;
                sombraSelecionada = null;
                selecionandoObjetos = true;
                indice = 0;
            }*/
        }
    }

    void AtualizarIndice()
    {
        if(objetos.Count == 0 ){return;}/*|| sombras.Count == 0) {return;}*/
        else
        //Debug.Log("indice dentro da função: "+indice);
        if(selecionandoObjetos)
        {    //objetos
            if(Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
            {
                //Debug.Log("items dentro de objetos: "+objetos.Count);
                //Debug.Log("items dentro de sombras: "+sombras.Count);
                indice++;
                //Debug.Log("indice apos incremento: "+indice);
                indice = Mathf.Clamp(indice, 0, objetos.Count - 1);
                //Debug.Log("indice apos mathf.clamp: "+indice);
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
            {
                indice--;
                indice = Mathf.Clamp(indice, 0, objetos.Count - 1);
            }
        }
        else
        {   //sombras
            if(Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
            {
                indice++;
                //indice = Mathf.Clamp(indice, 0, sombras.Count - 1);
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
            {
                indice--;
                //indice = Mathf.Clamp(indice, 0, sombras.Count - 1);
            }
        }
    }

    void verificarPareamento()
    {
        if(objetoSelecionado.name == sombraSelecionada.name)
        {
            Debug.Log("Par Correto");
        }
        else
            Debug.Log("Par Errado");
    }
}

