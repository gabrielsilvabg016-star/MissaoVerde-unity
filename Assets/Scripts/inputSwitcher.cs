using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputSwitcher : MonoBehaviour
{
    public GameObject primeiroBotao;
    public Toggle toggleSom;
    private bool Mouse = true;
    public bool NavSom = false; //se ativado toca som ao navegar pelos objetos
    private AudioSource audioSource;
    public AudioClip SomMovimento;
    private GameObject ultimoSelect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //carrega
        NavSom = (PlayerPrefs.GetInt("NavSom") != 0);
        if(toggleSom)
        {
            toggleSom.isOn = NavSom;
        }
    }

    void Update()
    {
        DetectMouse();//mouse = hover
        DetectTeclado();//teclado = seleção
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
                    audioSource.PlayOneShot(SomMovimento);
                }
                ultimoSelect = current;
            }
    }

    public void SetActiveNavSom(bool value)
    {
        NavSom = value;
        PlayerPrefs.SetInt("NavSom", value ? 1 : 0);
        PlayerPrefs.Save();
    }
    
}
