using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public GameObject player;

	void Start ()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            player = PlayerController.singleton.gameObject;
            if (player == null)
            {
                Instantiate(player, transform);
            }
        }
	}
	
	
}
