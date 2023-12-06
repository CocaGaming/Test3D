﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerGroup
{
  List<Action<object>> actions= new List<Action<object>>();
  public void BroadCast(object value)//dùng cho object bắn ra sự kiện
    {
        for(int i=0;i<actions.Count; i++)
        {
            actions[i](value);
        }
    }
    public void Attach(Action<object> action)//dùng cho object lắng nghe sự kiện, thêm vào
    {
        for(int i=0; i<actions.Count; i++)
        {
            if (actions[i] == action)
                return;
        }
        actions.Add(action);
    }
    public void Detach(Action<object> action)//dùng cho object không có trong sự kiện, remove ra
    {
        for(int i=0;i<actions.Count ; i++)
        {
            if (actions[i] == action)
            {
                actions.Remove(action);
                break;
            }
        }
    }
}
