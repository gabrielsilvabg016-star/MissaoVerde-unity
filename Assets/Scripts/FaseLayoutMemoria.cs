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
                Debug.LogError("[+LUDUS] Erro ao carregar a fase, verifique as configura��es.");
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
                Debug.LogError("[+LUDUS] Vari�vel pasta n�o defindida, verifique as vari�veis do script e adicione a pasta corresponde aos sprites. Dica: procure na pasta Resources/fases e busque O NOME DA FASE DESEJADA");
                return;
            }

            try
            {
                //função Carregar Assets e feita mais para frente
                //spritesObjeto = this.CarregarAssets<Sprite>("objeto");
                //spritesSombra = this.CarregarAsset<Sprite>("sombra");
            } catch (System.Exception ex)
            {
                Debug.LogError("[+LUDUS] Sprites n�o encontrados. Verifique se a pasta fases/" + pasta + "/objeto e fases/" + pasta + "/sombra existem dentro da pasta Resources, e se os os sprites est�o corretamente divididos (multiple)");
                Debug.LogException(ex);
                return;
            }
            #region legendaObjeto
            //busca o conteudo da legenda do objeto, caso exista
            //textos = this.CarregarAssetsPastas("texto", out this.temLegendaObjeto);
            #endregion
        }
    }
}
