using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		Destroy (other.gameObject);
		var handScript = other.GetComponent<SelectionHands> ();
		if (handScript.minusHeart)
			GameManager.instance.MinusLife ();
		GameManager.instance.OnDestroyRespawnHands ();
	}
}
