using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class iBeaconTest : MonoBehaviour {

    private List<Beacon> mybeacons = new List<Beacon>();
    private Vector2 scrolldistance;
    private bool beaconFound = false;
    // Use this for initialization
    void Start ()
    {
        iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void searchIBeacon()
    {
        iBeaconReceiver.Init();
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
        scrolldistance = GUI.BeginScrollView(new Rect(10, currenty, Screen.width - 20, Screen.height - currenty - 10), scrolldistance, new Rect(0, 0, Screen.width - 20, mybeacons.Count * 100));
        GUILayout.BeginVertical("box", GUILayout.Width(Screen.width - 20), GUILayout.Height(50));
        foreach (Beacon b in mybeacons)
        {
            GUILayout.Label("UUID: " + b.UUID);
            GUILayout.Label("Major: " + b.major);
            GUILayout.Label("Minor: " + b.minor);
            GUILayout.Label("Range: " + b.range.ToString());
            GUILayout.Label("Rssi: " + b.rssi);
        }
        GUILayout.EndVertical();
        GUI.EndScrollView();
    }

    private void OnBeaconRangeChanged(List<Beacon> beacons)
    { // 
        foreach (Beacon b in beacons)
        {
            if (mybeacons.Contains(b))
            {
                mybeacons[mybeacons.IndexOf(b)] = b;
                beaconFound = true;
            }
            else {
                // this beacon was not in the list before
                // this would be the place where the BeaconArrivedEvent would have been spawned in the the earlier versions
                mybeacons.Add(b);
                beaconFound = true;
            }
        }
        foreach (Beacon b in mybeacons)
        {
            if (b.lastSeen.AddSeconds(10) < DateTime.Now)
            {
                // we delete the beacon if it was last seen more than 10 seconds ago
                // this would be the place where the BeaconOutOfRangeEvent would have been spawned in the earlier versions
                mybeacons.Remove(b);
            }
        }
    }
}
