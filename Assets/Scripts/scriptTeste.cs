using UnityEngine;
using UnityEngine.UI;

public class scriptTeste : MonoBehaviour
{
    //achar um jeito de fazer todo esse script funcionar so se detectar uso de setas
    //janela de config no inicio talvez
    public static scriptTeste Instancia;

    private GameObject lixoSelect;
    public GameObject painelObjetos;
    public GameObject painelLixeiras;

    void Awake()
    {
        Instancia = this;
    }
    void Start()
    {
        for(int x = 0; x<painelObjetos.transform.childCount; x++)
        {
            GameObject filho = painelObjetos.transform.GetChild(x).gameObject;
            Button btn = filho.GetComponent<Button>();

            btn.onClick.AddListener(() => QuandoClicarLixo(filho));
        }

        for(int x = 0; x<painelLixeiras.transform.childCount; x++)
        {
            GameObject filho = painelLixeiras.transform.GetChild(x).gameObject;
            Button btn = filho.GetComponent<Button>();

            btn.onClick.AddListener(() => QuandoClicarLixeira(filho));
        }
    }

    void QuandoClicarLixo(GameObject lixo)
    {
        Image img = lixo.GetComponent<Image>();
        lixoSelect = lixo;
        Debug.Log("Clicou em " + lixoSelect.name + " imagem: "+img.sprite.name);
    }

    void QuandoClicarLixeira(GameObject lixeira)
    {
        Image img = lixeira.GetComponent<Image>();
        lixeira.name = "lixeira";
        Debug.Log("Clicou em " + lixeira.name+ " imagem: "+img.sprite.name);
        Pareamento(lixeira);
    }

    void Pareamento(GameObject lixeira)
    {
        Image imgLixo = lixoSelect.GetComponent<Image>();
        Image imgLixeira = lixeira.GetComponent<Image>();

        string nomeLixo = imgLixo.sprite.name;
        string nomeLixeira = imgLixeira.sprite.name;

        nomeLixo = nomeLixo.Split('_')[1];
        nomeLixeira = nomeLixeira.Split('_')[1];

        if(nomeLixo == nomeLixeira)
        {
            Debug.Log("jogou fora: "+ lixoSelect.name);
            //tem que adicionar em algum canto daqui um replace sprite de lixeira vazia com sprite full
            //tocar o audio de acerto
            Destroy(lixoSelect);
            lixoSelect = null;
        }
        else
        {
            //fazer tocar o audio de erro
            Debug.Log("Seleção errada!");
        }
    }
}
