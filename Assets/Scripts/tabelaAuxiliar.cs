using UnityEngine;
using UnityEngine.UI;


public class tabelaAuxiliar : MonoBehaviour
{
    public GameObject cnvs;
    public Button botao;
    public GameObject painel;

    public static bool estado = false;

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
        if(cnvs.activeSelf == false) //tabela escondida
        {
            for(int x = 0; x<painel.transform.childCount; x++)
            {
                painel.transform.GetChild(x).gameObject.SetActive(false);
            }

            estado = true;
            cnvs.SetActive(true); //mostra a tabela
            //Debug.Log("teste imagem escondida");
            
        }
        else if (cnvs.activeSelf == true) //tabela mostrada
        {
            for(int x = 0; x<painel.transform.childCount; x++)
            {
                painel.transform.GetChild(x).gameObject.SetActive(true);
            }

            estado = false;
            cnvs.SetActive(false); //esconde a tabela
            //Debug.Log("teste imagem mostrada");
        }
    }
}