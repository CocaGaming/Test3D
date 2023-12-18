using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class OverlapFade : BaseOverlap
{
    [SerializeField]
    private Image imgFade;
    [SerializeField]
    private Color fadeColor;
    public override void Init()
    {
        base.Init();
        Fade(1, OnFinish);
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }
    private void SetAlpha(float alp)
    {
        Color cl=this.imgFade.color;//lấy ra giá trị màu của hình
        cl.a = alp;//thay đổi alpha của màu hình đó
        this.imgFade.color = cl;//gán lại màu cho hình đã thay đổi alpha
    }
    public void Fade(float fadeTime,Action onFinish)//thgian fade và khi fade xong thì làm gì
    {
        imgFade.color = fadeColor;
        SetAlpha(0);
        Sequence seq = DOTween.Sequence();
        seq.Append(this.imgFade.DOFade(1f, fadeTime));//fade lên giá trị 1 trong thgian bao nhiu, fade in
        seq.Append(this.imgFade.DOFade(0, fadeTime));//fade out
        seq.OnComplete(() =>
        {
            onFinish?.Invoke();
        });
    }
    private void OnFinish()
    {
        this.Hide();
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenGame>();
        }
    }
}
