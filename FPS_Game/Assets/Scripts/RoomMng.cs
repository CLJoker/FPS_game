using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityStandardAssets.Characters.FirstPerson;

using ExitGames.Client.Photon;

public class RoomMng : PunBehaviour
{
    public Transform spawnPts;
    public GameObject playerPref;
    public bool isConnected = false;
    public RoomOptions roomOps;
    public TypedLobby typeLobby;
    [HideInInspector]
    public PhotonAnimatorView m_AnimatorView;

    #region Photon

    public override void OnJoinedRoom()
    {
        CreatePlayerObject();

    }
   
    private void CreatePlayerObject()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPref.name, spawnPts.position, spawnPts.rotation, 0) as GameObject;
        m_AnimatorView = player.GetComponentInChildren<PhotonAnimatorView>();

        CharacterController characterController = player.GetComponent<CharacterController>();
        FirstPersonController firstPersonController = player.GetComponent<FirstPersonController>();
        Animator animator = player.GetComponent<Animator>();
        Camera cam = player.GetComponentInChildren<Camera>();



        characterController.enabled = true;
        firstPersonController.enabled = true;
        animator.enabled = true;
        cam.enabled = true;
    }

    #endregion
}
