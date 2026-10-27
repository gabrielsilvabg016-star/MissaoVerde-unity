using static Ludus.SDK.Framework.Configuracao;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

//continuar apartir do script de FaseLayout do framework

namespace Ludus.SDK.Framework
{
    public class FaseLayoutMemoria : MonoBehaviour
    {
        [Header("Configuração da Fase")]

        public FormasdeInteracao formaDeInteracao = FormasdeInteracao.CliqueSimples;
        public Button botaoTrocarCena;
        public string pasta;
        public string cenaFinal;
        public List<Nivel> niveis;

        [Header("Configurações - Auxiliar")]
        public bool conteudoauxiliar;
        public bool substituirObjetoAoParear;

        public Auxiliares sombraauxiliar = Auxiliares.Direita;

        [Header("Dimensões das Imagens")]
        [Tooltip("Usar largura e altura padrão")]

        public bool usarDimensoesImagens = false;
        public int objetoLargura = 100, objetoAltura = 100, sombraLargura = 100, sombraAltura = 100;
        public int sombraAuxiliarLargura = 100, sombraAuxiliarAltura = 100;

        [Header("Apenas para Interação por Clique")]
        public bool trocarImagemSombraAoClicar = false;
        [Header("Monitoramento")]
        public bool monitorarCena;

        protected GameObject objeto;
        protected GameObject sombra;
        protected GameObject painel;
        protected List<Image> imgsObjeto;
        protected List<Image> imgsObjetoPareado;
        protected List<AudioSource> audiosSourceObjetos;

        protected List<Sprite> spritesObjeto;
        protected List<Sprite> spritesObjetoPareado;
        protected List<Sprite> spritesSombra;
        protected List<AudioClip> audiosObjeto;
        protected List<Sprite> spriteAuxiliar;
        protected List<AudioClip> audiosAuxiliar;

        //Legenda
        protected TextMeshProUGUI txtLegenda;
        string[] textos;
        protected string[] textosAuxiliar;
        protected bool temLegendaObjeto, temLegendaAuxiliar;

        protected virtual void CarregarConfiguracao()
        {
            #region CarregarConfiguracao
            //carrega os paineis
            try
            {
                painel = GameObject.Find(this.gameObject.name.ToString());
                objeto = GameObject.Find("PainelObjeto");
                sombra = GameObject.Find("PainelSombra");
                //busca o text de legenda (opcional ter na tela)

                try
                {
                    GameObject goTexto = GameObject.Find("Legenda");
                    if(goTexto != null)
                    {
                        txtLegenda = goTexto.GetComponent<TextMeshProUGUI>();
                    }
                } catch (System.Exception)
                {
                    throw;
                }

            } catch(System.Exception ex)
            {
                Debug.LogError("[+LUDUS] Erro ao instanciar as vari�veis, verifique se os pain�is est�o dispostos de acordo com o preFab");
                Debug.LogException(ex);
                return;
            }

            //Se habilitado, adiciona o monitoramento a cena atraves do script Monitor
            if (this.monitorarCena)
            {
                this.gameObject.AddComponent<Monitor>();
            }

            //associa os valores passados visualmente no painel pra classe estatica de Controle
            try
            {
                Controle.configuracao.botaoTroca = this.botaoTrocarCena;

                if(this.niveis == null || this.cenaFinal == null)
                {
                    Debug.LogError("[+LUDUS] Erro ao carregar a configura��o da fase. Verifique se os n�veis e a cena final foram corretamente atribu�das no inspector do painel ");
                    return;
                }
                Controle.configuracao.formaDeInteracao = this.formaDeInteracao;
                Controle.configuracao.niveis = this.niveis;
                Controle.configuracao.cenaFinal = this.cenaFinal;
                Controle.configuracao.conteudoauxiliar = this.conteudoauxiliar;
                Controle.configuracao.sombraauxiliar = this.sombraauxiliar;
                Controle.configuracao.substituirObjetoAoParear = this.substituirObjetoAoParear;

                //Altura e Largura
                Controle.configuracao.objetoAltura = this.objetoAltura;
                Controle.configuracao.objetoLargura = this.objetoLargura;
                Controle.configuracao.sombraAltura = this.sombraAltura;
                Controle.configuracao.sombraLargura = this.sombraLargura;
                Controle.configuracao.sombraAuxiliarAltura = this.sombraAuxiliarAltura;
                Controle.configuracao.sombraAuxiliarLargura = this.sombraAuxiliarLargura;
                Controle.configuracao.txtLegenda = this.txtLegenda;

                //Clique
                Controle.configuracao.trocarImagemSombraAoClicar = this.trocarImagemSombraAoClicar;

                //Carrega as configurações da fase
                Controle.configuracao.CarregarConfiguracao(painel);
            } catch (System.Exception ex)
            {
                Debug.LogError("[+LUDUS] Erro ao carregar a fase, verifique as configurações.");
                Debug.LogException(ex);
                return;
            }
            #endregion
        }

