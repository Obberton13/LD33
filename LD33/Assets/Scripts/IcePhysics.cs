using UnityEngine;
using System.Collections;

public class IcePhysics : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		PlayerController rb2d;
		if((rb2d=other.gameObject.GetComponent<PlayerController>())!=null)
		{
			rb2d.incrementEntered();
		}
		HumanAI human = other.gameObject.GetComponent<HumanAI>();
		if(human!=null)
		{
			human.incrementEntered();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		PlayerController rb2d;
		if((rb2d=other.gameObject.GetComponent<PlayerController>())!=null)
		{
			rb2d.decrementEntered();
		}
		HumanAI human = other.gameObject.GetComponent<HumanAI>();
		if(human!= null)
		{
			human.decrementEntered();
		}
	}
}
