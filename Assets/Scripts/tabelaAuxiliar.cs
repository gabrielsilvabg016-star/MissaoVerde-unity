using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tabelaAuxiliar : MonoBehaviour
{
    public GameObject imagemTabela;
    public Button botao;
    void Start() 
    {
        if (imagemTabela != null)
        {
            imagemTabela.SetActive(false); //seta a imagem como escondida
        }
        botao.onClick.AddListener(MostrarImagem);
    }

    public void MostrarImagem()
    {
        if(imagemTabela.activeSelf == false)
        {
            imagemTabela.SetActive(true); // esconde a imagem
        }
        else if (imagemTabela.activeSelf == true)
        {
            imagemTabela.SetActive(false);
        }
    }
}