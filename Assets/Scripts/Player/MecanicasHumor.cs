using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasHumor : MonoBehaviour {

	[Range(0f,100f)]
	float humor;
	[Range(-1.0f,1.0f)]
	float humorParametro; //Determina onde na escala de humor a pessoa fica de mau humor.
	float humorRedutor;
	[Range(0f,1f)]
	float percentualRedutor; //Infleuncia do Redutor sobre o Modificador
	[Range(0f,1f)]
	float percentualModificador; //Influencia das atividades de alteracao de humor sobre o Modificador
	float humorModificador; //Numero que determina a direcao e velocidade do humor apos uma atividade.
	[Range(-3f,3f)]
	private int humorIntensidade; //Demonstra a variavel anterior

	float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AtualizarHumor();
	}
	public void ModificarHumor(float x){
		humor=humor+x;
		humorModificador=humorModificador+(x*percentualModificador);
	}
	private void AtualizarHumor(){
		timer=Time.deltaTime+timer;
		if(timer>1f){
			humor=humor-humorRedutor+humorModificador;
			humorModificador=humorModificador-(humorRedutor*percentualRedutor);
		}
	}
	private void DefinirIntensidade(){
		if(humorModificador<-2){
			humorIntensidade=-3;
		}else if(humorModificador<-1)
		{
			humorIntensidade=-2;
		}else if (humorModificador<0)
		{
			humorIntensidade=-1;
		}else if (humorModificador==0)
		{
			humorIntensidade=0;
		}else if (humorModificador>2)
		{
			humorIntensidade=3;	
		}else if (humorModificador>1)
		{
			humorIntensidade=2;
		}else if (humorModificador>0)
		{
			humorIntensidade=1;
		}
	}

}
