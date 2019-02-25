using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasFome : MonoBehaviour {

	public int estomagoTamanho;
	int estomagoOcupado;
	public int digestao; //Quantos segundos demora para desocupar uma unidade de estomago.
	float digestaoContador;
	float timer;
	public float limitadorPlayer; //1 para todos os convidados, 0.25 para o player.

	#region Variaveis IA
	[Range(0,1)]
	public float modificadorHumorFome;
	[Range(0,1)]
	public float iminenciaFome; //Porcentagem do tamanho do estomago que quando vazio determina fome.
	bool fome; // player nunca tem fome.
	#endregion

	// Use this for initialization
	void Start () {
		iminenciaFome=iminenciaFome*GameManager.Instance.multiplicadorMestreFome;
		digestao=digestao/Mathf.CeilToInt(GameManager.Instance.multiplicadorMestreFome);
	}
	
	// Update is called once per frame
	void Update () {
		Digestao();
		FomeAlteraHumor();
	}
	public void Comer(float carneHumor){
		estomagoOcupado++;
		GetComponent<MecanicasHumor>().ModificarHumor(carneHumor*limitadorPlayer);
		if(fome){
			GetComponent<MecanicasHumor>().ModificarHumor(carneHumor*(modificadorHumorFome+1));
		}

	}
	void SentirFome(){
		if(estomagoOcupado/estomagoTamanho<iminenciaFome){
			fome=true;
		}else{
			fome=false;
		}
		
	}
	void CalculoFomeHumor(){
		float x,y,z;
		x=estomagoOcupado/estomagoTamanho;
		y=1-(x/iminenciaFome);
		z=y*modificadorHumorFome*(GetComponent<MecanicasHumor>().humorRedutor*1.5f);
		GetComponent<MecanicasHumor>().ModificarHumor(z);
	}
	void FomeAlteraHumor(){
		if(fome){
            timer += Time.deltaTime;
            if (digestaoContador > 1f)
            {
				CalculoFomeHumor();
			}
		}
	}
	void Digestao(){
		if(estomagoOcupado>=0){
            digestaoContador += Time.deltaTime;
            if (digestaoContador > digestao)
            {
                estomagoOcupado--;
				SentirFome();
                digestaoContador = 0;
            }
		}
	}
}
