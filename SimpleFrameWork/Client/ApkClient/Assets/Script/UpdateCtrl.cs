using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UpdateCtrl : MonoBehaviour {
    public GameObject ConfirmGo;
    public Slider mSldier;
    public Text contentLabel;
    private Action OkHandle;
    private Action CancleHandle;
	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        mSldier.value = UpdateHelper.GetCopyResProgress();
	}

    public void SetUpdateWindow(Action OkFunc,Action CancleFunc,string content)
    {
        contentLabel.text = content;
        OkHandle = OkFunc;
        CancleHandle = CancleFunc;
    }
    

    public void OnClickOkBtn()
    {
        OkHandle();
    }

    public void OnClickCancleBtn()
    {
        CancleHandle();
    }


}
