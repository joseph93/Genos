﻿using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace Assets.Scripts
{

    [ExecuteInEditMode]
    public class iBeaconServer : MonoBehaviour
    {
#if UNITY_IOS	
	[DllImport ("__Internal")]
	private static extern void InitBeaconServer(string uuid, string region, bool shouldLog, int major, int minor);
	
	[DllImport ("__Internal")]
	private static extern void Transmit(bool transmit);
#endif

        public bool m_generate;
        public string m_uuid;
        public string m_identifier;
        public int m_major;
        public int m_minor;

        private static iBeaconServer m_instance;

        public iBeaconServer(string uuid, int ma, int mi)
        {
            m_uuid = uuid;
            m_major = ma;
            m_minor = mi;
        }

        void Awake()
        {
            m_instance = this;
        }

        // Use this for initialization
        void Start()
        {
        }

        public static void Init()
        {
#if UNITY_IOS	
		InitBeaconServer(m_instance.m_uuid,m_instance.m_identifier,true,m_instance.m_major,m_instance.m_minor);	
#endif
        }

        // Update is called once per frame
        void Update()
        {
            if (Application.isEditor)
            {
                if (m_generate)
                {
                    m_generate = false;
                    m_uuid = System.Guid.NewGuid().ToString();
                }
            }
        }

        public static void Transmit()
        {
#if UNITY_IOS	
		Transmit(true);
#endif
        }

        public static void StopTransmit()
        {
#if UNITY_IOS	
		Transmit(false);
#endif
        }

        public bool Equals(Beacon a)
        {
            if (a == null)
                return false;
            return a.UUID.ToLower().Equals(m_uuid.ToLower()) && m_major == a.major && m_minor == a.minor;
        }
    }
}
