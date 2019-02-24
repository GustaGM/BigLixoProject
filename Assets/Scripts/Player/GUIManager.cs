using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    [Header("Variaveis")]
    public float humorValue = 5f;
    int auxCaneca = 1;
    bool convidadoIsActive = false;
    [Header("Textos")]
    public Text humorConvidado;
    public Text nomeConvidado;
    public Text bebisseConvidado;
    [Header("Sprites")]
    public Sprite happyIcon;    
    public Sprite veryHappyIcon;
    public Sprite angryIcon;
    public Sprite veryAngryIcon;
    public Sprite normalIcon;
    [Header("Imagens")]
    //  Player
    public Image playerHumorIcon;
    public Image playerHumorBar;
    //  Guest
    public Image guestHumorIcon;
    public Image guestHumorBar;
    //  Geral
    [Header("Mecanicas")]
    //   Player
    public MecanicasAlcool playerMecAlc;
    public MecanicasHumor playerMecHumor;
    //    Guest    
    private MecanicasAlcool guestMecAlc;
    private MecanicasHumor guestMecHumor;
    private MecanicasFome guestMecFome;
    [Header("GameObjects")]
    //  Player
	public GameObject[] canecas = new GameObject[5];
    //  Guests
    public GameObject panelConvidados;
    public GameObject[] canecasGuests = new GameObject[5];    
    //
    static GUIManager instance;
    public static GUIManager Instance { get { return instance; } }

    void Awake(){
		if(instance == null){
			instance = this;
		}
	}
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        PlayerGUI();
        ConvidadoGUI();
        
    }
    #region MainGUI
    public void PlayerGUI() {
        BebisseGUI(playerMecAlc);
        HumorGUI(playerHumorBar,playerMecHumor);
    }

    public void ConvidadoGUI(){
       if (convidadoIsActive)
        {
            BebisseGUI(guestMecAlc);
            HumorGUI(guestHumorBar,guestMecHumor);
            //colocar gui fome e gui humor
        }
    }
    #endregion

    #region Geral
    public void ChangeHumorIcon(Image icon, Sprite humorState){// icon do guest ou do player//chamar quando houver mudança de estado
        icon.sprite = humorState;
    }
    public void BebisseGUI(MecanicasAlcool mecAlc){
        canecas[mecAlc.bebisseStage - 1].SetActive(true);
        calculoFill(mecAlc);
        if (auxCaneca > mecAlc.bebisseStage)
        {
            canecas[auxCaneca - 1].SetActive(false);
        }
        auxCaneca = mecAlc.bebisseStage;
    }

    public void calculoFill(MecanicasAlcool mecAlc){
        switch (mecAlc.bebisseStage)
        {
            case 1:
                canecas[mecAlc.bebisseStage - 1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc, 0, 10);
                break;
            case 2:
                canecas[mecAlc.bebisseStage - 1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc, 10, 30);
                break;
            case 3:
                canecas[mecAlc.bebisseStage - 1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc, 30, 70);
                break;
            case 4:
                canecas[mecAlc.bebisseStage - 1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc, 70, 95);
                break;
            case 5:
                canecas[mecAlc.bebisseStage - 1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc, 95, 100);
                break;

        }
    }

    public float fillCanecas(MecanicasAlcool mecAlc, float numInicial, float numFianl){
        return (mecAlc.bebisse - numInicial) / (numFianl - numInicial);
    }

    public void HumorGUI(Image humorBar, MecanicasHumor mecHumor){
        humorValue = mecHumor.getHumor();

        humorBar.fillAmount = (humorValue / 100);
        if (humorValue >= 0 && humorValue < 50)
        {
            humorBar.color = Color.red;
        }
        else if (humorValue >= 30 && humorValue < 75)
        {
            humorBar.color = Color.Lerp(Color.red, Color.yellow, 2f);
        }
        else if (humorValue >= 75)
        {
            humorBar.color = Color.Lerp(Color.yellow, Color.green, 2f);

        }
    }
    #endregion

    #region Guest

    public void ChangeGuiGuestName(string nome) {// ou mudar para Text nome e chamar nome.text
        nomeConvidado.text = nome;
    }    
	public void mostraConvidado(Guest convidado){
		panelConvidados.SetActive(true);
        convidadoIsActive = true;
        guestMecAlc = convidado.GetMecanicasAlcool();
        guestMecHumor = convidado.GetMecanicasHumor();
		//texto = convidado.valor.toString();
	}
	public void escondeConvidado(){
		panelConvidados.SetActive(false);
        convidadoIsActive = false;
		
	}
    #endregion

}
