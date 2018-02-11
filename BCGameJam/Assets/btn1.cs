using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn1 : MonoBehaviour {
    private Selection selectionScript;
    // Use this for initialization
    void Start () {
        selectionScript = GameObject.FindGameObjectWithTag("Selection").GetComponent<Selection>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
	{   #if UNITY_EDITOR
        selectionScript.CheckTheArea("Shake");
        GameManager.instance.shakehandSound.Play();
		#endif
    }
}
