﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera=Camera.main;
        Cursor.visible = false; //ẩn chuột
        Cursor.lockState= CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera=mainCamera.transform.rotation.eulerAngles.y; //góc Y
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);//thay đổi góc nhìn camera theo trục Y khi nv xoay
    }
}
