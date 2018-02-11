using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHands : MonoBehaviour {
	public HandsType handType;
	public BadHandStyle badHandStyle;
	public GoodHandStyle goodHandStyle;
	public Sprite[] sprites;
	public LayerMask layerMask;
	private SpriteRenderer spriteRneder;
	private Animator anim;

	// Use this for initialization
	void Start () {

		spriteRneder = GetComponent<SpriteRenderer> ();
		anim = gameObject.GetComponent<Animator> ();
		CheckCurrentLocation ();
		if (sprites.Length > 0) {
			switch (badHandStyle) {
			case BadHandStyle.Badhand1:
				spriteRneder.sprite = sprites [2];
				break;
			case BadHandStyle.BadHand2:
				spriteRneder.sprite = sprites [3];
				break;
			case BadHandStyle.BadHand3:
				spriteRneder.sprite = sprites [4];
				break;
			case BadHandStyle.BadHand4:
				spriteRneder.sprite = sprites [5];
				break;
			case BadHandStyle.BadHand5:
				spriteRneder.sprite = sprites [6];
				break;
			case BadHandStyle.BadHand6:
				spriteRneder.sprite = sprites [6];
				break;
			default:
				break;
			}

			switch (goodHandStyle) {
			case GoodHandStyle.GoodHand1:
				spriteRneder.sprite = sprites [0];
				break;

			case GoodHandStyle.GoodHand2:
				spriteRneder.sprite = sprites [1];
				break;
			default:
				break;
			}
		}
	}
	void update(){
		CheckCurrentLocation ();
	}

	public void HandDisappear(){
		anim.SetTrigger ("FadeOut");
		Invoke ("Died", 0.9f);
	}

	void Died(){
		Destroy (gameObject);
	}
	void CheckCurrentLocation(){
		Collider2D[] colliders = Physics2D.OverlapBoxAll (transform.position, GameManager.instance.gapSize,0f,layerMask);
		if (colliders.Length > 0) {
			foreach (var item in colliders) {
				if (item.gameObject != gameObject && item.name.StartsWith("Hand")){
					Destroy (item.gameObject);
				}
			}
		}
	}
}
