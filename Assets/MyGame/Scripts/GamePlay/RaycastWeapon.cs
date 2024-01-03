using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public string weaponName;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public bool isFiring = false;
    public int fireRate = 25;//1 giây bắn dc 25v đạn
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0f;

    private Ray ray;
    private RaycastHit hitInfo;
    private float accumulatedTime;//khoảng giữa 2 lần bắn
    private List<Bullet> bullets= new List<Bullet>();
    private float maxLifeTime=3f;//thời gian tồn tại tối đa của viên đạn là bao lâu
    
    private Vector3 GetPosition(Bullet bullet)//viên đạn bay hình vòng cung do có trọng lực
    {
        //p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition)//p
            + (bullet.initialVelocity * bullet.time)//v*t
            + (0.5f * gravity * bullet.time * bullet.time);//0.5*g*t*t
    }

    private Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet=new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }
    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0)
        {
            FireBullet();
            accumulatedTime-= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0=GetPosition(bullet);
            bullet.time+= deltaTime;
            Vector3 p1=GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);//remove trong list bullet những viên nào tồn tại quá 3s
    }

    private void RaycastSegment(Vector3 start,Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))//ktra xem tia bắn trúng gì
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);
            hitEffect.transform.position = hitInfo.point;//effect sinh ra chỗ bị bắn trúng
            hitEffect.transform.forward = hitInfo.normal;//effect sinh ra sẽ vuông góc vị trí bị bắn và đối diện với mình
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }
    private void FireBullet()
    {
        muzzleFlash.Emit(1);//số 1 là play 1 lần

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed ;// hướng đã dc chuẩn hóa độ lớn thành 1 * speed
        var bullet = CreateBullet(raycastOrigin.position,velocity);
        bullets.Add(bullet);
        //ray.origin = raycastOrigin.position;
        //ray.direction = raycastDestination.position - raycastOrigin.position;

        //var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        //tracer.AddPosition(ray.origin);
        //if (Physics.Raycast(ray, out hitInfo))//ktra xem tia bắn trúng gì
        //{
        //    //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);
        //    hitEffect.transform.position = hitInfo.point;//effect sinh ra chỗ bị bắn trúng
        //    hitEffect.transform.forward = hitInfo.normal;//effect sinh ra sẽ vuông góc vị trí bị bắn và đối diện với mình
        //    hitEffect.Emit(1);

        //    tracer.transform.position = hitInfo.point;
        //}
    }

    public void StopFiring()
    {
        isFiring=false;
    }
}
