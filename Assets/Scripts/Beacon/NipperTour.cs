using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private bool scanning = true;
        private List<Beacon> myBeacons = new List<Beacon>();
        public PointOfInterest[] PointOfInterests;
        private List<PointOfInterest> nodeList = new List<PointOfInterest>();
        private Vector2 scrolldistance;
        public Camera mainCam;
        private bool detected = false;

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            for (int i = 0; i < PointOfInterests.Length; i++)
            {
                nodeList.Add(PointOfInterests[i].GetComponent<PointOfInterest>());
               // Debug.Log("Beacon id for poi " + nodeList[i].id + " is " + nodeList[i].beacon.m_uuid);
            }
        }
        // Use this for initialization
        void Start()
        {
            //DisplayPointOfInterest();
            iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
            iBeaconReceiver.CheckBluetoothLEStatus();
            Debug.Log("Listening for beacons");
        }

        void OnDestroy()
        {
            iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent -= OnBluetoothStateChanged;
        }

        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            //StartCoroutine(searchForDistanceOfBeacon());
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

       

        public IEnumerator searchForDistanceOfBeacon()
        {
            yield return new WaitForSeconds(1);
            foreach (Beacon b in myBeacons)
            {
                foreach(PointOfInterest poi in nodeList)
                    {
                        if (!detected)
                        {
                            poi.enableSpriteRenderer();
                            mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                            detected = true;
                        }
                    }
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
