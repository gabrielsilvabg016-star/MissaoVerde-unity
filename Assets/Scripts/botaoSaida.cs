using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class botaoSaida : MonoBehaviour
{
        public GameObject canvas;
        public Button button;
        public GameObject painel;
        public GameObject buttonDefault;
        private bool estado = tabelaAuxiliar.estado;//recebe o estado do tabelaAuxiliar
    void Start()
    {
        button.onClick.AddListener(EsconderImagem);
    }

    public void EsconderImagem()
    {
        if(canvas.activeSelf == true)
        {
            for(int x = 0; x < painel.transform.childCount; x++)
            {
                painel.transform.GetChild(x).gameObject.SetActive(true);
            }
            EventSystem.current.SetSelectedGameObject(buttonDefault);
            canvas.SetActive(false);
            estado = false;//altera o estado local
            tabelaAuxiliar.estado = estado; //troca o valor do global pelo local
            Debug.Log("Canvas foi desativado");
            Debug.Log(estado);
        }
    }
}
