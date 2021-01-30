using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private  float horizontalCameraSpeed = 15f;
    [SerializeField] private  float verticalCameraSpeed = 50f;
    [SerializeField] private  float cameraTopThreshold = 5f;
    [SerializeField] private  float cameraBottomThreshold = -1.0f;

    //private Vector3 m_Velocity = Vector3.zero;
    private float m_horizontalVelocity = 0.0f;
    private float m_verticalVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cameraPos = new Vector3(0,transform.position.y,-10);
        //Top of Screen
        if(playerTransform.position.y - cameraPos.y > cameraTopThreshold) {
            cameraPos.y = playerTransform.position.y;
            cameraPos.y = Mathf.SmoothDamp(transform.position.y, cameraPos.y, ref m_verticalVelocity, verticalCameraSpeed*Time.fixedDeltaTime);
        }
        //Bottom of Screen
        if(playerTransform.position.y - cameraPos.y < cameraBottomThreshold) {
            cameraPos.y = playerTransform.position.y - cameraBottomThreshold;
            cameraPos.y = Mathf.SmoothDamp(transform.position.y, cameraPos.y, ref m_verticalVelocity, verticalCameraSpeed*Time.fixedDeltaTime*0.2f);
            //transform.position = cameraPos;
        }
        cameraPos.x = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref m_horizontalVelocity, horizontalCameraSpeed*Time.fixedDeltaTime);
        //transform.position = Vector3.SmoothDamp(transform.position,cameraPos,ref m_Velocity,);
        transform.position = cameraPos;
    }
}
