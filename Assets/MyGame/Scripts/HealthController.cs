using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    [Range(0,1)]
    private float maximumInjuredLayerWeight;//giá trị injured weight tối đa có thể đạt dc

    private float maxHealth = 100;
    private float curHealth;
    private int injuredLayerIndex;
    private float layerWeightVelocity;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        animator=GetComponent<Animator>();
        injuredLayerIndex = animator.GetLayerIndex("Injured Layer");//hàm lấy giá trị index của layer

        print($"Injured Layer Index: {injuredLayerIndex}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))//mỗi lần bấm cách là giảm 10 máu
        {
            curHealth -= maxHealth / 10;
            
            if(curHealth < 0)//khi máu bé hơn 0 thì quay lại base layer
            {
                curHealth = maxHealth;
            }
        }

        float healthPercentage=curHealth/maxHealth;//phần trăm máu
        healthText.text = $"HP: {healthPercentage * 100}%";

        float currentInjuredLayerWeight=animator.GetLayerWeight(injuredLayerIndex);//lấy chỉ số weight hiện tại trong layer
        float targetInjuredLayerWeight=(1- healthPercentage)*maximumInjuredLayerWeight;
        animator.SetLayerWeight(injuredLayerIndex, Mathf.SmoothDamp(currentInjuredLayerWeight,//hàm SmoothDamp để thay đổi mượt mà giữa 2 giá trị trong 1 khoảng tgian
                                                   targetInjuredLayerWeight,//ví dụ từ weight 0 lên 0.1
                                                   ref layerWeightVelocity,
                                                   0.2f));//giảm 10% máu thì tăng 0.1 weight
    }
}
