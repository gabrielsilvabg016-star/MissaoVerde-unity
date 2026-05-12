using UnityEngine.UI;
using UnityEngine;
using Mono.Cecil;

public class ObjetosSom : MonoBehaviour //script que captura ambos objetos e sombras e insere um audioClip neles
{
    public GameObject painelObjeto;
    public GameObject painelSombra;
    private bool NavSom;

    void Start()
    {
        //carrega valor do PlayerPrefs
        NavSom = (PlayerPrefs.GetInt("NavSom") != 0);
        Debug.Log("carregou o PlayerPrefs: "+NavSom);
        if(NavSom == false)//caso NavSom esteja desativado esse script faz nada
        {
            Debug.Log("entoru no if de navsom");
            return;
        }

        for(int x = 0; x<painelObjeto.transform.childCount; x++)//objetos
        {
            Debug.Log("entrou no for");
            GameObject filho = painelObjeto.transform.GetChild(x).gameObject; //objeto
            AudioSource saidaAudio = filho.GetComponent<AudioSource>(); //saida de audio
            CatchSomObjeto(filho, saidaAudio);
            Debug.Log("terminou no for de objeto som");
        }

        //lembrar que lista de objetos e maior que sombras//

        for(int x =0; x<painelSombra.transform.childCount; x++)//sombras
        {
            GameObject filho = painelSombra.transform.GetChild(x).gameObject; //objeto
            AudioSource saidaAudio = filho.GetComponent<AudioSource>(); //saida de audio
            CatchSomSombra(filho, saidaAudio);
        }
    }

    void CatchSomObjeto(GameObject filho, AudioSource saidaAudio)
    {
        Debug.Log("entrou no CatchSomObjeto");
        Image img = filho.GetComponent<Image>(); //sprite
        string nomeObjeto = img.sprite.name;
        nomeObjeto = nomeObjeto.Split('_')[1];
        
        AudioClip clip = Resources.Load<AudioClip>("sonsGerais/sonsAuxiliares/Objetos/audioObjeto_"+nomeObjeto);//procura na pasta sonsAuxiliares sons objetos
        Debug.Log("nome do audio clip objeto: "+clip.name);

        saidaAudio.clip = clip;
    }

    void CatchSomSombra(GameObject filho, AudioSource saidaAudio)
    {
        Image img = filho.GetComponent<Image>();
        string nomeObjeto = img.sprite.name;
        nomeObjeto = nomeObjeto.Split('_')[1];

        AudioClip clip = Resources.Load<AudioClip>("sonsGerais/sonsAuxiliares/Sombras/audioSombra_"+nomeObjeto);//procura na pasta sonsAuxiliares sons sombra
        Debug.Log("nome do audio clip sombra: "+clip.name);

        saidaAudio.clip = clip;
    }
}
