using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLockManager : MonoBehaviour
{
    private bool isMouseLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Cursor.lockState == CursorLockMode.None && isMouseLocked) {
            isMouseLocked = false;
        }

        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            ToggleMouseLock(true);
        }

        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMouseLock(false);
        }*/
    }

    public void ToggleMouseLock(bool lockMouse) {
        isMouseLocked = lockMouse;

        Cursor.lockState = lockMouse ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockMouse;
    }
}
