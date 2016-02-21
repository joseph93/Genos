using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private bool scanning = true;
        private PointOfInterest poi = new PointOfInterest(1, 0, -1, 1, "Test");
        // Use this for initialization
        void Start()
        {
            iBeaconReceiver.BeaconRangeChangedEvent += poi.OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
            iBeaconReceiver.CheckBluetoothLEStatus();
            Debug.Log("Listening for beacons");
        }

        void OnDestroy()
        {
            iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent -= OnBluetoothStateChanged;
        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(searchForDistanceOfBeacon());
        }

        

     

        private void OnBluetoothStateChanged(BluetoothLowEnergyState newstate)
        {
            switch (newstate)
            {
                case BluetoothLowEnergyState.POWERED_ON:
                    iBeaconReceiver.Init();
                    Debug.Log("It is on, go searching");
                    break;
                case BluetoothLowEnergyState.POWERED_OFF:
                    //iBeaconReceiver.EnableBluetooth();
                    Debug.Log("It is off, switch it on");
                    break;
                case BluetoothLowEnergyState.UNAUTHORIZED:
                    Debug.Log("User doesn't want this app to use Bluetooth, too bad");
                    break;
                case BluetoothLowEnergyState.UNSUPPORTED:
                    Debug.Log("This device doesn't support Bluetooth Low Energy, we should inform the user");
                    break;
                case BluetoothLowEnergyState.UNKNOWN:
                case BluetoothLowEnergyState.RESETTING:
                default:
                    Debug.Log("Nothing to do at the moment");
                    break;
            }
        }

        
        
    }
}
