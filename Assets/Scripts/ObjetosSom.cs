using UnityEngine;

public class ObjetosSom : MonoBehaviour //script que captura ambos objetos e sombras e insere um audioClip neles
{
    public GameObject painelObjeto;
    public GameObject painelSombra;
    private bool NavSom;

    void start()
    {
        //carrega valor do PlayerPrefs
        NavSom = (PlayerPrefs.GetInt("NavSom") != 0);

        if(!NavSom)//caso NavSom esteja desativado esse script faz nada
        {
            return;
        }

        for(int x =0; x<painelObjeto.transform.childCount; x++)//objetos
        {
            GameObject filho = painelObjeto.transform.GetChild(x).gameObject; //objeto
            AudioSource saidaAudio = filho.GetComponent<AudioSource>(); //saida de audio
            catchSomObjeto(filho, saidaAudio);
        }

        //lembrar que lista de objetos e maior que sombras//

        for(int x =0; x<painelSombra.transform.childCount; x++)//sombras
        {
            GameObject filho = painelSombra.transform.GetChild(x).gameObject; //objeto
            AudioSource saidaAudio = filho.GetComponent<AudioSource>(); //saida de audio
            catchSomSombra(filho, saidaAudio);
        }
    }

    void catchSomObjeto(GameObject filho, AudioSource saidaAudio)
    {
        Image img = filho.GetComponent<Image>(); //sprite
        string nomeObjeto = img.sprite.name;
        nomeObjeto = nomeObjeto.Split('_')[1];

        for(int x = 0; x<painelObjeto.transform.childCount; x++)
        {
            AudioClip clip = Resources.Load<AudioClip>("sonsGerais/sonsAuxiliares/audio_"+x);//procura na pasta sonsAuxiliares
        
            if(saidaAudio != null && clip != null)
            {
                if(x.ToString() == nomeObjeto)
                {
                    saidaAudio.clip = clip;
                }
            }
        }
    }

    void catchSomSombra(GameObject filho, AudioSource saidaAudio)
    {
        //continuar montando depois, mesma logica do catchSomObjeto();
    }
}