        protected virtual void CarregarAssetsPastas()
        {
            //verifica se a variavel de pasta foi definida
            if (string.IsNullOrEmpty(pasta))
            {
                Debug.LogError("[+LUDUS] Variavel pasta não defindida, verifique as variaveis do script e adicione a pasta corresponde aos sprites. Dica: procure na pasta Resources/fases e busque O NOME DA FASE DESEJADA");
                return;
            }

            try
            {
                //função Carregar Assets e feita mais para frente
                spritesObjeto = this.CarregarAssets<Sprite>("objeto");
                spritesSombra = this.CarregarAssets<Sprite>("sombra");
            } catch (System.Exception ex)
            {
                Debug.LogError("[+LUDUS] Sprites n�o encontrados. Verifique se a pasta fases/" + pasta + "/objeto e fases/" + pasta + "/sombra existem dentro da pasta Resources, e se os os sprites est�o corretamente divididos (multiple)");
                Debug.LogException(ex);
                return;
            }
            #region legendaObjeto
            //busca o conteudo da legenda do objeto, caso exista
            textos = this.CarregarAssets("texto", out this.temLegendaObjeto);
            #endregion

            if (Controle.configuracao.substituirObjetoAoParear)
            {
                spritesObjetoPareado = this.CarregarAssets<Sprite>("objetoPareado");
            }

            //carregar audios do objeto
            try
            {
                audiosObjeto = this.CarregarAssets<AudioClip>("objetoSons");
                audiosObjeto = this.ordenarAudio(audiosObjeto);
            } catch (System.Exception)
            {
                audiosObjeto = new List<AudioClip>();
            }

            //se tem conteudo auxiliar busca as imagens desse conteudo e sons
            if (this.conteudoauxiliar)
            {
                spriteAuxiliar = this.CarregarAssets<Sprite>("Auxiliar");

                //busca os sons auxiliares das sombras - caso não tenha atribui null ao audioAuxiliar
                try
                {
                    audiosAuxiliar = this.CarregarAssets<AudioClip>("auxiliarSons");
                    audiosAuxiliar = this.ordenarAudio(audiosAuxiliar);
                }
                catch (System.Exception)
                {
                    Debug.LogWarning("[+LUDUS] �udios auxiliares n�o encontrados. Caso estejam na pasta verifique se a pasta fases/" + pasta + "/auxiliarsons existe dentro da pasta Resources");
                    audiosAuxiliar = null;
                }
                #region legendaauxiliar
                this.textosAuxiliar = this.CarregarAssets("textoAuxiliar", out this.temLegendaAuxiliar);
                #endregion
            }

            if (spritesObjeto.IsUnityNull())
            {

                Debug.LogError("[+LUDUS] spritesObjeto n�o definido");
                return;
            }
            if (spritesSombra.IsUnityNull())
            {

                Debug.LogError("[+LUDUS] spritesSombra n�o definido");
                return;
            }

            //atualiza o booleano de legenda
            Controle.configuracao.temLegendaAuxiliar = this.temLegendaAuxiliar;
            Controle.configuracao.temLegendaObjeto = this.temLegendaObjeto;
        }

