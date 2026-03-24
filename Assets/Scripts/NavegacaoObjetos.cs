using System.Collections.Generic;
using UnityEngine;

public class NavegacaoObjetos : MonoBehaviour
{
    public GameObject prefabObjeto; // prefab a ser instanciado
    private List<GameObject> objetosCriados = new List<GameObject>();
    private int indiceAtual = 0;

    public int quantidadeItems;

    void Start()
    {
        // Exemplo: criar 5 objetos ao iniciar
        for (int i = 0; i < quantidadeItems; i++)
        {
            GameObject obj = Instantiate(prefabObjeto, this.transform);
            obj.name = "Objeto_" + i;
            objetosCriados.Add(obj);

            // opcional: desativar todos menos o primeiro
            obj.SetActive(i == 0);
        }
    }

    public void ProximoObjeto()
    {
        if (objetosCriados.Count == 0) return;

        objetosCriados[indiceAtual].SetActive(false);
        indiceAtual = (indiceAtual + 1) % objetosCriados.Count;
        objetosCriados[indiceAtual].SetActive(true);
    }

    public void ObjetoAnterior()
    {
        if (objetosCriados.Count == 0) return;

        objetosCriados[indiceAtual].SetActive(false);
        indiceAtual = (indiceAtual - 1 + objetosCriados.Count) % objetosCriados.Count;
        objetosCriados[indiceAtual].SetActive(true);
    }
}