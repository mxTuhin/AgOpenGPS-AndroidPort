using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceGPS : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(initiateGPS());
        
    }

    IEnumerator initiateGPS()
    {
        
        Input.location.Start();
        // If the connection failed this cancels location service use.
        int maxWait = 5;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            // print("Device Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " 
            //       + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            InvokeRepeating("printGPSData", 2.0f, 1.0f);
        }
    }

    private void printGPSData()
    {
        
        print("Device Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
    }
}
