using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform mainCamera;
    private Vector3 velocity = Vector3.zero;
    
    [Header("AdjustableVariables")]
    [SerializeField] private Vector3 offset;

    [SerializeField] private float smoothTime = 0.3f;

    private void Start()
    {

        offset = mainCamera.position - player.position;

    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        mainCamera.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


    }
}
