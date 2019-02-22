using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Churrasqueira : Interactive {
    //usado em outros scripts (PAC)
    
    // variaveis simples

    // GameObjects
    // Outros Scripts
    public GameObject[] carnes = new GameObject[4];
    //
    static Churrasqueira instance;
    public static Churrasqueira Instance {get{return instance;}}
    //
    void Start()
    {
        instance = this;
        slots = new bool[4] {false,false,false,false};        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DesativarMenu();
        }
    }

#region AddRev
    public void Adicionar() {
        //Set.Active(true)
        //RevInventario no PlayerInventario
        GameObject tipo = PlayerInventario.RevInventario();
        if (tipo.Equals(null))
        {
            return;
        }
        else if (tipo.GetComponent<Carne>())
        {
            if (!slots[0])
            {
                OcuparChurrasqueira(0,tipo);
            }
            else if (!slots[1])
            {
                OcuparChurrasqueira(1,tipo);
                
            }
            else if (!slots[2])
            {
                OcuparChurrasqueira(2,tipo);
                
            }
            else if (!slots[3])
            {
                OcuparChurrasqueira(3,tipo);
                
            }
            else
            {
                //Full
            }
        }

        

    }
    public void Remover(int carneIndex, string status)
    {
        GameObject carneTemp;
        carneTemp=carnes[carneIndex];
        carneTemp.name = status;
        carneTemp.GetComponent<Carne>().preparado=true;
        if (PlayerInventario.AddInventario(carneTemp))
        {            
            slots[carneIndex] = false;            
            //
            carnes[carneIndex].SetActive(false);
            //
            carnes[carneIndex].GetComponent<Carne>().ResetStatus();
        }      
        else
        {
            //Empty
        }
    }

    void OcuparChurrasqueira(int index, GameObject tipo){
                slots[index] = true;
                //
                GameObject clone = Instantiate(tipo, carnes[index].transform.position,Quaternion.identity) as GameObject;
                clone.transform.parent = this.transform;
                clone.GetComponent<Carne>().carneIndex=index;
                GameObject temp = carnes[index];                
                carnes[index] = clone;
                Destroy(temp);
    }
#endregion
}
