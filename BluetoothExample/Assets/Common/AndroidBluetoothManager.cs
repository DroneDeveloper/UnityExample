using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

/**
 *
 * @brief This function is designed to control the android bluetooth function
 * @details Control Android native plugin.
 * @author Thomas Hyunjun Kim, ggkids9211@gmail.com
 * @date 2017-08-07
 * @version 0.0.1
 * @todo Create bluetooth control function @n
    * Check Action function about is that work well in multiple connection function.
 * @see
 * UPDATE LOG @n
    * 170815 [UpdateValues1()] -> cope with 6 protocol environment.
    * 170815 [UpdateValues()] -> cope with 6 protocol environment.
    * 170808 [SendData()] -> created @n
    * 170808 [CallBluetoothData()] -> Build multiple receive threading @n
    * 170807 [UpdateValues1()] -> created @n
    * 170807 [UpdateValues()] -> created @n
    * 170807 [SearchDevice()] -> created @n
    * 170807 [ErrorMessage()] -> created @n
    * 170807 [CallBluetoothData()] -> created @n
    * 170807 [CallConnectedDevice1()] -> created @n
    * 170807 [CallConnectedDevice()] -> created @n
    * 170807 [CallBluetoothStop()] -> created @n
    * 170807 [CallBluetoothInit()] -> created @n
    * 170807 [AndroidBluetoothManager()] -> created @n
    * Copyright MG solutions all right reserved.
 * @warning This script is not running in other OS except Android. Use only Android environment.
 */
public class AndroidBluetoothManager {
    string TAG = "[AndroidBluetoothManager.cs]";
    public Action<AndroidBluetoothManager, int> onButtonClick;
    public Action<AndroidBluetoothManager, int> onButtonRelease;
    //Thread[] receivingThread = new Thread[2]; ///< use for receive threading.
    //bool isReceiving; ///< Check is Receiving or not.
    public static string notification;
#if UNITY_ANDROID
    AndroidJavaObject _activity; ///< Get android java class.
#endif

    /**
     * @brief Class initializing @n
     * @detail Get Device from Device,cs @n
        * Call Android java class from plugin @n
        * Make Static the Android java class 
     * @param AndroidJavaObject _activity
     * @param Device devices
     * 
     */
    public AndroidBluetoothManager()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
            Debug.Log(TAG + " AndroidBluetoothManager() -> _activity 생성");
        }
        catch (Exception e)
        {
            notification = e.ToString(); //UPDATE BY HJ -> To check state
        }
