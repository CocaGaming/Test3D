using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadCastEventTest : MonoBehaviour
{
    private int countVal = 0;

    private string strVal = "Hello";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            countVal++;

            if (ListenerManager.HasInstance)//đã có trên scene
            {
                ListenerManager.Instance.BroadCast(ListenType.LEFT_MOUSE_CLICK, countVal);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.RIGHT_MOUSE_CLICK, strVal);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerInfo playerInfo = new PlayerInfo()
            {
                PlayerName = "Duy",
                PlayerAge = 28
            };
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_PLAYER_INFO, playerInfo);
            }
        }
    }
}
