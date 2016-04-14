using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    
    public class BluetoothEnable : MonoBehaviour
    {
        private ModalWindow modalWindow;

        public Sprite iconImage;
        private UnityAction yesAction;
        private UnityAction noAction;
        
        private bool bluetoothOn;

        void Awake()
        {
            modalWindow = ModalWindow.Instance();
            yesAction = new UnityAction(iBeaconReceiver.EnableBluetooth);
            noAction = new UnityAction(modalWindow.closePanel);

        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            iBeaconReceiver.BluetoothStateChangedEvent += CheckForBluetooth;
            iBeaconReceiver.CheckBluetoothLEStatus();
        }

        void OnDestroy()
        {
            iBeaconReceiver.BluetoothStateChangedEvent -= CheckForBluetooth;
        }

        public void CheckForBluetooth(BluetoothLowEnergyState newstate)
        {
            switch (newstate)
            {
                case BluetoothLowEnergyState.POWERED_ON:
                    bluetoothOn = true;
                    break;

                case BluetoothLowEnergyState.POWERED_OFF:
                    bluetoothOn = false;
                    break;
            }
        }

        public void loadFreeRoaming()
        {
            if (bluetoothOn)
            {
                SceneManager.LoadScene("FreeRoaming");
                Vibration.Vibrate(100);
            }

            else
            {
                modalWindow.Choice("Your bluetooth is turned off and this application uses bluetooth. Would you like to turn it on?", iconImage, yesAction, noAction);
            }
        }

        public void loadStoryline(int slID)
        {
            if (bluetoothOn)
            {
                if (slID == 0)
                {
                    SceneManager.LoadScene("Overview0");    //change to Storyline0, this is for testing purposes.
                    Vibration.Vibrate(100);
                    PlayerPrefs.SetInt("storylineID", slID);
                }
                if (slID == 4)
                {
                    SceneManager.LoadScene("Storyline4");    //change to Storyline4, this is for testing purposes.
                    Vibration.Vibrate(100);
                    PlayerPrefs.SetInt("storylineID", slID);
                }
                
            }

            else
            {
                modalWindow.Choice("Your bluetooth is turned off and this application uses bluetooth. Would you like to turn it on?", iconImage, yesAction, noAction);
            }
        }
    }
}
