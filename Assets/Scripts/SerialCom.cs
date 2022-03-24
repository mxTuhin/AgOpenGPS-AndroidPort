using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;

using UnityEngine.UI;

public class SerialCom : MonoBehaviour
{
    private SerialPort sp;
    private string serialPortNumber;

    private float next_time;

    private string[] resText=new string[10];

    private int resTextCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        
        try
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                Debug.Log(port);
            }
        }
        catch (Exception e)
        {
            print(e);
        }
        
        try
        {
            foreach (string mysps in SerialPort.GetPortNames())
            {
                print("1");
                print(mysps);
                if (mysps != "COM1")
                {
                    serialPortNumber = mysps;
                }
            }
            print("2");
            sp = new SerialPort("/dev/"+serialPortNumber, 115200);
            sp.Open();
            if (sp.IsOpen)
            {
                InvokeRepeating("getSerialData", 2.0f, 1.0f);
            }
            print("3");
        }
        catch (Exception e)
        {
            print("Serial Port Not Available");
        }
        print("4");
        print("Port Length: "+SerialPort.GetPortNames().Length);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getSerialData()
    {
        try
        {
            string rs = sp.ReadLine();
            print(rs);
            resText[resTextCounter % resText.Length] = rs;
            resTextCounter++;
        }
        catch (Exception e)
        {
            
        }
    }

    [SerializeField] private Text logText;
    private bool shouldKeepRepeating;
    public GameObject logger;

    public void toggleLogger()
    {
        if (logger.activeSelf)
        {
            logger.SetActive(false);
        }
        else
        {
            logger.SetActive(true);
        }
    }
}
