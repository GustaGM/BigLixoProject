using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public GameObject[] canecas = new GameObject[5];
    public GameObject[] canecasGuests = new GameObject[5];
    public Image humorBar;
    public float humorValue = 5f;
    bool convidadoIsActive = false;

    public Text humorConvidado, nomeConvidado, bebisseConvidado;
	public GameObject panelConvidados;

    int auxCaneca = 1;

	static GUIManager instance;
    public static GUIManager Instance { get { return instance;} }

    //
   public MecanicasAlcool playerMecAlc;
   public MecanicasHumor playerMecHumor;
    //
	
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
        BebisseGUI();
        HumorGUI();
        
    }

    public void ConvidadoGUI(){
        if (convidadoIsActive)
        {
            canecasGuests[0].SetActive(true);
            canecasGuests[0].SetActive(true);
        }
    }
    public void BebisseGUI(MecanicasAlcool mecAlc)
    {
        canecas[mecAlc.bebisseStage-1].SetActive(true);
        calculoFill(mecAlc);
        if (auxCaneca>mecAlc.bebisseStage)
        {
            canecas[auxCaneca-1].SetActive(false);
        }
        auxCaneca = mecAlc.bebisseStage;        
    }

    public void calculoFill(MecanicasAlcool mecAlc){
        switch (mecAlc.bebisseStage)
        {
            case 1:
                canecas[mecAlc.bebisseStage-1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc,0,10);
                break;
            case 2:
                canecas[mecAlc.bebisseStage-1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc,10,30);
                break;
            case 3:
                canecas[mecAlc.bebisseStage-1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc,30,70);
                break;
            case 4:
                canecas[mecAlc.bebisseStage-1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc,70,95);
                break;
            case 5:
                canecas[mecAlc.bebisseStage-1].GetComponent<Image>().fillAmount = fillCanecas(mecAlc,95,100);
                break;
            
        }
    }

    public float fillCanecas(MecanicasAlcool mecAlc,float numInicial, float numFianl){
        return (mecAlc.bebisse-numInicial)/(numFianl-numInicial);
    }

    public void HumorGUI()
    {
        humorValue = playerMecHumor.getHumor();
        
        humorBar.fillAmount = (humorValue / 100);
        if (humorValue >= 0 && humorValue < 50)
        {
            humorBar.color = Color.red;
        }
        else if (humorValue >= 30 && humorValue < 75)
        {
            humorBar.color = Color.Lerp(Color.red,Color.yellow, 2f);
        }
        else if (humorValue >= 75)
        {
            humorBar.color = Color.Lerp(Color.yellow,Color.green,2f);
            
        }
	}
	public void mostraConvidado(Guest convidado){
		panelConvidados.SetActive(true);
        convidadoIsActive = true;
		//texto = convidado.valor.toString();
	}
	public void escondeConvidado(){
		panelConvidados.SetActive(false);
        convidadoIsActive = false;
		
	}


}