#endif
    }

    /**
     * @brief Call bluetooth initializing. @n
     * @detail Get method from android native plugin
     * 
     */
    public void CallBluetoothInit()
    {
#if UNITY_ANDROID
        try { 
            _activity.Call("BluetoothInit");
            Debug.Log(TAG + " CallBluetoothInit() -> Initializing Bluetooth");
            notification = "블루투스 초기화 중입니다.";
        }
        catch (Exception e)
        {
            notification = e.ToString(); //UPDATE BY HJ -> To check state
        }
#endif
    }

    /**
    * @brief Stop bluetooth threading. @n
    * @detail Call stop function in the android native threading.
    * 
    */
    public void CallBluetoothStop()
    {
#if UNITY_ANDROID
        try
        {
            _activity.Call("StopThread");
            Debug.Log(TAG + " CallBluetoothStop() -> Stop");
            notification = "블루투스 기능이 중지되었습니다.";
        }
        catch (Exception e)
        {
            notification = e.ToString(); //UPDATE BY HJ -> To check state
        }
#endif
    }

    /**
    * @brief Call first device connecting function. @n
    * @detail Get method from android native plugin
    * @throws Exception Connection Error 
    */
    public void CallConnectedDevice(string device1)
    {
#if UNITY_ANDROID
        try
        {
            _activity.Call("ConnectedFirstDevice",device1);
            Debug.Log(TAG + " CallConnectedDevice() -> Connection");
            notification = "Arm Band와 연결을 시도중입니다.";
        }
        catch(Exception e){
            Debug.Log(TAG + " CallConnectedDevice() -> Error"+e.ToString());
            notification = e.ToString(); //UPDATE BY HJ -> To check state
        }
#endif
    }

    /**
    * @brief Get both devices received values. @n
    * @detail Using multiple threading, receive data from devices separately.
    * @todo Check Threading connect what is safely connected.
    * @see 170728 Try -> unity threading test fail @n
    */
    public void CallBluetoothData()  //UPDATE BY HJ 170808
    {
#if UNITY_ANDROID
        try
        {
            UpdateValues(_activity.Call<String>("SendFirstData"));
        }
        catch (Exception e)
        {
            notification = e.ToString(); //UPDATE BY HJ -> To check state
        }
#endif
        /*
        #if UNITY_ANDROID
        isReceiving = true;

        receivingThread[0] = new Thread(() => {
            while (isReceiving)
            {
                UpdateValues(_activity.Call<String>("SendFirstData"));
            }
        });
        receivingThread[0].Start();

        receivingThread[1] = new Thread(() => {
            while (isReceiving)
            {
                UpdateValues(_activity.Call<String>("SendSecondData"));
            }
        });
        receivingThread[1].Start();
        #endif
        */
    }

    /**
    * @brief Stop receive threading.
    * @param bool isReceiving.
    * @param Thread[] receivingThread.
    * @see stop using.
    */
    /*
    public void StopReceiving()
    {
        //isReceiving = false;
        for (int i = 0; i < 2; i++)
        {
            if (receivingThread[i] != null)
            {
                //readThread.Join ();
                receivingThread[i].Abort();
                receivingThread[i] = null;
            }
        }
    }
    */
    /**
    * @brief Get error message. @n
    * @todo Fix in native what UnitySendMessage function.
    * @see 170807 Not working right now because of UnitySendMessage functions @n
    */
    public void ErrorMessage(string errorMsg)
    {
        string msg = errorMsg;
        Debug.Log(TAG + " ErrorMessage() -> "+msg);
    }

    string deviceList;
    /**
    * @brief Get device list. @n
    * @param deviceList
    * @todo Fix in native what UnitySendMessage function.
    * @see 170807 Not working right now because of UnitySendMessage functions @n
    */
    public void SearchDevice(string devices)
    {
        string[] device = devices.Split(',');
        for (int i = 0; i < devices.Length; i++)
        {
            deviceList += device[i];
            Debug.Log(TAG + " SearchDevice() -> " + device[i]);
        }

    }

    /**
    * @brief Get device received values.
    */
    public void UpdateValues(string rawData)
    {
        string[] splitted = rawData.Split(';');
        Device.data = Convert.ToInt32(splitted[0]);
    }
    

    /**
    * @brief Send data to target device
    * @todo Build target device sending function.
    * @see HOW TO USE @n
    * ex)SendData(data,0) -> send 'data' to first device. @n
    * ex)SendData(data,1) -> send 'data' to second device.
    */
    public void SendData(string str, int device)
    {
        switch (device)
        {
            case 0:
                //Send data to first device
                break;
            case 1:
                //Send data to second device
                break;
        }
    }

    /**
    * @brief Initializing cryptogram @n
    * @details Call cryptogram from android native plugin.
    */
    public void CryptogramInit()
    {
#if UNITY_ANDROID
        try
        {
            _activity.Call("CryptogramInit");
        }catch(Exception e){
            notification = e.ToString(); //UPDATE BY HJ -> To check state
        }
#endif
    }

    /**
    * @brief Cryptogram the target data @n
    * @details Call cryptogram function from android native plugin.
    */
    public string CallCryptogram(string data)
    {
        string result = "";
#if UNITY_ANDROID
        result = _activity.Call<String>("SendCryptogram", data);
#endif
        return result;
    }

}

