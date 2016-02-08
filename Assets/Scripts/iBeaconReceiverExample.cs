using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class iBeaconReceiverExample : MonoBehaviour {
	private Vector2 scrolldistance;
	private List<Beacon> mybeacons = new List<Beacon>();
	private bool scanning = true;
    private bool guion = false;
    public GameObject Nipper;
	// Use this for initialization
	void Start () {
        //CreateNipper();
        iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
		iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
		iBeaconReceiver.CheckBluetoothLEStatus();
		Debug.Log ("Listening for beacons");
	}
	
	void OnDestroy() {
		iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
		iBeaconReceiver.BluetoothStateChangedEvent -= OnBluetoothStateChanged;
	}
	// Update is called once per frame
	void Update () {
	    foreach (Beacon b in mybeacons)
	    {
	        if (0.00 < b.accuracy && b.accuracy < 2.00)
	        { 
                CreateNipper();
	            //guion = true;
	            //SceneManager.LoadScene("BeaconDetected");
	        }
	    }
	
	}

    public void CreateNipper()
    {
        GameObject nipper = Instantiate(Nipper, new Vector3(702, -77, 15), Quaternion.identity) as GameObject;
        nipper.transform.SetParent(transform);
    }

    private void OnBluetoothStateChanged(BluetoothLowEnergyState newstate) {
		switch (newstate) {
		case BluetoothLowEnergyState.POWERED_ON:
			iBeaconReceiver.Init();
			Debug.Log ("It is on, go searching");
			break;
		case BluetoothLowEnergyState.POWERED_OFF:
			//iBeaconReceiver.EnableBluetooth();
			Debug.Log ("It is off, switch it on");
			break;
		case BluetoothLowEnergyState.UNAUTHORIZED:
			Debug.Log("User doesn't want this app to use Bluetooth, too bad");
			break;
		case BluetoothLowEnergyState.UNSUPPORTED:
			Debug.Log ("This device doesn't support Bluetooth Low Energy, we should inform the user");
			break;
		case BluetoothLowEnergyState.UNKNOWN:
		case BluetoothLowEnergyState.RESETTING:
		default:
			Debug.Log ("Nothing to do at the moment");
			break;
		}
	}

	private void OnBeaconRangeChanged(List<Beacon> beacons) { // 
		foreach (Beacon b in beacons) {
			if (mybeacons.Contains(b)) {
				mybeacons[mybeacons.IndexOf(b)] = b;
			} else {
				// this beacon was not in the list before
				// this would be the place where the BeaconArrivedEvent would have been spawned in the the earlier versions
				mybeacons.Add(b);
			}
		}
		foreach (Beacon b in mybeacons) {
			if (b.lastSeen.AddSeconds(10) < DateTime.Now) {
				// we delete the beacon if it was last seen more than 10 seconds ago
				// this would be the place where the BeaconOutOfRangeEvent would have been spawned in the earlier versions
				mybeacons.Remove(b);
			}
		}
	}
	
	/*void OnGUI() {
		GUIStyle labelStyle = GUI.skin.GetStyle("Label");
#if UNITY_ANDROID
		labelStyle.fontSize = 40;
#elif UNITY_IOS
		labelStyle.fontSize = 25;
#endif
		float currenty = 10;
		float labelHeight = labelStyle.CalcHeight(new GUIContent("IBeacons"), Screen.width-20);
		GUI.Label(new Rect(currenty,10,Screen.width-20,labelHeight),"IBeacons");
		
		currenty += labelHeight;
		scrolldistance = GUI.BeginScrollView(new Rect(10,currenty,Screen.width -20, Screen.height - currenty - 10),scrolldistance,new Rect(0,0,Screen.width - 20,mybeacons.Count*100));
		GUILayout.BeginVertical("box",GUILayout.Width(Screen.width-20),GUILayout.Height(50));
        //if(guion) { GUI.Button(new Rect(10, 600, 500, 500), "Boom."); }
		foreach (Beacon b in mybeacons) {
			GUILayout.Label("UUID: "+b.UUID);
			GUILayout.Label("Major: "+b.major);
			GUILayout.Label("Minor: "+b.minor);
			//GUILayout.Label("Range: "+b.range.ToString());
            GUILayout.Label("Distance: "+b.accuracy);
			GUILayout.Label("Rssi: "+b.rssi);
		}
		GUILayout.EndVertical();
		GUI.EndScrollView();
	}*/
}
