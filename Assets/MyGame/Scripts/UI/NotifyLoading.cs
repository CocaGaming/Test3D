using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class NotifyLoading : BaseNotify
{
    public TextMeshProUGUI tmpLoading;
    public Slider slProgress;
    private string loadingText = "Loading";

    public override void Hide()
    {
        base.Hide();
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public void SetProgress(float value)
    {
        this.slProgress.value = value;
    }
    public void AnimationLoadingText(float dt)//làm ani cho chữ loading
    {
        DOTween.Kill(this.tmpLoading.GetInstanceID().ToString());//kill seq cũ đang dang dở, mỗi lần mở sẽ chạy lại seq từ đầu
        Sequence seq = DOTween.Sequence();
        seq.SetId(this.tmpLoading.GetInstanceID().ToString());
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText;
        });
        seq.AppendInterval(dt / 4f);
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText + ".";
        });
        seq.AppendInterval(dt / 4f);
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText + "..";
        });
        seq.AppendInterval(dt / 4f);
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText + "...";
        });
        seq.AppendInterval(dt / 4f);
        seq.SetId(-1);
    }
    public void DoAnimationLoadingProgress(float dt, Action OnComplete = null) //hàm chạy thanh loading
    {
        DOTween.Kill(this.slProgress.GetInstanceID().ToString());//nếu gọi hàm 2 lần thì kill quá trình cũ để chạy lại quá trình mới từ đầu
        Sequence seq =DOTween.Sequence();
        seq.SetId(this.slProgress.GetInstanceID().ToString());
        SetProgress(0);//set giá trị slider ban đầu =0
        seq.Append(this.slProgress.DOValue(slProgress.maxValue, dt).SetEase(Ease.InQuad));//slprogree.maxvalue là giá trị lớn nhất thanh chạy, dt là thời gian chạy full
        seq.OnComplete(() =>//chạy DOValue xong rồi sẽ chạy vào hàm OnComplete
        {
            OnComplete?.Invoke();//gọi hàm truyền vào để chạy
        });
    }
}
            