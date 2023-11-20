using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    [SerializeField]
    private float maximumForce;//lực bắn tối đa
    [SerializeField]
    private float maximumForceTime;//tgian giữ chuột để đạt lực tối đa
    private float timeMouseButtonDown;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))//số 0 là chuột trái
        {
            timeMouseButtonDown = Time.time;
        }
        if(Input.GetMouseButtonUp(0))//khi thả chuột ra mới bắn
        {
            Ray ray=mainCamera.ScreenPointToRay(Input.mousePosition);//tạo ra 1 tia truyền vào vị trí click chuột
            if(Physics.Raycast(ray,out RaycastHit hitInfo))//tia bắn trúng
            {
                ZombieTest zombie = hitInfo.collider.GetComponentInParent<ZombieTest>();//ví dụ bắn trúng đầu, bộ phận cơ thể, thì get component từ GO zombie
                if(zombie != null)
                {
                    float mouseButtonDownDuration=Time.time - timeMouseButtonDown;//tgian giữ chuột trái
                    float forcePercentage=mouseButtonDownDuration/maximumForceTime;//phần trăm lực
                    float forceMagnitude=Mathf.Lerp(1,maximumForce,forcePercentage);//nội suy tuyến tính

                    Vector3 forceDirection=zombie.transform.position-mainCamera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force=forceMagnitude*forceDirection;

                    zombie.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}
