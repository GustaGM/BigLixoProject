using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Text txtBebisse;

	public Text humorConvidado, nomeConvidado, bebisseConvidado;
	public GameObject panelConvidados;


	static GUIManager instance;
    public static GUIManager Instance { get { return instance;} }


	
	void Awake(){
		if(instance == null){
			instance = this;
		}
	}

	// Update is called once per frame
	void Update () {
		txtBebisse.text = "" + PAC.Instance.bebisseReference;
	}

	public void mostraConvidado(Guest convidado){
		panelConvidados.SetActive(true);
		//texto = convidado.valor.toString();
	}
	public void escondeConvidado(){
		panelConvidados.SetActive(false);
		
	}


}
