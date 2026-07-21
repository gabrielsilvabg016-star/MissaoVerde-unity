using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSetas : MonoBehaviour
{
    
    private static ControllerSetas Instancia;

    private GameObject lixoSelect;
    public GameObject painelObjetos;
    public GameObject painelLixeiras;
    public GameObject proxNivel;
    public Boolean substituirAposParear;
    private AudioSource audioSource;
    public AudioClip somAcerto;
    public AudioClip somErro;
    private bool travaBotao = false;
    private int numObjetos;
    private int numSombras;
    private int quantItems;

    void Awake()
    {
        Instancia = this;
    }
    void Start()
    {
        numObjetos = painelObjetos.transform.childCount;
        numSombras = painelLixeiras.transform.childCount;

        for(int x = 0; x<numObjetos; x++)//captura os filhos do painel, pega o elemento button e adiciona a função ClicarLixo
        {
            GameObject filho = painelObjetos.transform.GetChild(x).gameObject;
            Button btn = filho.GetComponent<Button>();

            btn.onClick.AddListener(() => {
            Debug.Log(filho == null);
            ClicarLixo(filho);
            });
        }

        for(int x = 0; x<numSombras; x++)//captura os filhos do painel, pega o elemento button e adiciona a função ClicarLixeira
        {
            GameObject filho = painelLixeiras.transform.GetChild(x).gameObject;
            Button btn = filho.GetComponent<Button>();

            btn.onClick.AddListener(() => {
            Debug.Log(filho == null);
            ClicarLixeira(filho);
            });
        }

        audioSource = GetComponent<AudioSource>();//saida de audio geral para o sistema de pareamento
    }

    void Update()
    {
        quantItems = painelObjetos.transform.childCount;

        if(quantItems > 0)
        {
            //Debug.Log("entrou no if quantItens");
            for(int x = 0; x<painelObjetos.transform.childCount; x++)
            {
                GameObject filho = painelObjetos.transform.GetChild(x).gameObject;

                if(!filho.activeSelf && tabelaAuxiliar.estado == false)
                {
                    //Debug.Log("entrou no if lixoSelect");
                    Destroy(filho);
                    quantItems -= 1;
                    break;
                }
            }
        }

        if(quantItems != numObjetos)
        {
            Debug.Log("valor do quantItems: "+quantItems);
            numObjetos = quantItems;

            if(travaBotao == false && numObjetos <= 0)
            {
                AtivarBotaoProxNivel();
            }
        }
    }

    void ClicarLixo(GameObject lixo)
    {
        Image img = lixo.GetComponent<Image>();
        lixoSelect = lixo;
        Debug.Log("Clicou em " + lixoSelect.name + " imagem: "+img.sprite.name);
    }

    void ClicarLixeira(GameObject lixeira)
    {
        Image img = lixeira.GetComponent<Image>();
        lixeira.name = "lixeira";
        Debug.Log("Clicou em " + lixeira.name+ " imagem: "+img.sprite.name);
        if(lixoSelect == null)
        {
            Debug.Log("Selecione um lixo antes de selecionar uma lixeira");
        }
        Pareamento(lixeira);
        Debug.Log(painelObjetos.transform.childCount);
        if(painelObjetos.transform.childCount <= 1)
        {
            AtivarBotaoProxNivel();
        }
    }

    void Pareamento(GameObject lixeira)
    {
        Image imgLixo = lixoSelect.GetComponent<Image>();
        Image imgLixeira = lixeira.GetComponent<Image>();
        //Button btn = lixeira.GetComponent<Button>();

        string nomeLixo = imgLixo.sprite.name;
        string nomeLixeira = imgLixeira.sprite.name;

        nomeLixo = nomeLixo.Split('_')[1];
        nomeLixeira = nomeLixeira.Split('_')[1];

        if(nomeLixo == nomeLixeira)
        {
            Debug.Log("jogou fora: "+ lixoSelect.name);
            if(substituirAposParear)
            {
                Sprite[] arraySprites = Resources.LoadAll<Sprite>("fases/reciclagemFase1/objetoPareado"); //so alterar esse caminho para alterar a imagem pareada;
                //Debug.Log("entrou no if de objetoPareado");
                //Debug.Log("largura do array: "+arraySprites.Length);
                foreach(Sprite novo in arraySprites)
                    {
                        string novoSprite = novo.name.Split('_')[1];
                        if(nomeLixeira == novoSprite)
                        {
                            imgLixeira.sprite = novo;
                            break;
                        }
                    }           
            }
            audioSource.PlayOneShot(somAcerto);
            Destroy(lixoSelect);
            lixoSelect = null;
            //btn.interactable = false; //deixar false impede a navegação por esse objeto;
        }
        else
        {
            lixoSelect = null;
            audioSource.PlayOneShot(somErro);
            Debug.Log("Seleção errada!");
        }
    }

    void AtivarBotaoProxNivel()
    {
        travaBotao = true;
        Debug.Log("entrou na função ativar botão");
        proxNivel.SetActive(true);
    }
}
