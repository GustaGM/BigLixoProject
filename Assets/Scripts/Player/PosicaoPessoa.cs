using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicaoPessoa : MonoBehaviour {

	public bool livre = true;
	public List<PosicaoPessoa> PosicoesConversa = new List<PosicaoPessoa>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Vector3 PegarPosicao(){
		return(transform.position);
	}
	public PosicaoPessoa PegaPosicaoConversa(){
		if(livre){
			return(this);
		}
		for(int i=0;i<PosicoesConversa.Count;i++){
			if(PosicoesConversa[i].livre){
				return(PosicoesConversa[i]);
			}
		}
		return(null);
	}
}
