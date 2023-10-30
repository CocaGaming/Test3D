using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
    //Main camera:
    //Clipping Planes: near, far kcach gần nhất và xa nhất mà camera có thể render dc GO
    //Culling Mask: render những gì layer nào camera muốn thấy, ví dụ giữa Player và Cây có vật chắn, bỏ layer vật chắn ra Culling Mask

{
    public float radius;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay: " + other.name);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: " + other.name);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay: " + collision.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit: " + collision.gameObject.name);
    }
}