        protected virtual void CarregarObjetosPainel()
        {
            try
            {
                //carrega os GameObjects Image do painel OBJETO
                //são duas iamgens, caso a ideia seja trocar o elemento ao parear.

                if (this.substituirObjetoAoParear)
                {
                    //se tem que manter a informação da imagem que vai ser colada ao parear,
                    // busca os inativos do prefab pra poder trocar depois

                    List<Image> imgs = objeto.GetComponentsInChildren<Image>(true).ToList<Image>();
                    imgs = this.RetirarImagem(objeto, imgs);
                    imgsObjeto = imgs.Where(s => s.gameObject.activeInHierarchy).ToList<Image>();
                    imgsObjetoPareado = imgs.Where(s => !s.gameObject.activeInHierarchy).ToList<Image>();
                }
                else
                {
                    imgsObjeto = objeto.GetComponentsInChildren<Image>().ToList<Image>();
                    imgsObjeto = this.RetirarImagem(objeto, imgsObjeto);
                }

                //lista de elementos de audio do painel OBJETO
                audiosSourceObjetos = objeto.GetComponentsInChildren<AudioSource>().ToList<AudioSource>();
            }catch (System.Exception ex)
            {
                Debug.LogError("[+LUDUS] Erro ao buscar as imagens dentro do painel objeto', verifique se as imagens est�o corretamente dispostas na hierarquia de elemetos");
                Debug.LogException(ex);
                return;
            }
        }

        //m�todo para retirar da lista de filhos do Painel o compontente que
        //pertence ao pr�prio objeto
        protected virtual List<Image> RetirarImagem(GameObject painel, List<Image> imgs)
        {
            if(painel.GetComponent<Image>() != null)
            {
                imgs.Remove(painel.GetComponent<Image>());
            }

            return imgs;
        }

        protected virtual int IndiceNovo(List<int> indiceSelecionado)
        {
            int novoindice;
            /*fatiar
            atual
            pedaços
            tamanho
            
            atual = 0
            pedaços = 0
            tamanho = 0*/

            novoindice = Random.Range(0, spritesObjeto.Count);

            int tentativas = 1;

            //verifica se o indice ja apareceu naquele nivel e se as tentativas de achar o indice novo acabaram
            //a variavel tentativas e um controle para que o jogo não tranque caso não tenha sprites disponiveis para exibir naquela fase,
            //nesse caso vai aceitar imagem repetida
            while (Controle.configuracao.VerificaOcorrenciaIndiceNoNivel(novoindice))
            {
                novoindice = Random.Range(0, spritesObjeto.Count);
                tentativas++;

                //se o numero de itens disponiveis para a cena for igual a quantidade de objetos ja exibidos zera os exibidos
                if(Controle.configuracao.objetosJaExibidos.Count == spritesObjeto.Count)
                {
                    Controle.configuracao.ZerarExibidos();
                }
            }

            indiceSelecionado.Add(novoindice);
            return novoindice;
        }

        protected virtual List

        private List<AudioClip> ordenarAudio(List<AudioClip> audiosObjeto)
        {
            AudioClip aux;
            string vetori, vetorj;

            for(int i = 0; i < audiosObjeto.Count; i++)
            {
                for(int j = 0; j < audiosObjeto.Count; j++)
                {
                    vetori = audiosObjeto[i].name.ToString().Split('_')[1];
                    vetorj = audiosObjeto[j].name.ToString().Split('_')[1];

                    if(System.Int32.Parse(vetori) < System.Int32.Parse(vetorj))
                    {
                        //aqui acontece a troca, maior para direita, menor para esquerda
                        aux = audiosObjeto[i];
                        audiosObjeto[i] = audiosObjeto[j];
                        audiosObjeto[j] = aux;
                    }
                }
            }

            return audiosObjeto;
        }

        public List<T> CarregarAssets<T>(string asset) where T: Object
        {
            try
            {
                string caminho = $"fases/{pasta}/{asset}";
                return Resources.LoadAll<T>(caminho).ToList();
            }catch (System.Exception ex)
            {
                Debug.LogError($"[+LUDUS] Erro ao carregar o asset na pasta {pasta}/{asset}.");
                Debug.LogError(ex.StackTrace);
                return null;
            }
        }

        public string[] CarregarAssets(string asset, out bool temLegenda)
        {
            string[] conteudo;

            try
            {
                string caminho = $"fases/{pasta}/{asset}";
                TextAsset texto = Resources.Load<TextAsset>(caminho);

                if(texto != null)
                {
                    temLegenda = true;
                    conteudo = texto.text.Split(";");
                }
                else
                {
                    temLegenda = false;
                    conteudo = null;
                    Debug.LogWarning("[+LUDUS] Nenhuma Legenda para o Objeto");
                }
            }catch (System.Exception ex)
            {
                Debug.LogError($"[+LUDUS] Erro ao carregar o asset na pasta {pasta}/{asset}.");
                Debug.LogError(ex.StackTrace);
                conteudo = null;
                temLegenda = false;
            }

            return conteudo;
        }
    }
}
