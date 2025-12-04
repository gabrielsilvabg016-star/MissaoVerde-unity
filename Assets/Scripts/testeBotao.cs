using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class testeBotao : MonoBehaviour
{
    public Button botao;
    void Start()
    {
        botao.onClick.AddListener(msgTeste);
    }

    public void msgTeste()
    {
        Debug.Log("teste");
    }
}
