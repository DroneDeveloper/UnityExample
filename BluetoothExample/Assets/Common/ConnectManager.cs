using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Android
/// </summary>
public class ConnectManager : MonoBehaviour {
    int environment;

    /// <summary>
    /// 환경에 맞춰 블루투스 연결을 셋팅
    /// </summary>
    public ConnectManager(string deviceName)
    {
        this.environment = (int)Environment.OSVersion.Platform;
        //if(environment == 128)
        //if (environment == 6)
        if (environment == 4)//android
        {
            this.deviceName = deviceName;
        }
    }

    #region Android
    AndroidBluetoothManager androidBluetoothManager;
    string deviceName;
    public string notification;
    bool isUpdateDataStarted = false;
    
    /// <summary>
    /// 안드로이드용 블루투스 초기화 메서드
    /// </summary>
    public void AndroidInit() {
#if UNITY_ANDROID
        isUpdateDataStarted = false;
        androidBluetoothManager = new AndroidBluetoothManager();
        androidBluetoothManager.CallBluetoothInit();
#endif
    }

    /// <summary>
    /// 안드로이드 블루투스 데이터 리스닝 메서드
    /// </summary>
    public void AndroidListening()
    {
#if UNITY_ANDROID
        if (isUpdateDataStarted)
        {
            androidBluetoothManager.CallBluetoothData();
        }
         notification = AndroidBluetoothManager.notification;
#endif
    }

    /// <summary>
    /// 안드로이드용 블루투스 연결 메서드
    /// </summary>
    public void AndroidConnect()
    {
#if UNITY_ANDROID
        androidBluetoothManager.CallConnectedDevice(deviceName);
        isUpdateDataStarted = true;
#endif
    }

    /// <summary>
    /// 안드로이드용 블루투스 연결 해제 메서드
    /// </summary>
    public void AndroidDisconnect()
    {
#if UNITY_ANDROID
        androidBluetoothManager.CallBluetoothStop();
        isUpdateDataStarted = false;
#endif
    }

    void OnApplicationQuit() {
#if UNITY_ANDROID
        AndroidDisconnect();
#endif
    }
    #endregion
}
