using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**
 *
 * @brief Device class is a c# implementation of moti-rehab arm band.
 * @details Device object contains essential sensor data and button click/release callbacks.
 * @author Suhyeon Lee, +82)10-4611-6169
 * @date 2017-01-01
 * @version 0.0.1
 * @todo Add Android native plugin.
 * @bug -
 * @see UPDATE LOG @n
    * 171227 [SendMessage()] -> created to use send message to devicve. @n
    * Copyright MG solutions all right reserved.
 * @ref DeviceManager
 * 
 */
public class Device : MonoBehaviour {
    public static int data;
    public static bool isConnect = false;
}