using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private  float cameraSpeed = 0.2f;
    [SerializeField] private  float cameraTopThreshold = 5f;
    [SerializeField] private  float cameraBottomThreshold = -1.5f;

    private Vector3 m_Velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cameraPos = new Vector3(playerTransform.position.x,transform.position.y,-10);
        if(playerTransform.position.y - cameraPos.y > cameraTopThreshold) {
            cameraPos.y = playerTransform.position.y;
        }
        if(playerTransform.position.y - cameraPos.y < cameraBottomThreshold) {
            cameraPos.y = playerTransform.position.y - cameraBottomThreshold;
            transform.position = cameraPos;
        } else {
            transform.position = Vector3.SmoothDamp(transform.position,cameraPos,ref m_Velocity,cameraSpeed*Time.fixedDeltaTime);

        }
        //transform.position = cameraPos;
    }
}
