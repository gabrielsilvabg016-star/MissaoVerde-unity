using UnityEngine;
using UnityEngine.UI;

/*Ideia e fazer um scrip similar ao da tabela porem pra canvas só.
ao clicar no botão = canvas anterior desativado > canvas do botão ativa > mostra a tela.
ao clicar no botão de saida do novo canvas = canvas e desativado > canvas principal ativa > mostra a tela.*/

public class canvasAuxiliar : MonoBehaviour
{
    public GameObject canvasMain;
    public GameObject canvasAux;
    private Button botao;

    void Start()
    {
        if(canvasAux != null)
        {
            canvasAux.SetActive(false);
        }
        botao = GetComponent<Button>();
        botao.onClick.AddListener(AtivarCanvas);
    }

    public void AtivarCanvas()
    {
        if(canvasAux.activeSelf == false)//canvas escondido
        {
            canvasMain.SetActive(false);
            canvasAux.SetActive(true);//ativa o canvas
        }
        else if(canvasAux.activeSelf == true)//canvas ativo
        {
            canvasMain.SetActive(true);
            canvasAux.SetActive(false);//desativa o canvas
        }
    }
}
