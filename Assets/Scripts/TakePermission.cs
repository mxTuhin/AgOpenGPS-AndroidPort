using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class TakePermission : MonoBehaviour
{
    public GameObject permissionPanel;
    
    void Start ()
    {
#if PLATFORM_ANDROID
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.Camera);
            
            
            
            
        }
        else
        {
            SceneManager.LoadScene("WorkingScene");
        }
#endif
    }
    

    void OnGUI ()
    {
        #if PLATFORM_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    if(!permissionPanel.activeSelf)
                        permissionPanel.SetActive(true);
                }
                else
                {
                    
                    SceneManager.LoadScene("WorkingScene");
                    
                }
        #endif
        // Now you can do things with the microphone
    }

    public void askForLocation()
    {
        #if PLATFORM_ANDROID
                Permission.RequestUserPermission(Permission.FineLocation);
                Permission.RequestUserPermission(Permission.Camera);
#endif
    }

    public void denyLocation()
    {
        Application.Quit();
    }
}
