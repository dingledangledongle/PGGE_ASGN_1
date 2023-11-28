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

        public TPCBase(Transform cameraTransform, Transform playerTransform,LayerMask layerMask,Vector3 offset)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
            mask = layerMask;

        }

        public void RepositionCamera()
        {
            //get the offset of the player's height for the camera
            Vector3 yOffset = new Vector3(0, mPlayerTransform.GetComponent<CharacterController>().height, 0);

            //get distance between the camera and (player position + offset)
            float distance = Vector3.Distance(mPlayerTransform.position + yOffset, mCameraTransform.position);

            //get the direction from the player+offset to camera
            Vector3 direction = (mCameraTransform.position - (mPlayerTransform.position + yOffset)).normalized; // player to camera direction

            //cast a ray from the player to the camera to detect if theres an obstruction between them
            bool rayCast = Physics.Raycast(mPlayerTransform.position + yOffset, direction, out RaycastHit hit, distance, mask);

            //Debug.DrawRay(mPlayerTransform.position + yOffset, direction * distance, Color.red);

            //if the ray hits an obstruction, the camera's position would move to where the ray hits
            if(rayCast)
                mCameraTransform.position = hit.point;
        }
        public abstract void Update();
    }
}
