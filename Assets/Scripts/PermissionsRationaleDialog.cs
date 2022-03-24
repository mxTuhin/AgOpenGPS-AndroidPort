using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionsRationaleDialog : MonoBehaviour
{
    const int kDialogWidth = 1200;
    const int kDialogHeight = 500;
    private bool windowOpen = true;

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(100, 200, kDialogWidth - 20, kDialogHeight - 50), "Please let me use the Location Service.");
        if (GUI.Button(new Rect(30, kDialogHeight - 30, 300, 100), "No"))
        {
            Application.Quit();
        }
        if (GUI.Button(new Rect(kDialogWidth - 110, kDialogHeight - 30, 300, 100), "Yes"))
        {
#if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.FineLocation);
#endif
            windowOpen = false;
        }
    }

    void OnGUI ()
    {
        if (windowOpen)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, DoMyWindow, "Permissions Request Dialog");
        }
    }
}
