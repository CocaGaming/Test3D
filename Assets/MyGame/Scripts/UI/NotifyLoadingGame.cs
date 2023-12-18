using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotifyLoadingGame : BaseNotify
{
    public TextMeshProUGUI loadingPercentText;
    public Slider loadingSlider;
    public override void Init()
    {
        base.Init();
        StopAllCoroutines();//chỉ stop coroutine trong script này
        StartCoroutine(LoadScene());
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }

    private IEnumerator LoadScene()
    {
        yield return null;//nghĩa là trong 1 frame ko làm gì hết

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingPercentText.SetText($"LOADING SCENES: {asyncOperation.progress*100}%");
            if (asyncOperation.progress >= 0.9f)
            {
                loadingSlider.value = 1f;
                loadingPercentText.SetText($"LOADING SCENES: {loadingSlider.value * 100}%");
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                }
                yield return new WaitForSeconds(1f);//nghĩa là trong 1s ko làm gì hết
                asyncOperation.allowSceneActivation = true;
                this.Hide();
            }
            yield return null;//để thoát ra khỏi vòng while
        }
    }
}
