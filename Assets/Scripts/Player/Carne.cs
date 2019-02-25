using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carne : Item {
    public int carneIndex;
    private string status;
    private float carneTimer;
    private int carneEstado=0;
    private float limiarEstado;
    public int tempoPreparo=30;
    public float humorBase;
    public float modificadorSabor;
    public bool preparado;
    private bool cozinhar;
    int porcoes;
    private Renderer rend;

    public bool estaNaMesa = false;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        limiarEstado=tempoPreparo/10;
        ResetStatus();
    }
	
	// Update is called once per frame
	void Update () {
        CarneCozinhando();
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && Churrasqueira.Instance.Menu.activeSelf)
        {
            CarneStatus();
            Churrasqueira.Instance.Remover(carneIndex, status);
            cozinhar = false;

        }
        else if (Input.GetMouseButtonDown(0) && estaNaMesa)// linha 43 Mesa
        {
            DeduzirPorcao();
            PAC.Instance.mecFome.Comer(humorBase*modificadorSabor);

        }
    }

    public void CarneCozinhando() {
        if (cozinhar)
        {
            carneTimer += Time.deltaTime;
            if (carneTimer > limiarEstado)
            {
                carneEstado++;
                limiarEstado=limiarEstado+(tempoPreparo/10);
                if (carneEstado==10){
                    cozinhar=false;
                }
            }
        }
    }
    public void ResetStatus()
    {
        cozinhar = true;
        carneTimer = 0f;
        status = "Crua";
    }
    public void CarneStatus(){
        switch(carneEstado){
            case 0:
                status="Crua";
                modificadorSabor=-3;
                break;
            case 1:
                status="Descongelada";
                modificadorSabor=-1.5f;
                break;
            case 2:
                status="Aquecida";
                modificadorSabor=-1;
                break;
            case 3:
                status="Mal Passada";
                modificadorSabor=2;
                break;
            case 4:
                status="Mal ao Ponto";
                modificadorSabor=5;
                break;
            case 5:
                status="Ao Ponto";
                modificadorSabor=3;
                break;
            case 6:
                status="Bem ao Ponto";
                modificadorSabor=2;
                break;
            case 7:
                status="Bem Passada";
                modificadorSabor=1;
                break;
            case 8:
                status="Torrada";
                modificadorSabor=0.5f;
                break;
            case 9:
                status="Queimada";
                modificadorSabor=-3;
                break;
            case 10:
                status="Carvão";
                modificadorSabor=-10;
                break;
        }
    }
    void DeduzirPorcao(){
        porcoes--;
        if(porcoes==0){
            Mesa.Instance.DesligarCarne();
        }
    }

}
