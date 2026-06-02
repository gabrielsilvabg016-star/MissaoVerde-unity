using UnityEngine;
using UnityEngine.Video;

public class vidPlayer : MonoBehaviour
{
    [SerializeField] string videoFileName; //nome do video, tem que estar no formato [nome].[formato (mp4,wav ou outro)]
    void Start()
    {
        PlayVideo();
    }

        void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);//pega o caminho para a pasta StreamingAssets e combina com o nome inserido no videoFileName
            Debug.Log(videoPath); //mostra o caminho
            videoPlayer.url =  videoPath; //inseri dentro da url do videoPlayer
            videoPlayer.Play(); //toca o video, tecnicamente burlando o cors da web
        }
    }
}
