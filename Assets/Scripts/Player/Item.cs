using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum TipoItem { BEBIDA, COMIDA, }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool Equals(object other){
		
		if(!(other is Item)){
			return false;
		}

		return other.GetHashCode() == this.GetHashCode();
	}

	public override int GetHashCode(){
		return gameObject.name.GetHashCode();
	}
}
