using UnityEngine;

namespace TPC
{   //TPCTrack Class

    public class TPCTrack : TPCBase
    {
        public TPCTrack(Transform cameraTransform, Transform playerTransform)
             : base(cameraTransform, playerTransform)
        {
        }
        public override void Frame()
        {
            const float playerHeight = 2.0f;
            Vector3 targetPos = mainPlayerTransform.position;
            targetPos.y += playerHeight;
            mainCameraTransform.LookAt(targetPos);
        }
    }
}