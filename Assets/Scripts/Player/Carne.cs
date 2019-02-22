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
    public bool preparado;
    private bool cozinhar;
    int porcoes;
    private Renderer rend;


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
                break;
            case 1:
                status="Descongelada";
                break;
            case 2:
                status="Aquecida";
                break;
            case 3:
                status="Mal Passada";
                break;
            case 4:
                status="Mal ao Ponto";
                break;
            case 5:
                status="Ao Ponto";
                break;
            case 6:
                status="Bem ao Ponto";
                break;
            case 7:
                status="Bem Passada";
                break;
            case 8:
                status="Torrada";
                break;
            case 9:
                status="Queimada";
                break;
            case 10:
                status="Carvão";
                break;
        }
    }

}
