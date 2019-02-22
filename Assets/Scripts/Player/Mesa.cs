using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesa : Interactive {
	public GameObject carneMesa;
	//
	private bool temCarneNaMesa;
	//
	public float carneTimer;
	//
	public int fatorSub = 0 ;
	//
	public int tempoNaMesa = 15;
	//
	public GameObject mesaTrigger;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timerCarne();
	}

    public override void VerificacaoMenu()
    {
        Debug.Log("Veri");
        if (menuPodeAtivar && !temCarneNaMesa)
        {
            Debug.Log("Entrou");
            GameObject tipo = PlayerInventario.RevInventario();
			
			if(tipo.Equals(null)){
				return;
			}
			else if(tipo.GetComponent<Carne>()){
				GameObject clone = Instantiate(tipo, carneMesa.transform.position,Quaternion.identity) as GameObject;
				clone.transform.parent = this.transform;
                GameObject temp = carneMesa;                
                carneMesa = clone;
				carneMesa.SetActive(true);
				Destroy(temp);
			}
            temCarneNaMesa = true;
        }       
    }

    public void timerCarne(){
		if (temCarneNaMesa)
		{
			carneTimer+=Time.deltaTime;
			if (carneTimer>(tempoNaMesa - fatorSub))
			{
				RevCarne();
			}
		}
	}
	private void RevCarne(){
		carneMesa.SetActive(false);
		temCarneNaMesa = false;
		carneTimer = 0;
	}




	
}
