using UnityEngine;
using UnityEngine.EventSystems;

public class pimbalholhas : MonoBehaviour
{

    //SerializedField permite uma variavel privada aparecer no inspetor enquanto impede outros scripts de alterar ela.// nome do scrip dado pelo J.P Cousen
    [SerializeField] private GameObject defaultBotao;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(defaultBotao);
    }
}
