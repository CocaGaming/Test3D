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

    private float maxHealth = 100;
    private float curHealth;
    private int injuredLayerIndex;
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
    }
}
