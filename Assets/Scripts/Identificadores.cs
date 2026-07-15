Using UnityEngine;
Using UnityEngine.UI;

public class Identificadores : MonoBehaviour{
    public GameObject painelObjetos;
    public GameObject painelLixeiras;
    public RegistroIdentificador[] identificadores;  //adiciona atraves do inspetor

    void Start(){

        int numObjetos = painelObjetos.transform.childCount;
        int numSombras = painelLixeiras.transform.childCount;

        for(int x = 0; x<numObjetos; x++){
            GameObject filho = painelObjetos.transform.GetChild(x).gameObject;
            Image sprt = filho.GetComponent<Image>();

            Identificador id = filho.AddComponent<Identificador>();

            AdicionarIdentificador(id, sprt);
        }

        for(int x = 0; x<numObjetos; x++){
            GameObject filho = painelLixeiras.transform.GetChild(x).gameObject;
            Image sprt = filho.GetComponent<Image>();

            Identificador id = filho.AddComponent<Identificador>();

            AdicionarIdentificador(id, sprt);
        }
    }

    void AdicionarIdentificador(Identificador id, Image imagem){
        string nome = imagem.sprite.name;

        foreach(RegistroIdentificador registro in identificadores)
        {
            if(registro.nomeSprite == nome)
            {
                Identificador.id = registro.id;
                return;
            }
        }
        Debug.LogWarning($"Nenhum identificador encontrado para o sprite '{nome}'.");
    }
}

public class RegistroIdentificador{
    public string nomeSprite;
    public string id;
}