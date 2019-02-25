using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasHumor : MonoBehaviour {

	[SerializeField]
	[Range(0f,100f)]
	private float humor = 0;
	public float Humor{get{return humor;} set{humor = Mathf.Clamp(value,0,100);}}
	[Range(-1.0f,1.0f)]
	float humorParametro; //Determina onde na escala de humor a pessoa fica de mau humor.
	public float humorRedutor; //Reducao fixa de humor por segundo
	[Range(0f,1f)]
	public float percentualRedutor; //Infleuncia do Redutor sobre o Modificador
	[Range(0f,1f)]
	public float percentualModificador; //Influencia das atividades de alteracao de humor sobre o Modificador
	float humorModificador; //Numero que determina a direcao e velocidade do humor apos uma atividade, aplicado todo segundo.
	[Range(-3f,3f)]
	private int humorIntensidade; //Demonstra a variavel anterior
	public int HumorIntensidade{get{return humorIntensidade;} set{humorIntensidade = Mathf.Clamp(value,-3,3);}}
	int aux=0;
	public bool trocou=true;


	float timer=0;
	// Use this for initialization
	void Start () {
		//humorRedutor=humorRedutor*GameManager.Instance.multiplicadorMestreHumor;
	}
	
	// Update is called once per frame
	void Update () {
		AtualizarHumor();
	}
	public void ModificarHumor(float x){
		//x=x*GameManager.Instance.multiplicadorMestreHumor;
		Humor=Humor+x;
		humorModificador=humorModificador+(x*percentualModificador);
	}
	private void AtualizarHumor(){
		timer += Time.deltaTime;
		if(timer>1f){
			Humor=humor-humorRedutor+humorModificador;
			humorModificador=humorModificador-(humorRedutor*percentualRedutor);
			DefinirIntensidade();

			timer=0;
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
		ChecaMudancaIntesidade();
		aux=humorIntensidade;
	}
	private void ChecaMudancaIntesidade(){
		if(humorIntensidade!=aux){
			trocou=true;
		}else{
			trocou=false;
		}
	}


}
