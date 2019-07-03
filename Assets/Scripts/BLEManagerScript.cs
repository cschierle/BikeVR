using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BLEManagerScript : MonoBehaviour, IBLEManager
{
    public float ScanDuration = 10;
    public string DeviceAddress = "B8:27:EB:5B:82:A3";
    public string ServiceUUID = "00000000-0000-4b23-9358-f235b978d07c";

    public Text OutputLog;
    public GameObject ScanningIcons;
    public GameObject ConnectingIcons;
    public GameObject SubscribingIcons;
    public Button ConnectButton;
    public Button StartButton;
    
    private List<Characteristic> _characteristics;
    private States _state;
    private float _timeout;

    enum States
    {
        Disconnected,
        Scanning,
        Connecting,
        Subscribing,
        Subscribed
    }

    private void SetState(States state, float timeout)
    {
        _state = state;
        _timeout = timeout;
    }
#if UNITY_ANDROID && !UNITY_EDITOR

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        _state = States.Disconnected;
        _timeout = 0f;
        _characteristics = new List<Characteristic>(
            new Characteristic[] { new IRCharacteristic() }
            );
        
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            //SetState(States.Scanning, 0.5f);
        }, (error) =>
        {
            Log(error);
            BluetoothLEHardwareInterface.Log("Error during initialize: " + error);
        });
    }

    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                UpdateGUI();
                switch (_state)
                {
                    case States.Disconnected:
                        {
                            Log("Disconnected");
                            break;
                        }
                    case States.Scanning:
                        {
                            Log("Scanning");

                            string[] UUIDs = { ServiceUUID };
                            BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(UUIDs, (address, name) =>
                            {
                                if (address.Equals(DeviceAddress))
                                {
                                    BluetoothLEHardwareInterface.StopScan();
                                    SetState(States.Connecting, 0.5f);
                                }
                            });
                            break;
                        }
                    case States.Connecting:
                        {
                            Log("Connecting");

                            BluetoothLEHardwareInterface.ConnectToPeripheral(DeviceAddress, null, null, (address, service, characteristicUUID) =>
                            {
                                foreach (var c in _characteristics)
                                {
                                    if (c.UUID.Equals(characteristicUUID))
                                    {
                                        c.Discovered = true;
                                    }
                                }

                                if (_characteristics.All(c => c.Discovered))
                                {
                                    SetState(States.Subscribing, 0.5f);
                                }
                            });

                            break;
                        }
                    case States.Subscribing:
                        {
                            Log("Subscribing");

                            try
                            {
                                var c = _characteristics.First(ch => !ch.Subscribed);

                                BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(DeviceAddress, ServiceUUID, c.UUID, null, (address, charUUID, bytes) =>
                                {
                                    c.UpdateValue(bytes);

                                    if (!c.Subscribed)
                                    {
                                        c.Subscribed = true;
                                        SetState(States.Subscribing, 0.5f);
                                    }
                                });
                            }
                            catch (InvalidOperationException)
                            {
                                // Subscribed to every characteristic
                                SetState(States.Subscribed, 0.1f);
                            }

                            break;
                        }
                    case States.Subscribed:
                        {
                            Log("Subscribed");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
    
    public void PerformAction()
    {
        switch (_state)
        {
            case States.Disconnected:
                {
                    ScanForDevice();
                    break;
                }
            case States.Scanning:
                {
                    CancelScanning();
                    break;
                }
            case States.Connecting:
                {
                    DisconnectFromDevice();
                    break;
                }
            case States.Subscribing:
                {
                    DisconnectFromDevice();
                    break;
                }
            case States.Subscribed:
                {
                    DisconnectFromDevice();
                    break;
                }
        }
    }

    private void ScanForDevice()
    {
        SetState(States.Scanning, 0.1f);
        StartCoroutine(ScanTimer());
    }

    private IEnumerator ScanTimer()
    {
        yield return new WaitForSeconds(ScanDuration);
        if (_state.Equals(States.Scanning))
        {
            CancelScanning();
        }
    }

    private void CancelScanning()
    {
        BluetoothLEHardwareInterface.StopScan();
        SetState(States.Disconnected, 0.1f);
    }

    private void DisconnectFromDevice()
    {
        BluetoothLEHardwareInterface.DisconnectPeripheral(DeviceAddress, (address) =>
        {
            SetState(States.Disconnected, 0.1f);
        });
    }

    void OnApplicationQuit()
    {
        BluetoothLEHardwareInterface.DeInitialize(null);
    }

    private void UpdateGUI()
    {
        switch (_state)
        {
            case States.Disconnected:
                {
                    ScanningIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ScanningIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);

                    ConnectButton.GetComponentInChildren<Text>().color = Color.green;
                    ConnectButton.GetComponentInChildren<Text>().text = "Connect";
                    StartButton.interactable = false;
                    break;
                }
            case States.Scanning:
                {
                    ScanningIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(true);
                    ScanningIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);

                    ConnectButton.GetComponentInChildren<Text>().color = Color.red;
                    ConnectButton.GetComponentInChildren<Text>().text = "Cancel";
                    StartButton.interactable = false;
                    break;
                }
            case States.Connecting:
                {
                    ScanningIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ScanningIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(true);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(true);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);

                    ConnectButton.GetComponentInChildren<Text>().color = Color.red;
                    ConnectButton.GetComponentInChildren<Text>().text = "Cancel";
                    StartButton.interactable = false;
                    break;
                }
            case States.Subscribing:
                {
                    ScanningIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ScanningIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(true);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(true);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(true);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(false);

                    ConnectButton.GetComponentInChildren<Text>().color = Color.red;
                    ConnectButton.GetComponentInChildren<Text>().text = "Cancel";
                    StartButton.interactable = false;
                    break;
                }
            case States.Subscribed:
                {
                    ScanningIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ScanningIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(true);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    ConnectingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(true);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("LoadingIcon").gameObject.SetActive(false);
                    SubscribingIcons.FindComponentInChildWithTag<Transform>("FinishedIcon").gameObject.SetActive(true);

                    ConnectButton.GetComponentInChildren<Text>().color = Color.red;
                    ConnectButton.GetComponentInChildren<Text>().text = "Disconnect";
                    StartButton.interactable = true;
                    break;
                }
        }
    }

    private void Log(string message)
    {
        if (OutputLog != null)
        {
            OutputLog.text = message;
        }
    }
#endif

    public T GetCharacteristic<T>() where T : Characteristic
    {
        return _characteristics.Find(c => c.GetType() == typeof(T)) as T;
    }
}
