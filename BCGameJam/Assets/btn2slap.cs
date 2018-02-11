using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn2slap : MonoBehaviour {
    private Selection selectionScript;
    // Use this for initialization
    void Start () {
        selectionScript = GameObject.FindGameObjectWithTag("Selection").GetComponent<Selection>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
		#if UNITY_EDITOR
        selectionScript.CheckTheArea("Slap");
        GameManager.instance.SlapSound.Play();
		#endif
    }
}
