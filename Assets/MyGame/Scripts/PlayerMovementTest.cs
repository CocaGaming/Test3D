using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float gravityMultiplier;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float jumpButtonGracePeriod;//tgian gia hạn khi nv vừa ra khỏi mép vật thể vẫn có thể bấm nhảy dc

    private float horizontalInput;
    private float verticalInput;
    private float yForce;
    private float originalStepOffset;
    private float? lastGroundedTime;//dấu ? có nghĩa là biến có thể null cũng dc, lần cuối nv đứng trên mặt đất
    private float? jumpButtonPressTime;//tgian bấm nút cách
    private bool isJumping;
    private bool isGrounded;


    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim=GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset=characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");//ko dùng axisraw, để nhân vật tăng tốc từ từ khi di chuyển
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        //print($"Vector Magnitude before normalize: {movementDirection.magnitude}");//trc khi chuẩn hóa

        float inputMagnitude= Mathf.Clamp01(movementDirection.magnitude);//độ lớn vector
        playerAnim.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);//0.05f là damptime: tgian chờ để chuyển anim
        //float speed = inputMagnitude * moveSpeed;

        movementDirection.Normalize();//chuẩn hóa vector, bth khi đi chéo thì vector chéo sẽ lớn hơn 1 nên đi chéo sẽ nhanh hơn dọc và ngang.
                                      //hàm này giúp vector chéo lúc nào cũng tối đa bằng 1 và = vector dọc và ngang.
        print($"Vector Magnitude after normalize: {movementDirection.magnitude}");//sau chuẩn hóa

        /*transform.Translate(magnitude* moveSpeed*Time.deltaTime* movementDirection, Space.World);*///dùng để di chuyển những vật ko có vật lý

        //yForce là trọng lực của nv
        float gravity = Physics.gravity.y * gravityMultiplier;

        if(isJumping && yForce>0 && Input.GetButton("Jump")== false)//giữ nút nhảy càng lâu bay càng cao
        {
            gravity *= 2;
        }
        yForce += gravity * Time.deltaTime;//physics.gravity.y=-9.81f, += để nv khi nhảy lên sẽ rơi xuống lại từ từ, ko bị giật hẳn xuống

        if(characterController.isGrounded)
        {
            lastGroundedTime = Time.time; //Time.time là tgian hiện tại
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            yForce = -0.5f;//chắc chắn nv chạm đất thay vì set =0 có lúc nhảy dc lúc ko
            characterController.stepOffset = originalStepOffset;//chống tình trạng nv bị dính trên tường khi nhảy, lúc nv đang trên đất thì mới so với stepOffset
            //Đang trên mặt đất
            playerAnim.SetBool("IsGrounded", true);
            isGrounded = true;

            playerAnim.SetBool("IsJumping", false);
            isJumping = false;

            playerAnim.SetBool("IsFalling", false);
            if (Time.time-jumpButtonPressTime<= jumpButtonGracePeriod)//khi bấm nút cách
            {
                yForce = Mathf.Sqrt(jumpForce* -3.0f * gravity);

                playerAnim.SetBool("IsJumping", true);
                isJumping = true;

                jumpButtonPressTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;//lúc đang nhảy thì nv sẽ ko bị so sánh độ cao với stepOffset
            //Đang nhảy
            playerAnim.SetBool("IsGrounded", false);
            isGrounded = false;

            if((isJumping && yForce < 0) || yForce<-3)//Nhảy lên và đang rớt xuống || đang đi mà lọt hố, số -3 mà âm bé quá thì sẽ bị nhầm với đi xuống bậc cầu thang= falling
            {
                playerAnim.SetBool("IsFalling", true);
            }
        }

        //Quay hướng nhìn của nv theo hướng di chuyển 
        if (movementDirection != Vector3.zero)//nếu có input nút nào đó
        {
            playerAnim.SetBool("IsMoving", true);
            //tạo 1 góc xoay từ hướng nhìn nhân vật hiện tại tới hướng input mới (vd đang nhìn phải, bấm input trái thì góc là 180 độ)
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            //xoay rotation hiện tại tới góc đã tính    
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("IsMoving", false);
        }

        if(!isGrounded)
        {
            Vector3 velocity =inputMagnitude* jumpSpeed* movementDirection;
            velocity.y = yForce;

            characterController.Move(velocity * Time.deltaTime);//vì là trong hàm update nên *Time.deltaTime
        }
    }
    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = playerAnim.deltaPosition * moveSpeed;//trong Update mode (Animator) normal đã nhân sẵn time.deltatime
            velocity.y = yForce * Time.deltaTime;//phải nhân time.deltatime để đồng bộ với velocity

            characterController.Move(velocity);
        } 
    }
}
