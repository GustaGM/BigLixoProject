using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventario : MonoBehaviour {
    public static bool full;
    private static GameObject descItem ;

    

    void Start () {
		
	}
	
	void Update () {
		
	}

    public static bool AddInventario(GameObject item) {
        //Dúvida
        if (!full)
        {
            descItem = item;
            full = true;
            Debug.Log("Add: " + descItem.name + "  full?: " + full);
            return true;
        }
        return false;
        
        
    }
    public static GameObject RevInventario()
    {
        if (full)
        {
            GameObject temp = descItem;
            descItem = null;
            full = false;
            Debug.Log("Rev: " + temp.name + "  full?: " + full);
            return temp;
        }
        else
        {
            return descItem;
        }
        
    }


}
