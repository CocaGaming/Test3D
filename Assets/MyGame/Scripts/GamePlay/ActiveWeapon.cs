using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public Transform weaponParent;
    public Transform crossHairTarget;
    public Animator rigController;
    private RaycastWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existingWeapon)//nếu có súng đang mang trên ng
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon != null)//ktra nếu có súng mới bắn dc
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring)//cập nhật tia bắn liên tục
            {
                weapon.UpdateFiring(Time.deltaTime);
            }

            weapon.UpdateBullets(Time.deltaTime);

            if (Input.GetButtonUp("Fire1"))//khi nhả chuột ra
            {
                weapon.StopFiring();
            }
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if (weapon)//nếu đã có súng rồi mà lụm thêm thì sẽ destroy cái đang cầm
        {
            Destroy(weapon.gameObject);
        }
        weapon=newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.SetParent(weaponParent, false);
        rigController.Play("equip_" + weapon.weaponName);
    }
}
