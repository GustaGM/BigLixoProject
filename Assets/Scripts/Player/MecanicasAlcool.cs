using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasAlcool : MonoBehaviour {

	private Renderer rend;
	MecanicasHumor MecHum;
	public bool bebendo=false;
    //
    private float timer=0f;
    private int contSec=0;
    //
    private GameObject itemAtual;
	[Range(0f,100f)]
    public float bebisse;
    public int bebisseStage;
	string descricaoStage;
    float resistenciaAlcool=0f;
    float regeneraçãoAlcool=0.3f;
    int tempoBebida=7;
    public int multiplicadorBebida=0;
    private float alcoolFinal;
    float tempoSemBebida=4f;
    private float tempoRepeticaoBebida;
    private Queue BebidasRecentes = new Queue();
    private int ContadorDeMistura;

	#region Variaveis IA
	[Range(0f,1f)]
	public float dependenciaAlcool;
	public float modificadorHumorBebida;
	public float modificadorHumorSemBebida;
	public float modificadorBebidaEstomagoVazio=1;


//

	#endregion


	// Use this for initialization
	void Start () {
//
		rend = GetComponent<Renderer>();
		MecHum=GetComponent<MecanicasHumor>();
        tempoRepeticaoBebida=(tempoBebida+regeneraçãoAlcool)/2;
	}
	
	// Update is called once per frame
	void Update () {
		Bebendo();
		SetAlcoolStage();
	}

	public void CalculoAlcool(GameObject bebida){
        Bebida bbd = bebida.GetComponent<Bebida>();
        GerenciarFilaBebida(bbd);
        DefinirMultiplicadoresVelocidade();
        DefinirMultiplicadoresMistura(bbd);
        alcoolFinal=bbd.quantidadeAlcool-(bbd.quantidadeAlcool*resistenciaAlcool);
        alcoolFinal=alcoolFinal*(Mathf.Pow(bbd.multiplicadorNormal,multiplicadorBebida)+(bbd.multiplicadorMistura*ContadorDeMistura));
		//alcoolFinal=alcoolFinal*modificadorBebidaEstomagoVazio;
		//alcoolFinal=alcoolFinal*GameManager.Instance.multiplicadorMestreBebida;
        Debug.Log(alcoolFinal);
        ComeçarBeber();
    }
	public void ComeçarBeber()
    {
        if(!bebendo){
            rend.material.color = Color.yellow;
            bebendo = true;
        }
    }

	public void Bebendo() {
        if (bebendo)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                bebisse=bebisse+(alcoolFinal/tempoBebida);
                timer = 0;
                //Debug.Log(bebisse);
                contSec++;
            }
            if(contSec==tempoBebida){
                EncerrarBebida();
            }
        }else{
			AlcoolDegen();
        }
    }
	void EncerrarBebida(){
        rend.material.color=Color.white;
        bebendo=false;
        tempoSemBebida=0;
        contSec=0;
    }
	void AlcoolDegen(){
		    tempoSemBebida+=Time.deltaTime;
           if(tempoSemBebida>10f){
                bebisse-=regeneraçãoAlcool/60;
                if(bebisse<0){
                    bebisse=0;
                }
           }
	}
	void DefinirMultiplicadoresVelocidade(){
        if(tempoSemBebida<=tempoRepeticaoBebida){
            multiplicadorBebida++;
        }else{
            multiplicadorBebida=0;
        }
    }
    void DefinirMultiplicadoresMistura(Bebida bbd){
        int x = BebidasRecentes.Count;
        HashSet<tipoBebida> conjunto = new HashSet<tipoBebida>();
        ContadorDeMistura=-1;
        for(int i=0;i<x;i++){
            tipoBebida pop = (tipoBebida)BebidasRecentes.Dequeue();
            if(conjunto.Add(pop)){
               ContadorDeMistura++;
               Debug.Log(ContadorDeMistura);
           }
           BebidasRecentes.Enqueue(pop); 
        }
    }
    public void VirarBebida(){
        if(bebendo){
            float x;
            x=tempoBebida-contSec;
            x=(alcoolFinal/tempoBebida)*x;
            bebisse=bebisse+(x*2f);
            EncerrarBebida();
        }
    }
    void GerenciarFilaBebida(Bebida bbd){
        int a = BebidasRecentes.Count;
        if(a>=10){
            BebidasRecentes.Dequeue();
        }
        BebidasRecentes.Enqueue(bbd.tipo);
    }
	void SetAlcoolStage(){
		if(bebisse<=10){
			bebisseStage=1;
			descricaoStage="Sobrio";
		}else if (bebisse<=30)
		{
			bebisseStage=2;
			descricaoStage="Animado";
		}else if (bebisse<=70)
		{
			bebisseStage=3;
			descricaoStage="Consciente";
		}else if (bebisse<=95)
		{
			bebisseStage=4;
			descricaoStage="Bebasso";
		}else if (bebisse<=99)
		{
			bebisseStage=5;
			descricaoStage="Alucinando";
		}else
		{
			bebisseStage=6;
			descricaoStage="DEU PT";
		}
	}
}
