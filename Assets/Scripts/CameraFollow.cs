using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float offsetX = 0f;
    public float offsetY = 0f;
    private Func<Vector2> GetFollowPosition;
    
    public void Setup(Func<Vector2> GetFollowPosition){
        this.GetFollowPosition = GetFollowPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraFollowPosition = GetFollowPosition();
        transform.position = new Vector3(cameraFollowPosition.x + offsetX, cameraFollowPosition.y + offsetY, transform.position.z);
    }
}
