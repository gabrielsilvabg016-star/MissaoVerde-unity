using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ObjetosSom : MonoBehaviour //script que captura ambos objetos e sombras e insere um audioClip neles
{
    public GameObject painelObjeto;
    public GameObject painelSombra;
    public bool travaCriacao = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(InputSwitcher.NavSom && travaCriacao == false)
        {
            if(ControllerSetas.quantItems > 0)
            {
                ConfigurarAudio();
                travaCriacao = true;
            }
        }
    }

    public void ConfigurarAudio()
    {
        for(int x = 0; x<painelObjeto.transform.childCount; x++)//objetos
        {
            //Debug.Log("entrou no for");
            GameObject filho = painelObjeto.transform.GetChild(x).gameObject; //objeto
            AudioSource saidaAudio = filho.GetComponent<AudioSource>();
            saidaAudio.playOnAwake = false; //saida de audio
            CatchSomObjeto(filho, saidaAudio);
            //Debug.Log("terminou no for de objeto som");
        }

        //lembrar que lista de objetos e maior que sombras//

        for(int x =0; x<painelSombra.transform.childCount; x++)//sombras
        {
            GameObject filho = painelSombra.transform.GetChild(x).gameObject; //objeto
            AudioSource saidaAudio = filho.GetComponent<AudioSource>(); //saida de audio
            saidaAudio.playOnAwake = false;
            CatchSomSombra(filho, saidaAudio);
        }
    }

    void CatchSomObjeto(GameObject filho, AudioSource saidaAudio)
    {
        //Debug.Log("entrou no CatchSomObjeto");
        Image img = filho.GetComponent<Image>(); //sprite
        string nomeObjeto = img.sprite.name;
        nomeObjeto = nomeObjeto.Split('_')[1];
        
        AudioClip clip = Resources.Load<AudioClip>("sonsGerais/sonsAuxiliares/Objetos/audioObjeto_"+nomeObjeto);//procura na pasta sonsAuxiliares sons objetos
        //Debug.Log("nome do audio clip objeto: "+clip.name);

        saidaAudio.clip = clip;
        saidaAudio.playOnAwake = false;
    }

    void CatchSomSombra(GameObject filho, AudioSource saidaAudio)//roda uma vez para cada sombra
    {
        //Debug.Log("entrou no CatchSomSombra");
        Image img = filho.GetComponent<Image>();
        string nomeSombra = img.sprite.name;
        nomeSombra = nomeSombra.Split('_')[1];

        AudioClip clip = Resources.Load<AudioClip>("sonsGerais/sonsAuxiliares/Sombras/audioSombra_"+nomeSombra);//bota todos os audios no array

        saidaAudio.clip = clip;
        saidaAudio.playOnAwake = false;
    }
}
