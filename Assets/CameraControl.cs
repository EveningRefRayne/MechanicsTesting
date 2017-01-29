using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;
using System.Drawing;

public class CameraControl : MonoBehaviour {
    
    //## - Means set in the inspector

    public float XSensitivity = 10f;//##
    public float YSensitivity = 10f;//##
    public float invertX = 1;
    public float invertY = 1;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;//##
    public float MaximumX = 90;//##
    public bool smooth;
    public float smoothTime = 2f;//##
    public Transform cameraAnchorZ;//##
    public Transform cameraAnchorX;//##

    private Quaternion cameraAnchorTargetRotX;
    private Quaternion cameraAnchorTargetRotZ;
    private bool lockCursor = false;
    private bool MLMode = true;


    public void Awake()
    {
        //Sets the character and camera's target rotation to their current one when the script starts running.
        cameraAnchorTargetRotZ = cameraAnchorZ.localRotation;
        cameraAnchorTargetRotX = cameraAnchorX.localRotation;
        //setCursorLock(lockCursor);
    }

    private void rotateCam()
    {
        //Updates every frame. If the player's in mouselook mode, it runs the function that does the work and checks to see if they want to lock/unlock the mouse.
        if (MLMode)
        {
            if (lockCursor) lookRotation();
            //updateCursorLock();
        }
    }


    private void lookRotation()
    {
        //This is what runs the mouselook, moving the character and camera transforms to follow the mouse. Called every Update.
        float yRot = Input.GetAxis("Mouse X") * XSensitivity * invertX;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity * invertY;

        cameraAnchorTargetRotZ *= Quaternion.Euler(0f, yRot, 0f);
        cameraAnchorTargetRotX *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            cameraAnchorTargetRotX = clampRotationAroundXAxis(cameraAnchorTargetRotX);

        if (smooth)
        {
            cameraAnchorTargetRotZ = Quaternion.Slerp(cameraAnchorZ.localRotation, cameraAnchorTargetRotZ,
                smoothTime * Time.deltaTime);
            cameraAnchorTargetRotX = Quaternion.Slerp(cameraAnchorX.localRotation, cameraAnchorTargetRotX,
                smoothTime * Time.deltaTime);
        }
        //cameraAnchor.localRotation = Quaternion.Euler(cameraAnchorTargetRotUpright);
        cameraAnchorZ.localRotation = cameraAnchorTargetRotZ;
        cameraAnchorX.localRotation = cameraAnchorTargetRotX;
    }

    private Quaternion clampRotationAroundXAxis(Quaternion q)
    {
        //This is used by lookRotation to clamp the axis to vertical up and down so the player can't do terrible things with the camera.
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX); 
        return q;
    }

    /*private void updateCursorLock()
    {
        //this checks every update if the player hit escape to unlock the mouse, or clicked back in.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            setCursorLock(false);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            setCursorLock(true);
        }
    }*/


    /*private void setCursorLock(bool value)
    {
        //this is called internally to do the locking and unlocking when you want to lock or unlock the cursor.
        lockCursor = value;
        if (lockCursor)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }*/

    public void setMLMode(bool value)
    {
        //This is an external function to be called by other scripts to change into and out of mouselook mode entirely.
        MLMode = value;
        //setCursorLock(MLMode);
    }

    //These two are for inverting the X and Y movement.
    public void invertXLook()
    {
        invertX *= -1;
    }
    public void invertYLook()
    {
        invertY *= -1;
    }












    public bool setLockPos = true;
    private Point mouseLockPos;

    public void Update()
    {

        
        if(Input.GetAxis("MiddleClick") == 1)
        {
            if(setLockPos == true)
            {
                mouseLockPos = System.Windows.Forms.Cursor.Position;
                setLockPos = false;
                lockCursor = true;
                UnityEngine.Cursor.visible = false;
            }
            System.Windows.Forms.Cursor.Position = mouseLockPos;
            rotateCam();
        }
        else
        {
            lockCursor = false;
            setLockPos = true;
            UnityEngine.Cursor.visible = true;
        }
    }	
}