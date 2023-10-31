using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;
        protected LayerMask mask;
        protected Vector3 originalOffset;
        protected float originalDist;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform,LayerMask layerMask,Vector3 offset)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
            mask = layerMask;
            originalOffset = offset;
            originalDist = Vector3.Distance(mPlayerTransform.position, mCameraTransform.position);

        }

        public void RepositionCamera()
        {
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point

            float distance = Vector3.Distance(mPlayerTransform.position, mCameraTransform.position);
            Vector3 direction = (mCameraTransform.position - mPlayerTransform.position).normalized;

            RaycastHit hit;
            //cast a ray from the player to the camera to detect if theres an obstruction between them
            bool rayCast = Physics.Raycast(mPlayerTransform.position, direction, out hit, distance, mask);
  
            Vector3 offset = rayCast? offset = hit.point - mPlayerTransform.position : originalOffset;


            CameraConstants.CameraPositionOffset = Vector3.Lerp(CameraConstants.CameraPositionOffset, offset, Time.deltaTime);
        }
        public abstract void Update();
    }
}
