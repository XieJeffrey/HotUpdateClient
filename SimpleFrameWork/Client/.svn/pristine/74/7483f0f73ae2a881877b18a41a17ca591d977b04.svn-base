 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Window
{
    public Window()
    {
        m_name = GetType().Name;
    }

    #region UI事件

    protected void RegistEvent(UIEventType UIEventType, EventManager.VoidHandle voidHandle)
    {
        EventManager.instance.RegistEvent(UIEventType, this, voidHandle);
    }

    protected void RemoveEvent(UIEventType UIEventType)
    {
        EventManager.instance.RemoveEvent(UIEventType);
    }

    #endregion

    #region 虚函数

    public virtual void OnShow()
    {
    }

    public virtual void OnClose()
    {
    }

    public virtual void OnHide()
    { }

    public virtual void OnInit()
    {
        if (!m_isInit)
        {

            if (m_mono == null)
            {
                m_mono = m_gameObject.AddComponent<emptyMono>();
            }

            if (m_gameObject.GetComponent<Canvas>() == null)
            {
                m_gameObject.AddComponent<Canvas>();
            }
            m_panel = m_gameObject.GetComponent<Canvas>();
            m_sortingOrder = m_gameObject.transform.GetSiblingIndex();
            m_panel.sortingOrder = m_sortingOrder;

            RegistEvents();
        }

    }

    public virtual void RegistEvents()
    {
       /// Logger.Log("registEvents");
    }

    private void RemoveEvents()
    {
        m_eventLsit.Clear();
    }

    public virtual void OnUpdate()
    {

    }

    #endregion

    #region 成员
    private bool m_isInit = false;
    public Canvas m_panel;
    private Transform parent = UnityEngine.GameObject.Find("Canvas").transform;
    public UnityEngine.GameObject m_gameObject;
    public List<UIEventType> m_eventLsit = new List<UIEventType>();    
    public emptyMono m_mono;
    public bool m_done = false;

    private bool m_isShow = false;
    public bool IsShow
    {
        get { return m_isShow; }
    }

    private string m_name;
    public string Name
    {
        get
        {
            return m_name;
        }
    }

    private bool m_isLoad = false;
    public bool IsLoad
    {
        get { return m_isLoad; }
    }

    public WindowResType m_resType;

    public WindowType m_winType;

    private int m_sortingOrder;
    public int SortingOrder
    {
        get { return m_sortingOrder; }
        set { m_sortingOrder = value; }
    }

    public virtual string bundle
    {
        get { return string.Empty; }
    }

    public virtual ResourceType resType
    {
        get { return ResourceType.Resource; }
    }




    

    public Dictionary<string, GameObject> m_cache_go = new Dictionary<string, GameObject>();


    #endregion
    public void OnLoad()
    {
        RemoveEvents();
        if (m_panel == null)
        {
            m_panel = m_gameObject.GetComponent<Canvas>();
        }
        m_panel.sortingOrder = m_sortingOrder;
        m_gameObject.transform.parent = parent;
        m_gameObject.transform.localScale = UnityEngine.Vector3.one;
        m_gameObject.transform.localPosition = new UnityEngine.Vector3(0, 0, 0);
        if (m_isInit == false)
        {
            OnInit();
            m_isInit = true;
        }
        m_isLoad = true;
    }


    public virtual void Close()
    {
        RemoveEvents();
        OnClose();
        m_cache_go.Clear();
        m_done = false;
        m_isLoad = false;
        m_isShow = false;
        GameObject.Destroy(m_gameObject);
        WindowManager.instance.RemoveWindow(this);
    }


    public void Hide()
    {
        m_gameObject.SetActive(false);
        m_isShow = false;
        OnHide();
    }

    public void Show(params object[] param)
    {
        m_gameObject.SetActive(true);
        m_isShow = true;
        OnShow();
    }

    public UIEventListener Register(GameObject go)
    {
        return UIEventListener.Get(go);
    }



    public GameObject Find(string goName)
    {
        if (string.IsNullOrEmpty(goName))
            return null;
        if (m_cache_go.ContainsKey(goName))
            return m_cache_go[goName];
        GameObject go = m_gameObject.transform.FindChild(goName).gameObject;

        if (go != null)
            m_cache_go.Add(goName, go);
        else { }
           // Logger.Log("can't find gameObject Name:{0},parent:{1}", m_gameObject.name, goName);
        return go;
    }

    public GameObject Find(GameObject go, string goName)
    {
        if (go == null || string.IsNullOrEmpty(goName))
            return null;
        string key = go.name + "_" + goName;
        if (m_cache_go.ContainsKey(key))
            return m_cache_go[key];
        GameObject tmpgo = m_gameObject.transform.FindChild(goName).gameObject;
        if (tmpgo != null)
            m_cache_go.Add(goName, tmpgo);
        else { }
           // Logger.Log("can't find gameObject Name:{0},parent:{1}", m_gameObject.name, goName);
        return tmpgo;
    }


    public T Find<T>(string goName) where T : class
    {
        GameObject go = Find(goName);
        if (go != null)
        {
            return go.GetComponent<T>();
        }
        return null;
    }

    public T Find<T>(GameObject go, string goName) where T : class
    {
        GameObject obj = Find(go, goName);
        if (obj != null)
        {
            return go.GetComponent<T>();
        }
        return null;
    }

    public void SetActive(GameObject go, bool isActive)
    {
       // UIHelper.instance.SetActive(go, isActive);
    }
}
