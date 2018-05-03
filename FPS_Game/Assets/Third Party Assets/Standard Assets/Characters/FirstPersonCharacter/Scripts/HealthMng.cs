using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthMng : MonoBehaviour {

    public int health = 100;
    [SerializeField] private GameObject me;
    private PhotonView photonView;

	// Use this for initialization
	void Start () {
        photonView = GetComponent<PhotonView>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if(photonView.isMine)
        {
            GUI.Box(new Rect(10, 10, 100, 30), "HP | " + health);
        }
    }

    [PunRPC]
    public void ApplyDamage(int dmg)
    {
        health = health - dmg;
        if (health <= 0)
        {
            photonView.RPC("Die", PhotonTargets.AllBuffered);
        } 
    }

    [PunRPC]
    private void Die()
    {
        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(me);
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("Main");
        }

    }

    
}
