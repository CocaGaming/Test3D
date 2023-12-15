using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    private float notifyLoadingTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading scr = UIManager.Instance.GetExistNotify<NotifyLoading>();
            if (scr != null)
            {
                scr.AnimationLoadingText(notifyLoadingTime);
                scr.DoAnimationLoadingProgress(notifyLoadingTime, 
                OnComplete: () => //gọi hàm này tương tự hàm void, hành động sau khi complete chạy xong loading
                {
                    Debug.Log("NotifyLoading Complete");
                    scr.Hide();
                    UIManager.Instance.ShowScreen<ScreenHome>();
                });
            }
        }
        //StartCoroutine(TestCoroutine(OnTestStart, OnTestComplete)); ví dụ về Action
    }

    //ví dụ về Action gọi hàm
    //private IEnumerator TestCoroutine(Action OnStart=null, Action OnComplete = null)//action là gọi hàm đó ở bất kì lúc nào, set ban đầu là null
    //{
    //    OnStart?.Invoke();//nếu onstart có tồn tại thì gọi
    //    yield return new WaitForSeconds(1f);
    //    OnComplete?.Invoke();
    //}
    //void OnTestStart()
    //{
    //    Debug.Log("Start");
    //}
    //void OnTestComplete()
    //{
    //    Debug.Log("Complete");
    //}
}
