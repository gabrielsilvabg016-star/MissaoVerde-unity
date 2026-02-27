using UnityEngine;
using UnityEngine.UI;


public class tabelaAuxiliar : MonoBehaviour
{
    public GameObject cnvs;
    public Button botao;
    public GameObject painel;

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
            for(int x = 0; x<painel.transform.childCount; x++)
            {
                painel.transform.GetChild(x).gameObject.SetActive(false);
            }

            cnvs.SetActive(true); //mostra a imagem
            //Debug.Log("teste imagem escondida");
            
        }
        else if (cnvs.activeSelf == true) //imagem mostrada
        {
            for(int x = 0; x<painel.transform.childCount; x++)
            {
                painel.transform.GetChild(x).gameObject.SetActive(true);
            }
            cnvs.SetActive(false); //esconde a imagem
            //Debug.Log("teste imagem mostrada");
        }
    }
}