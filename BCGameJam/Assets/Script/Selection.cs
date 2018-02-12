using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {
	public LayerMask layerMask;
	private BoxCollider2D boxCollider;
	private PlayerHand playerHandScript;
    [SerializeField]
    private GameObject Check;
    [SerializeField]
    private GameObject Cross;
	// Use this for initialization
	void Start () {
		boxCollider = gameObject.GetComponent<BoxCollider2D> ();
		playerHandScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHand> ();
        Check.SetActive(false);
        Cross.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	}
    void ShowCheck()
    {
        Check.SetActive(true);
        Invoke("CheckDisappearInDelay", GameManager.instance.delayToDisableUI);
        GameManager.instance.correctSound.Play();
    }
    void ShowCross()
    {
        Cross.SetActive(true);
        Invoke("CrossDisappearInDelay", GameManager.instance.delayToDisableUI);
        GameManager.instance.wrongSound.Play();
    }

    void CheckDisappearInDelay()
    {
        Check.SetActive(false);
    }
    void CrossDisappearInDelay()
    {
        Cross.SetActive(false);
    }


    public void CheckTheArea(string action){
		if (action == "Slap") {
			playerHandScript.Slap ();
			// slap the in coming hands 
			Collider2D[] colliders = Physics2D.OverlapBoxAll (transform.position,boxCollider.size,0f,layerMask);
			if (colliders.Length > 1) {
				foreach (var item in colliders) {
					if (item.gameObject != gameObject) {
						SelectionHands selectedHands = item.GetComponent<SelectionHands> ();
						if (selectedHands.handType == HandsType.Good) {
                            ShowCross();
							GameManager.instance.MinusLife ();
							selectedHands.minusHeart = false;
                        } else {
							GameManager.instance.AddScore ();
							GameManager.instance.AddLife ();
							selectedHands.HandDisappear ();
							GameManager.instance.OnDestroyRespawnHands ();
                            ShowCheck();
                        }
					}
				}
			} else {
                ShowCross();
				GameManager.instance.MinusLife ();
			}
		} else {
			playerHandScript.ShakeHand ();
			//action =="shake"
			// slap the in coming hands 
			Collider2D[] colliders = Physics2D.OverlapBoxAll (transform.position,boxCollider.size,0f,layerMask);
			if (colliders.Length > 1) {
				foreach (var item in colliders) {
					if (item.gameObject != gameObject) {
						SelectionHands selectedHands = item.GetComponent<SelectionHands> ();
						if (selectedHands.handType == HandsType.Good) {
							// the result is Good
							GameManager.instance.AddScore ();
							GameManager.instance.AddLife ();
							selectedHands.HandDisappear ();
							GameManager.instance.OnDestroyRespawnHands ();
                            ShowCheck();
                        } else {
                            ShowCross();
							selectedHands.minusHeart = false;
							GameManager.instance.MinusLife ();
                        }
					}
				}
			} else {
                ShowCross();
				GameManager.instance.MinusLife ();
            }
		}

	}
}
