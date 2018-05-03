using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            //stream.SendNext(anim.GetFloat("Velocity"));
        }
        else
        {
            //Network player, receive data
            //anim.SetFloat("Velocity", (float)stream.ReceiveNext());
        }
    }
}
