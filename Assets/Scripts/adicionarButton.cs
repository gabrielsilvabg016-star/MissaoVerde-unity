using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class adicionarButton : MonoBehaviour
{
    public GameObject painel; // painel onde o item será filho


    void Start()
    {
        for(int x = 0; x < painel.transform.childCount; x++)
        {
            GameObject item = painel.transform.GetChild(x).gameObject;

            Image img = item.AddComponent<Image>();
            img.color = Color.green;

            Button btn = item.AddComponent<Button>();
            btn.interactable = true;
            btn.onClick.AddListener(() => OnItemClicado(item));
        }
    }

    void OnItemClicado(GameObject item)
    {
        Debug.Log("Item clicado: "+ item.name);
    }
}