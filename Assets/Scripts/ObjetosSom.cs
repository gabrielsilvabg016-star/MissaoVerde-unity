using UnityEngine.UI;
using UnityEngine;

public class ObjetosSom : MonoBehaviour //script que captura ambos objetos e sombras e insere um audioClip neles
{
    public GameObject painelObjeto;
    public GameObject painelSombra;

    void Start()
    {
        ConfigurarAudio();
    }

    public void ConfigurarAudio()
    {
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

    void CatchSomSombra(GameObject filho, AudioSource saidaAudio)//roda uma vez para cada sombra
    {
        Debug.Log("entrou no CatchSomSombra");
        AudioClip[] audios;//array de audios
        Image img = filho.GetComponent<Image>();

        int indiceSombra;
        int indiceAudio;
        string nomeSombra = img.sprite.name;

        audios = Resources.LoadAll<AudioClip>("sonsGerais/sonsAuxiliares/Sombras");//bota todos os audios no array
        indiceSombra = int.Parse(nomeSombra.Split('_')[1]); //transforma o numero da sombra em int
        indiceAudio = indiceSombra % audios.Length; //calcula indice divisivel pelo tamanho do array

        saidaAudio.clip = audios[indiceAudio]; //em teoria roda, bota o audio relacionado ao resultado do calculo de indice
        Debug.Log("nome do audio: " + audios[indiceAudio].name);
        Debug.Log("Saiu do CatchSomSombra");
    }
}
