using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*Ideia e fazer um scrip similar ao da tabela porem pra canvas só.
ao clicar no botão = canvas anterior desativado > canvas do botão ativa > mostra a tela.
ao clicar no botão de saida do novo canvas = canvas e desativado > canvas principal ativa > mostra a tela.*/

public class canvasAuxiliar : MonoBehaviour
{
    public GameObject canvasMain;
    public GameObject canvasAux;
    private Button botao;
    
    //SerializedField permite uma variavel privada aparecer no inspetor enquanto impede outros scripts de alterar ela.
    [SerializeField]private GameObject botaoDefault;

    void Start()
    {
        /*if(canvasAux != null)
        {
            canvasAux.SetActive(false);
        }*/
        botao = GetComponent<Button>();
        botao.onClick.AddListener(AtivarCanvas);
    }

    public void AtivarCanvas()
    {
        if(canvasAux.activeSelf == false)//canvas escondido
        {
            canvasAux.SetActive(true);//ativa o canvas
            canvasMain.SetActive(false);
            EventSystem.current.SetSelectedGameObject(botaoDefault);
        }
        else if(canvasAux.activeSelf == true)//canvas ativo
        {
            canvasAux.SetActive(false);//desativa o canvas
            canvasMain.SetActive(true);
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
    }
}
