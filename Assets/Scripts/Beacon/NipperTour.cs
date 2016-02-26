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

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            for (int i = 0; i < PointOfInterests.Length; i++)
            {
                nodeList.Add(PointOfInterests[i].GetComponent<PointOfInterest>());
                Debug.Log("Beacon id for poi " + nodeList[i].id + " is " + nodeList[i].beacon.m_uuid);
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

        // Update is called once per frame
        void Update()
        {
            //StartCoroutine(searchForDistanceOfBeacon());
            foreach (Beacon b in myBeacons)
            {
               /* if (0.00 < b.accuracy && b.accuracy < 2.00)
                {
                    foreach (PointOfInterest poi in nodeList)
                    {
                        poi.enableSpriteRenderer();
                    }
                }
                if (b.accuracy > 2.00)
                {
                   
                }*/
                Debug.Log("Test: " + b.UUID);
            }
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

        /*public void DisplayPointOfInterest()
        {
            Instantiate(poiIBeacon, new Vector3(poi.x, poi.y, -7), Quaternion.identity);
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
        }*/

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

        void OnGUI()
        {
            GUIStyle labelStyle = GUI.skin.GetStyle("Label");
#if UNITY_ANDROID
            labelStyle.fontSize = 40;
#elif UNITY_IOS
		labelStyle.fontSize = 25;
#endif
            float currenty = 10;
            float labelHeight = labelStyle.CalcHeight(new GUIContent("IBeacons"), Screen.width - 20);
            GUI.Label(new Rect(currenty, 10, Screen.width - 20, labelHeight), "IBeacons");

            currenty += labelHeight;
            scrolldistance = GUI.BeginScrollView(new Rect(10, currenty, Screen.width - 20, Screen.height - currenty - 10), scrolldistance, new Rect(0, 0, Screen.width - 20, myBeacons.Count * 100));
            GUILayout.BeginVertical("box", GUILayout.Width(Screen.width - 20), GUILayout.Height(50));
            //if(guion) { GUI.Button(new Rect(10, 600, 500, 500), "Boom."); }
            foreach (Beacon b in myBeacons)
            {
                GUILayout.Label("UUID: " + b.UUID);
                GUILayout.Label("Major: " + b.major);
                GUILayout.Label("Minor: " + b.minor);
                //GUILayout.Label("Range: "+b.range.ToString());
                GUILayout.Label("Distance: " + b.accuracy);
                GUILayout.Label("Rssi: " + b.rssi);
            }
            GUILayout.EndVertical();
            GUI.EndScrollView();
        }


    }
}
