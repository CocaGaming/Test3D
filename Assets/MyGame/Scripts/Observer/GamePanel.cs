using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    public TextMeshProUGUI testText;
    public TextMeshProUGUI strText;
    public TextMeshProUGUI name;
    public TextMeshProUGUI age;
    //Dùng 1 trong 2 cặp hàm bên dưới
    private void Start()//lắng nghe sự kiện khi start
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.LEFT_MOUSE_CLICK, OnListenLeftMouseClickEvent);
            ListenerManager.Instance.Register(ListenType.RIGHT_MOUSE_CLICK,OnListenRightMouseClickEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_PLAYER_INFO,OnUpdatePlayerInfoEvent);
        }
    }

    private void OnDestroy()//bỏ sự kiện 
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.LEFT_MOUSE_CLICK,OnListenLeftMouseClickEvent);
            ListenerManager.Instance.Unregister(ListenType.RIGHT_MOUSE_CLICK,OnListenRightMouseClickEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_PLAYER_INFO,OnUpdatePlayerInfoEvent);
        }
    }

    private void OnListenLeftMouseClickEvent(object value)
    {
        if(value != null)
        {
            if(value is int countValue)
            {
                testText.text=countValue.ToString();
            }
        }
    }
    private void OnListenRightMouseClickEvent(object value)
    {
        if (value != null)
        {
            if(value is string stringValue)
            {
                strText.text = stringValue;
            }
        }
    }
    private void OnUpdatePlayerInfoEvent(object value)
    {
        if(value != null)
        {
            if(value is PlayerInfo playerInfo)
            {
                name.text = playerInfo.PlayerName;
                age.text = playerInfo.PlayerAge.ToString();
            }
        }
    }

    //private void OnEnable()//lắng nghe sự kiện
    //{
        
    //}
    //private void OnDisable()//bỏ sự kiện
    //{
        
    //}
}
