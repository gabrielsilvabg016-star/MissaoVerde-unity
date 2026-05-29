using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputSwitcher : MonoBehaviour
{
    public GameObject primeiroBotao;
    public Toggle toggleSom; //e apenas utilizado na fase inicial para setar o valor natural do toggle de som, pode ignorar nas outras fases
    private bool Mouse = true;
    public bool NavSom; //se ativado toca som de navegar de cada objeto
    private AudioSource audioSource;
    private GameObject ultimoSelect;

    void Start()
    {
        //carrega valor do PlayerPrefs
        //playerPrefs funciona como um minibanco de dados do jogo, se da ultima vez o navsom foi ativado, ficou salvo la dentro como ativo
        //e vai sempre carregar ativo ate que seja desativado na tela inicial no toggle de som, independente do toggle do som
        
        NavSom = PlayerPrefs.GetInt("NavSom", 0) != 0;

        if(toggleSom)//deixa o toggle da fase inicial visualmente ativo
        {
            toggleSom.isOn = NavSom;
        }
    }

    void Update()
    {
        DetectMouse();//mouse = hover
        DetectTeclado();//teclado = seleção
        Debug.Log(EventSystem.current.currentSelectedGameObject);
    }

    void DetectMouse()
    {
        //movimento do mouse
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if(!Mouse)
            {
                Mouse = true;
                EventSystem.current.SetSelectedGameObject(null); //remove a seleção do menu
            }
        }

        //clique do mouse
        if(Input.GetMouseButtonDown(0))
        {
            if(!Mouse)
            {
                Mouse = true;
                EventSystem.current.SetSelectedGameObject(null); //remove a seleção do menu
            }
        }
    }
    void DetectTeclado()
    {
        //pressionamento de uma tecla
        if(Input.anyKeyDown)
        {
            if(Input.GetMouseButtonDown(0))//clique do mouse conta como anyKeyDown, isso evita conflito
                return;

            if(Mouse)
            {
                Mouse = false;

                if(EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(primeiroBotao);//nenhum botão selecionado, seleciona a variavel primeiro botão
                }

                if (NavSom)
                {
                    TocarSom();
                }
            }
        }

        //detecta teclas WASD e analogico de controle
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if(Mouse)
            {
                Mouse = false;

                if(EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(primeiroBotao);
                }
            }

            if (NavSom)
            {
                Debug.Log("entrou no if de tocarSom");
                TocarSom();
            }
        }
    }

    void TocarSom()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;
            if(current != ultimoSelect)
            {
                if(current != null)
                {
                    audioSource = current.GetComponent<AudioSource>();

                    if(audioSource != null && audioSource.clip != null)
                    {
                        AudioController.instance.PlayAudio(audioSource);
                        //audioSource.Play();
                        //audioSource.PlayOneShot toca o som sem interromper outro som que esteja tocando
                        //audioSource.Play() toca o som principal interrompendo qualquer outro som que esse audioSource esteja tocando
                    } else
                    Debug.Log("audioSource ou audioClip estão nulos");
                }

                ultimoSelect = current;
            }
    }

    public void SetActiveNavSom(bool value)
    {
        NavSom = value;
        if(NavSom)
        {
            ultimoSelect = null;
        }
        PlayerPrefs.SetInt("NavSom", value ? 1 : 0);
        PlayerPrefs.Save();
    }
    
}
