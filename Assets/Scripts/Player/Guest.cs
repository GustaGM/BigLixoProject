using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum tipoDecisao{IRPARAMESA,IRPARAGELADEIRA}
public class Guest : MonoBehaviour {
	private MecanicasAlcool MecAlc;
	private MecanicasHumor MecHum;
	private MecanicasFome MecFom;
	// Use this for initialization
	public GameObject[] bebidas;
	public GameObject[] carne;
	NavMeshAgent agent;
	float timer=0f;
	bool timerOn=false;
	float decisionTime;
	public Transform PosicoesPai;

	#region Variaveis de Comportamento
	public int grupoSocial;
	[Range(-1f,1f)]
	public float personalidade; //-1 Agressivo 1 Educado
	[Range(-1f,1f)]
	public float sociabilidade; //-1 Antissocial 1 Super Sociavel
	[Range (-1f,1f)] 
	public float aventureiroSocial; //-1 Sempre na Panelinha 1 Gosta de Conhecer Pessoas Novas
	[Range(-1f,1f)]
	public float estilo; //-1 Topa Tudo 1 Fresco
	public float modificadorHumorAmbiente; //O quanto os problemas do ambiente influenciam o humor
	#endregion  
	void Start () {
        Geladeira.Instance.AdicionarItem(carne[1].GetComponent<Carne>());
		Geladeira.Instance.AdicionarItem(bebidas[Random.Range(0,300)%bebidas.Length].GetComponent<Bebida>());
		agent = GetComponent<NavMeshAgent>();
		MecAlc=GetComponent<MecanicasAlcool>();
		MecHum=GetComponent<MecanicasHumor>();
		MecFom=GetComponent<MecanicasFome>();
		PosicoesPai = GameObject.Find("PosicoesPai").transform;
		decisionTime = Random.Range(4f,7f);
		//Decisions(tipoDecisao.IRPARAGELADEIRA);



		//if (timerOn)
		//{
		//	timer+=Time.deltaTime;
		//}
    }
	private void OnMouseOver(){
		GUIManager.Instance.mostraConvidado(this);
	}
	private void OnMouseExit(){
		GUIManager.Instance.escondeConvidado();
	}

	/*
	void Decisions(tipoDecisao decisao){
		switch(decisao){
			case tipoDecisao.IRPARAGELADEIRA:
				agent.SetDestination(Geladeira.Instance.localDeAcessoPlayer.transform.position);
				timerOn = true;
				if (timer>5)
				{
					Decisions(tipoDecisao.IRPARAMESA);
					timerOn = false;
					timer = 0;
				}				
			break;
			case tipoDecisao.IRPARAMESA:
				agent.SetDestination(new Vector3(Random.Range(9,40),1,Random.Range(-10,5)));
			break;
		}

	}
	*/
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer>decisionTime){
			ShiftAction();
		}
	}

	void ShiftAction(){
		timer=0f;
		decisionTime = Random.Range(4f,7f);
        if (Random.Range(0, 100) < 101)
        {
            agent.SetDestination(SorteiaPosition());
        }
        
    }
	Vector3 SorteiaPosition(){
		Vector3 novaPos = this.transform.position;
		for(int i = 0; i < PosicoesPai.childCount; i++){
			Transform child = PosicoesPai.GetChild(i);
			PosicaoPessoa pp = child.GetComponent<PosicaoPessoa>();
			PosicaoPessoa posicaoConversa = pp.PegaPosicaoConversa();
			if(posicaoConversa != null){
				novaPos = posicaoConversa.PegarPosicao();
				posicaoConversa.livre = false;
                if (posicaoConversa.CompareTag("geladeira"))
                {
                    int rand = Random.Range(0,300)%bebidas.Length;
                    Geladeira.Instance.RemoverItem(bebidas[rand].GetComponent<Bebida>());
                    Debug.Log("retirou: " + bebidas[rand].name);
                }
				break;
			}
		}
		return novaPos;
	}

    #region GETSET

    public MecanicasAlcool GetMecanicasAlcool() {
        return MecAlc;
    }
    public MecanicasFome GetMecanicasFome(){
        return MecFom;
    }
    public MecanicasHumor GetMecanicasHumor(){
        return MecHum;
    }

    #endregion

}
