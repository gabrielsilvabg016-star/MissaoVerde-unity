using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tabelaAuxiliar : MonoBehaviour
{
    public GameObject cnvs;
    public Button botao;
    void Start() 
    {
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
            cnvs.SetActive(true); //mostra a imagem
        }
        else if (cnvs.activeSelf == true) //imagem mostrada
        {
            cnvs.SetActive(false); //esconde a imagem
        }
    }
}