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

        public void changeSceneToFloor()
        {
            if (bluetoothOn)
            {
                SceneManager.LoadScene("F2");
                Vibration.Vibrate(100);
            }

            else
            {
                modalWindow.Choice("Your bluetooth is turned off and this application uses bluetooth. Would you like to turn it on?", iconImage, yesAction, noAction);
            }
        }
    }
}
