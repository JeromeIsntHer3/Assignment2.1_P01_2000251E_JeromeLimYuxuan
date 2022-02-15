using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPC;

//Main Mono Class to Call Other Class Functions
public class ThirdPersonCamera : MonoBehaviour
{
   [Header("Transforms")]
   public Transform mainCameraTransform;
   public Transform mainPlayerTransform;

   [Header("Camera Collision")]
   public LayerMask objMask;

   [Header("Setting New Game Constants")]
   public Vector3 inspCameraAngleOffset = new Vector3(5.0f, 0.0f, 0.0f);
   public Vector3 inspCameraPositionOffset = new Vector3(0f, 2f, -2f);
   public float inspDamping = 1f;

   public enum CameraSelection
   {
      Track,
      TrackPosition,
      TrackPositionAndRotation,
      IndependantRotation
   }

   [Header("Camera Selection")]
   public CameraSelection myCameraSelection = CameraSelection.TrackPositionAndRotation;
   Dictionary<CameraSelection, TPCBase> myCameras = new Dictionary<CameraSelection, TPCBase>();

   void Start()
   {
      GameConstants.CameraAngleOffset = inspCameraAngleOffset;
      GameConstants.CameraPositionOffset = inspCameraPositionOffset;
      GameConstants.Damping = inspDamping;

      //instantiate the real camera from TPC into dictionary
      myCameras.Add(CameraSelection.Track, new TPCTrack(mainCameraTransform, mainPlayerTransform));
      myCameras.Add(CameraSelection.TrackPosition, new TPCFollowTrackPosition(mainCameraTransform, mainPlayerTransform));
      myCameras.Add(CameraSelection.TrackPositionAndRotation, new TPCFollowTrackPositionAndRotation(mainCameraTransform, mainPlayerTransform));
      myCameraSelection = CameraSelection.TrackPositionAndRotation;
   }

   void Update()
   {
      GameConstants.CameraAngleOffset = inspCameraAngleOffset;
      GameConstants.CameraPositionOffset = inspCameraPositionOffset;
      GameConstants.Damping = inspDamping;
   }

   void LateUpdate()
   {
      //call the TPC implementation updates
      myCameras[myCameraSelection].Frame();
      RepositionCamera();
   }

   //correct version player to camera
   void RepositionCamera()
   {
      RaycastHit hit;
      if (Physics.SphereCast(mainPlayerTransform.position, 0.01f, -(mainPlayerTransform.forward), out hit, 3f, objMask))
      {
         inspCameraPositionOffset.z = 0.1f - hit.distance;
      }
   }
}
