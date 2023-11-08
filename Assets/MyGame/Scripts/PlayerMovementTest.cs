using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        print ($"Vector Magnitude before normalize: {movementDirection.magnitude}");//trc khi chuẩn hóa
        movementDirection.Normalize();//chuẩn hóa vector, bth khi đi chéo thì vector chéo sẽ lớn hơn 1 nên đi chéo sẽ nhanh hơn dọc và ngang.
                                      //hàm này giúp vector chéo lúc nào cũng tối đa bằng 1 và = vector dọc và ngang.
        print($"Vector Magnitude after normalize: {movementDirection.magnitude}");//sau chuẩn hóa

        transform.Translate(movementDirection *moveSpeed*Time.deltaTime, Space.World);//dùng để di chuyển những vật ko có vật lý

        if(movementDirection != Vector3.zero)//nếu có input nút nào đó
        {
            //tạo 1 góc xoay từ hướng nhìn nhân vật hiện tại tới hướng input mới (vd đang nhìn phải, bấm input trái thì góc là 180 độ)
            Quaternion toRotation= Quaternion.LookRotation(movementDirection, Vector3.up);
            //xoay rotation hiện tại tới góc đã tính 
            transform.rotation =Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed* Time.deltaTime);
        }
    }
}
