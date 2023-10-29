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

        public TPCBase(Transform cameraTransform, Transform playerTransform,LayerMask layerMask)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
            mask = layerMask;
        }

        public void RepositionCamera()
        {
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point
            Debug.DrawLine(mCameraTransform.position, mPlayerTransform.position,Color.cyan);
            RaycastHit hit;
            if (Physics.Linecast(mCameraTransform.position, mPlayerTransform.position,out hit,mask))
            {
                Vector3 test = hit.point - mCameraTransform.position;
                //mCameraTransform.position -= test;
                Debug.Log("Hit something");
            }
        }

        public abstract void Update();
    }
}
