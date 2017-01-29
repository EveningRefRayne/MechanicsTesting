using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;
using System.Drawing;

public class CameraControl : MonoBehaviour {

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
            }
            UnityEngine.Cursor.visible = false;
            System.Windows.Forms.Cursor.Position = mouseLockPos;
        }
        else
        {
            setLockPos = true;
            UnityEngine.Cursor.visible = true;
        }
    }	
}