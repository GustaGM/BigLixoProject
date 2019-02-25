using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tipoBebida {CERVEJA,VODKA,WHISKY,VINHO,CACHACA,TEQUILA,SAKE,RUM}
public class Bebida : Item {

	public float quantidadeAlcool;
	public float multiplicadorNormal;
	public float multiplicadorVirada;
	public float multiplicadorMistura;
	public float qualidadeBebida;
	public float humorFlat;
	public int tamanhoBebida;
	[SerializeField]
	public tipoBebida tipo;
	public string nome;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
