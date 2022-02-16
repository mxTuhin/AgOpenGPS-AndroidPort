using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Movement Components")] [SerializeField]
    private Slider vertical;

    [SerializeField] private Slider horizontal;
    [SerializeField] private Slider zoomSlider;

    private float horizontalValue;

    private float verticalValue;

    [SerializeField] private Text angleValue;
    [SerializeField] private RawImage startIcon;
    [SerializeField] private CinemachineVirtualCamera cmvCam;
    private Vector3 prev;



    [Header("Character Controller Component")]
    [SerializeField] private float _rotationSpeed;

    private CharacterController _characterController;
    private Vector3 rotation;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        horizontal.value = 0.5f;
        vertical.value = 0.5f;
        zoomSlider.value = 0.5f;
        horizontalValue = horizontal.value;
        verticalValue = vertical.value;
        prev = cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if ((horizontalValue > 0.5f || verticalValue > 0.5f)|| horizontalValue < 0.5f || verticalValue < 0.5f)
        {
            startIcon.color = new Color(0.25f, 0.9f, 0.60f);
        }
        else
        {
            startIcon.color = new Color(0.9f, 0.29f, 0.25f);
        }
    }

    private void FixedUpdate()
    {
        // Vector3 move = new Vector3(horizontalValue, 0, verticalValue);
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // _characterController.Move(move * Time.deltaTime *20);
        
        // _characterController.transform.Rotate(Vector3.up*Input.GetAxis("Horizontal")*_rotationSpeed*Time.deltaTime);
        // _characterController.Move(_characterController.transform.forward*-1 * Input.GetAxis("Vertical") * 20 *
        //                           Time.deltaTime);
        
        _characterController.transform.Rotate(Vector3.up * ((horizontalValue-0.5f) * 2 * _rotationSpeed * Time.deltaTime));
        _characterController.Move(_characterController.transform.forward * (-1 * (verticalValue-0.5f) * 2 * 20 * Time.deltaTime));
    }

    public void onHorizontalSliderChanged()
    {
        horizontalValue = horizontal.value;
        angleValue.text = ((horizontalValue-0.5f)*2).ToString("0.00");
    }

    public void onVerticalSliderChange()
    {
        verticalValue = vertical.value;
    }

    public void onVerticalReset()
    {
        vertical.value = 0.5f;
        verticalValue = 0.5f;
    }

    public void onHorizontalReset()
    {
        horizontal.value = 0.5f;
        horizontalValue = 0.5f;
    }

    public void onZoomSliderChange()
    {
        if (zoomSlider.value != 0.5f)
        {
            cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(prev.x, prev.y+(zoomSlider.value-0.5f)*4, prev.z);
        }
    }

    public void onZoomPositiveClick()
    {
        if (prev.z > 2)
        {
            cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(prev.x, prev.y, prev.z-1.0f);
            prev = cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }
        
    }
    
    public void onZoomNegativeClick()
    {
        if (prev.z < 15)
        {
            cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(prev.x, prev.y, prev.z+1.0f);
            prev = cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }
        
    }
    
    public void onYRollPositiveClick()
    {
        if (prev.y > 5)
        {
            cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(prev.x, prev.y-1.0f, prev.z);
            prev = cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }
        
    }
    
    public void onYRollNegativeClick()
    {
        if (prev.y < 25)
        {
            cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(prev.x, prev.y+1.0f, prev.z);
            prev = cmvCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }
        
    }
    
    
}
