using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverBotao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button botao;

    void Start()
    {
        //Image imgBotao = botao.GetComponent<>//
        //continuar o codigo de cima dps, alterar o alpha
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(botao.IsActive())//botao ativo
        {
            botao.a
        }
        else
            Debug.Log("Botao entrou || Botão desativado");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(botao.IsActive())//botao desativado
        {
            Debug.Log("Botao saiu || Botao ta ativado");
        }
        else
            Debug.Log("Botao saiu || Botão desativado");
    }
}
