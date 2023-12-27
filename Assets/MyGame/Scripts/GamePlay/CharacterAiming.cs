﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public Rig aimLayer;
    public float turnSpeed = 15f;
    public float aimDuration = 0.3f;
    private Camera mainCamera;
    private RaycastWeapon raycastWeapon;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera=Camera.main;
        Cursor.visible = false; //ẩn chuột khi play 
        Cursor.lockState= CursorLockMode.Locked;
        raycastWeapon=GetComponentInChildren<RaycastWeapon>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            raycastWeapon.StartFiring();
        }
        if (Input.GetButtonUp("Fire1"))//khi nhả chuột ra
        {
            raycastWeapon.StopFiring();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera=mainCamera.transform.rotation.eulerAngles.y; //xoay quanh trục Y
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);//thay đổi góc nhìn camera theo trục Y khi nv xoay
    }
}
