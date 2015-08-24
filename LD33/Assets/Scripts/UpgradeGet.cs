using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class UpgradeGet : MonoBehaviour {
	[SerializeField]
	private uint _attackType = 0;

	private PlayerController _player;
	void Awake()
	{
		_player = FindObjectOfType<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>()==_player)
		{
			//this does an | instead of an =, but then sets the selected weapon, so it actually works fine.
			_player.AvailableAttacks = _attackType;
			Destroy(gameObject);
		}
	}
}
