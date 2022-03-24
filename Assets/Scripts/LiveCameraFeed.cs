using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LiveCameraFeed : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackGround;

    public RawImage background;

    public AspectRatioFitter fit;
    

    GameObject dialog = null;

    // Start is called before the first frame update
    void Start()
    {
        print("In Camera Scene");
        defaultBackGround = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            print("No Cams");
            camAvailable = false;
        }

        for (int i = 0; i < devices.Length; ++i)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            print("No back Cam");
            return;
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        if (!camAvailable)
            return;
        float ratio = (float) backCam.width / (float) backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    public void playCamera()
    {
        backCam.Play();
        background.texture = backCam;
        camAvailable = true;
    }

    public void pauseCamera()
    {
        backCam.Stop();
    }
    
    static byte[] ScreenshotWebcam(WebCamTexture wct)
    {
        

        return null;
    }
    
    public string dirPath;
    public void takePicture()
    {
        byte[] data = ScreenshotWebcam(backCam);

        File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".jpg", data);
        // File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", saveImageBytes);
        Debug.Log(data.Length / 1024 + "Kb was saved as: " + dirPath);
    }
    
}
