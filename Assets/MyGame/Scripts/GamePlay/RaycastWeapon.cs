using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public bool isFiring = false;

    private Ray ray;
    private RaycastHit hitInfo;
    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.Emit(1);//số 1 là play 1 lần
        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position-raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))//ktra xem tia bắn trúng gì
        {
            Debug.DrawLine(ray.origin, hitInfo.point,Color.red,1f);
            hitEffect.transform.position = hitInfo.point;//effect sinh ra chỗ bị bắn trúng
            hitEffect.transform.forward = hitInfo.normal;//effect sinh ra sẽ vuông góc vị trí bị bắn và đối diện với mình
            hitEffect.Emit(1);
        }
    }
    public void StopFiring()
    {
        isFiring=false;
    }
}
