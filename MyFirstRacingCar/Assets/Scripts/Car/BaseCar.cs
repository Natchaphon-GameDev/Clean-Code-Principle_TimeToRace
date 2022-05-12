using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Car
{
    public abstract class BaseCar : MonoBehaviour
    {
        protected float FrontWheelMotorForce { get; private set; }
        protected float RearWheelMotorForce { get; private set; }
        protected float BreakForce { get; set; }
        protected float TurboEngine { get; private set; }
        protected float TrackDrag { get; private set; }
        protected float GroundDrag { get; private set; }
        protected float AirDrag { get; private set; }
        protected float DownForce { get; private set; }
        
        
        protected enum WheelsPosition
        {
            FrontLeft,
            FrontRight,
            RearLeft,
            RearRight
        }

        [Serializable] protected struct Wheels
        {
            public Transform WheelModel;
            public WheelCollider WheelCollider;
            public WheelsPosition WheelsPosition;
            // [Range(0, 3)] public float Sttffness;
            // public float Spring;
            // public float Damper;
        }

        protected void Init(float frontWheelMotorForce, float rearWheelMotorForce, float breakForce, float turboEngine ,float trackDrag, float groundDrag, float airDrag, float downForce)
        {
            //Set init of car
            FrontWheelMotorForce = frontWheelMotorForce;
            RearWheelMotorForce = rearWheelMotorForce;
            BreakForce = breakForce;
            TurboEngine = turboEngine;
            TrackDrag = trackDrag;
            GroundDrag = groundDrag;
            AirDrag = airDrag;
            DownForce = downForce;
        }

        //Let Child Class Implement Input
        protected abstract void GetInput();
    }
}
//Please Give A to 1620701795 senPai :)

