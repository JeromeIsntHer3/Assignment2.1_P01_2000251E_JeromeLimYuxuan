using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
   // We will instantiate the prefab using the name
   // of the prefab.
   public string PlayerPrefabName;

   // We keep a reference to the spawnpoints component.
   // This is required to spawn our player at runtime.
   public PlayerSpawnPoints SpawnPoints;

   // This is the game object created from the prefab name.
   private GameObject _playerGameObject;

   // We will create out third-person camera from
   // this script and bind it to the camera at runtime.
   private ThirdPersonCamera _thirdPersonCamer;

   private void Start()
   {
      CreatePlayer();
   }


   public void CreatePlayer()
   {
      _playerGameObject = PhotonNetwork.Instantiate(PlayerPrefabName,
          SpawnPoints.GetSpawnPoint().position,
          SpawnPoints.GetSpawnPoint().rotation,
          0);

      _playerGameObject.GetComponent<PlayerMovement>().mFollowCameraForward = false;
      _thirdPersonCamer = Camera.main.gameObject.AddComponent<ThirdPersonCamera>();
      _thirdPersonCamer.mainPlayerTransform = _playerGameObject.transform;
      _thirdPersonCamer.mainCameraTransform = Camera.main.transform;
      _thirdPersonCamer.inspDamping = 5.0f;
      _thirdPersonCamer.myCameraSelection = ThirdPersonCamera.CameraSelection.TrackPositionAndRotation;
   }
   public void OnClick_LeaveRoom()
   {
      Debug.LogFormat("LeaveRoom");
      PhotonNetwork.LeaveRoom();
   }
   public override void OnLeftRoom()
   {
      Debug.LogFormat("OnLeftRoom()");
      SceneManager.LoadScene("Menu");
   }
}