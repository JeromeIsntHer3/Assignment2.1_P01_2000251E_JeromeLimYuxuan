using UnityEngine;

namespace TPC
{
    //Base class that holds the references to the tranforms
    //TPCBase does not implement does run any functions
    public abstract class TPCBase
    {
        protected Transform mainCameraTransform;
        protected Transform mainPlayerTransform;

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mainCameraTransform = cameraTransform;
            mainPlayerTransform = playerTransform;
        }
        public abstract void Frame();
    }
}