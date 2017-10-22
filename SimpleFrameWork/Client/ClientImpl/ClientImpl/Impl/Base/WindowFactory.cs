using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class WindowFactory:Singleton<WindowFactory> {    
    private List<WindowType> m_winList = new List<WindowType>();
   

    public void CreateWindow(WindowType winType, EventManager.VoidHandle handle=null,bool isShow=true,params object[] param)
    {     
        if (m_winList.Contains(winType))
            return;
        Window win = WindowManager.instance.GetWindow(winType);
      //  Logger.Log("Create Window name：{0}，isshow:{1}", win.Name, isShow);
        m_winList.Add(winType);
        if (win == null)
        {
            win = (Window)Activator.CreateInstance(Type.GetType(WindowManager.instance.GetWindowTypeName(winType)));
            win.m_winType = winType;          
          //  PanelManager.instance.CreatePanel(win, win.bundle, handle, isShow, param);
        }
        else
        {
            m_winList.Remove(winType);
            if (win.IsLoad)
            {
                if (isShow)
                {
                    if (handle != null)
                        handle();
                    win.Show(param);
                }
                else
                    win.Hide();
            }
        }
    } 

    public void OnCreateWindow(Window win)
    {
        if (m_winList.Contains(win.m_winType))
        {
         //   Logger.Log("OnCreateWinType:{0}", win.m_winType);
            m_winList.Remove(win.m_winType);
        }
    }

    public void OnStopLoad(WindowType wintype)
    {
        if (m_winList.Contains(wintype))
        {
           // Logger.Log("OnStopLoadWinType:{0}", wintype);
            m_winList.Remove(wintype);
        }
    }
}
