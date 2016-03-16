using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class iBeaconHandler : MonoBehaviour
    {
        private List<Beacon> myBeacons;

        public iBeaconHandler()
        {
        }

        void Awake()
        {
            myBeacons = new List<Beacon>();
            initiliazeIBeacon();
        }

        void Start()
        {
               
        }

        void OnDestroy()
        {
            destroyBeacons();
        }

        public List<Beacon> getBeacons()
        {
            return myBeacons;
        } 

        public void initiliazeIBeacon()
        {
            iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
            iBeaconReceiver.CheckBluetoothLEStatus();
            Debug.Log("Listening for beacons");
        }

        public void destroyBeacons()
        {
            iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent -= OnBluetoothStateChanged;
            print("Beacons were destroyed");
        }

        public void OnBluetoothStateChanged(BluetoothLowEnergyState newstate)
        {
            switch (newstate)
            {
                case BluetoothLowEnergyState.POWERED_ON:
                    iBeaconReceiver.Init();
                    Debug.Log("It is on, go searching");
                    break;
                case BluetoothLowEnergyState.POWERED_OFF:
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

        private void OnBeaconRangeChanged(List<Beacon> beacons)
        {
            // 
            foreach (Beacon b in beacons)
            {
                if (myBeacons.Contains(b))
                {
                    myBeacons[myBeacons.IndexOf(b)] = b;
                }
                else
                {
                    // this beacon was not in the list before
                    // this would be the place where the BeaconArrivedEvent would have been spawned in the the earlier versions
                    myBeacons.Add(b);
                }
            }
            foreach (Beacon b in myBeacons)
            {
                if (b.lastSeen.AddSeconds(10) < DateTime.Now)
                {
                    // we delete the beacon if it was last seen more than 10 seconds ago
                    // this would be the place where the BeaconOutOfRangeEvent would have been spawned in the earlier versions
                    myBeacons.Remove(b);
                }
            }
        }
    }
}
