using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMng : Photon.MonoBehaviour {

    public string verNum = "0.0.1";
    public string roomName = "TestRoom";
    public Transform spawnPts;
    public GameObject playerPref;
    public bool isConnected = false;
    public RoomOptions roomOps;
    public TypedLobby typeLobby;


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(verNum);
        Debug.Log("Start connecting...");
    }

    public void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);

    }

    public void OnJoinedRoom()
    {
        isConnected = true;
        SpawnPlayer();
        Debug.Log("Connected !");
    }

    public void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPref.name, spawnPts.position, spawnPts.rotation, 0) as GameObject;
        Debug.Log(playerPref.name);
        CharacterController controller = player.GetComponent<CharacterController>();
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPerson = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        firstPerson.enabled = true;
        Camera cam = player.GetComponentInChildren<Camera>();
        controller.enabled = true;
        cam.enabled = true;
    }
}
