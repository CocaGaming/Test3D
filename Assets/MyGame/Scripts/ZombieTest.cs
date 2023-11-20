using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieTest : MonoBehaviour
{
    private enum ZombieState
    {
        Walking,
        Ragdoll
    }
    [SerializeField]
    private Camera mainCamera;
    private Rigidbody[] zombieRBs;
    private CharacterJoint[] zombieJoints;
    private ZombieState currentState=ZombieState.Walking;//set state ban đầu là walking
    private Animator zombieAnimator;
    private CharacterController zombieCC;

    private void Awake()
    {
        zombieRBs=GetComponentsInChildren<Rigidbody>();//get tất cả component rigidbody trong tất cả các bộ phận cơ thể zombie rồi add vào mảng
        zombieJoints=GetComponentsInChildren<CharacterJoint>();
        zombieAnimator=GetComponent<Animator>();
        zombieCC=GetComponent<CharacterController>();
        DisableRagdoll();
        SetUpCharacterJoints();
    }

    // Update is called once per frame
    void Update()
    {
       
        switch (currentState)
        {
            case ZombieState.Walking:
                WalkingBehaviour();
                break;
            case ZombieState.Ragdoll:
                RagdollBehaviour(); 
                break;
        }
    }
    private void SetUpCharacterJoints()//chỉnh để cho khi bắn 1 lực quá mạnh, xương của nv sẽ ko bị giãn ra
    {
        foreach(var joint in zombieJoints)
        {
            joint.enableProjection = true;
        }
    }
    private void DisableRagdoll()//hàm để tắt vật lý trong từng bộ phận để khi play nv ko bị ngã xuống
    {
        foreach(var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = true;
        }

        zombieAnimator.enabled = true;
        zombieCC.enabled = true;
    }
    private void EnableRagdoll()
    {
        foreach (var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = false;
        }

        zombieAnimator.enabled = false;//khi chết tắt animation
        zombieCC.enabled = false;//khi chết ko di chuyển
    }
    private  void WalkingBehaviour()
    {
        Vector3 direction=mainCamera.transform.position- transform.position;//di chuyển đến hướng camera
        direction.y = 0;
        direction.Normalize();

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation=Quaternion.RotateTowards(transform.rotation, toRotation, 20*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))//khi bấm space thì bật vật lý trên các bộ phận (nv sẽ ngã xuống tương tự như chết)
        {
            EnableRagdoll();
            currentState = ZombieState.Ragdoll;
        }
    }
    private void RagdollBehaviour()
    {

    }
    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();
        //Sắp xếp mảng các rigidbody tăng dần theo khoảng cách từ vị trí hit tới rigidbody đó, nếu vị trí rb nào gần hitPoint nhất sẽ get ra
        Rigidbody hitRb=zombieRBs.OrderBy(rigidbody=>Vector3.Distance(rigidbody.position, hitPoint)).First();

        hitRb.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);//sau khi get dc rb, truyền lực vào điểm hitPoint, ...Impulse: tác động liền lập tức
    }
}
