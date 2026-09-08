using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class testeBotao : MonoBehaviour
{
    [TextArea]
    [SerializeField] public string texto;
    public void msgTeste()
    {
        Debug.Log(texto);
    }
}
