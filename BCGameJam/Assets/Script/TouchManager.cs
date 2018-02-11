using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {
	[SerializeField]
	private float touchThreshhold = 1f;
	private Selection selectionScript;
	private PlayerHand playerHandScript;
	// Use this for initialization
	void Start () {
		selectionScript = GameObject.FindGameObjectWithTag ("Selection").GetComponent<Selection> ();
	}

	// Update is called once per frame
	void Update () {
        //Check Button
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;

                if(Physics.Raycast(ray,out hit))
                {
                    if(hit.collider.name == "Btn_HandShake")
                    {
                      
                        selectionScript.CheckTheArea("Shake");
                        GameManager.instance.shakehandSound.Play();
                    }
                    else if (hit.collider.name == "Btn_Slap")
                    {
                       
                        selectionScript.CheckTheArea("Slap");
                        GameManager.instance.SlapSound.Play();
                    }
                }

            }
        }
	}


	//void DetectSwipeAndTouch()
	//{
	//	Vector2 firstPressPos = Vector2.zero ;
	//	Vector2 secondPressPos;
	//	Vector2 currentSwipe;
	//	if(Input.touches.Length > 0)
	//	{
	//		Touch t = Input.GetTouch(0);
	//		if(t.phase == TouchPhase.Ended)
	//		{
	//			currentSwipe = t.deltaPosition;
	//			if(currentSwipe.magnitude > touchThreshhold){
	//				selectionScript.CheckTheArea ("Slap");
	//			}else{;
	//				selectionScript.CheckTheArea ("Shake");
	//			}
	
	//		}
	//	}
	//}
}
