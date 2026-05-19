using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private AudioSource audioAtual;

    void Awake()
    {
        instance = this;
    }

    public void PlayAudio(AudioSource newAudio)
    {
        //evita repitir o audio enquanto ele ta tocando
        if(audioAtual == newAudio && audioAtual.isPlaying)
        {
            return;
        }

        //se tem outro audio tocando ele para o atual
        if(audioAtual != null && audioAtual.isPlaying)
        {
            audioAtual.Stop();
        }

        audioAtual = newAudio;
        audioAtual.Play();
    }
}
