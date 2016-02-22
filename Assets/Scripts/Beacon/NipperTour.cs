using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private bool scanning = true;
        private List<Beacon> myBeacons;
        public GameObject poiIBeacon;
        private PointOfInterest poi = new PointOfInterest(1, 0, -1, 1, "Test");
        // Use this for initialization
        void Start()
        {
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

        public void DisplayPointOfInterest()
        {
            GameObject nipper = Instantiate(poiIBeacon, new Vector3(poi.x, poi.y, -7), Quaternion.identity) as GameObject;
            // nipper.transform.SetParent(transform);
        }

        public void DisappearIcon()
        {
            Destroy(GameObject.FindGameObjectWithTag("Icon"));
        }

        public IEnumerator searchForDistanceOfBeacon()
        {
            yield return new WaitForSeconds(1);
            foreach (Beacon b in myBeacons)
            {
                if (0.00 < b.accuracy && b.accuracy < 2.00)
                {
                    DisplayPointOfInterest();
                }
                if (b.accuracy > 2.00)
                {
                    DisappearIcon();
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
