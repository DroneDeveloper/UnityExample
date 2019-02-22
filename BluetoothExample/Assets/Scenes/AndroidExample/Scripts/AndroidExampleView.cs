using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidExampleView : MonoBehaviour {

    ConnectManager connectManager;
    public Text stateText;
    public Text dataText;
    public InputField inputField;

	// Use this for initialization
	void Start () {
        connectManager = new ConnectManager(inputField.text);
    }
	
	// Update is called once per frame
	void Update () {
        connectManager.AndroidListening();
        dataText.text = Device.data.ToString();
        stateText.text = connectManager.notification;
    }

    /// <summary>
    /// 안드로이드 블루투스해제
    /// </summary>
    public void Connect()
    {
        connectManager = new ConnectManager(inputField.text);
        connectManager.AndroidInit();
        connectManager.AndroidConnect();
    }

    /// <summary>
    /// 안드로이드 블루투스 연결해제
    /// </summary>
    public void Disconnect()
    {
        connectManager.AndroidDisconnect();
    }
}
