using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car;
using Effect;
using Manager;
using UnityEngine;

namespace CarController
{
    public class PlayerCarController : BaseCar
    {
        [SerializeField] private float centerOfMass = -0.3f; //centerOfMass help car not easy to flip
        [SerializeField] private float maxSteeringAngle;
        [SerializeField] private float massOfCar;
        
        [SerializeField] private LayerMask airLayer;
        [SerializeField] private LayerMask raceTrackLayer;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private List<Wheels> wheels;

        private float horizontalInput;
        private float verticaInput;
        private float currentsteerAngle;
        private float currentbreakForce;
        
        private Rigidbody rigibody;

        private bool isBreaking;
        private bool turbo;
        private bool isCarOnAir;
        private bool isCarOnTrack;
        private bool rotateCar;
        private bool isCarFlipped;
        
        //Control Sound out of loop
        private bool playBreakSoundOnce;
        private bool playTurboSoundOnce;
        private bool playDriftSoundOnce; 
        
        public float CurrentCarSpeed { get; private set; }
        
        public static PlayerCarController Instance { get; private set; }
        
        public new void Init(float frontWheelMotorForce, float rearWheelMotorForce, float breakForce, float turboEngine,float trackDrag, float groundDrag, float airDrag, float downForce)
        {
            //Get base Init
            base.Init(frontWheelMotorForce, rearWheelMotorForce, breakForce, turboEngine,trackDrag,groundDrag,airDrag,downForce);
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Debug.Assert(centerOfMass <= 0 ,"centerOfMass Can't be more than the zero");
            Debug.Assert(maxSteeringAngle > 0 ,"maxSteeringAngle Can't be equal or under the zero");
            Debug.Assert(massOfCar > 0 ,"massOfCar Can't be equal or under the zero");
            Debug.Assert(airLayer != decimal.Zero,"airLayer Can't be null"); //decimal is struct
            Debug.Assert(raceTrackLayer != decimal.Zero,"raceTrackLayer Can't be null");
            Debug.Assert(groundLayer != decimal.Zero,"groundLayer Can't be null");
            Debug.Assert(groundLayer != decimal.Zero,"groundLayer Can't be null");
            Debug.Assert(wheels != null && wheels.Count != 0, "wheels clips need to setup");
        }

        private void Start()
        {
            rigibody = GetComponent<Rigidbody>();
            rigibody.mass = massOfCar; //set car mass
            rigibody.centerOfMass = new Vector3(0, centerOfMass, 0); //Set center of mess
        }

        private void Update()
        {
            GetInput();
        }
        
        private void FixedUpdate()
        {
            HandleMotor();
            HandleSteering();
            UpdateWheels();
            GroundCheck();
            RotateCar();
            Turbo();
            AddDownForce();
            TrailController();
            ControlSoundEngine();
        }

        private void ControlSoundEngine()
        {
            // * 3.6 / 80 is my magic number for decrees the acceleration to suit with the soundClip
            CurrentCarSpeed = rigibody.velocity.magnitude * 3.6f / 80;
            if (isCarOnAir)
            {
                //Slow engine sound for car in the air
                SoundManager.Instance.SlowMotionSound(SoundManager.Sound.MotorLoop);
            }
            else
            {
                SoundManager.Instance.ControlMotorSound(SoundManager.Sound.MotorLoop);
            }
        }

        protected Wheels GetWheel(WheelsPosition wheelPosition)
        {
            //Get wheels position
            foreach (var wheel in wheels)
            {
                if (wheel.WheelsPosition == wheelPosition)
                {
                    return wheel;
                }
            }
            return default(Wheels);
        }

        protected override void GetInput()
        {
            //Get input
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticaInput = Input.GetAxisRaw("Vertical");
            isBreaking = Input.GetKey(KeyCode.Space);
            turbo = Input.GetKey(KeyCode.LeftShift);
            rotateCar = Input.GetKey(KeyCode.R);
        }

        private void HandleSteering()
        {
            //Set front wheels turn 
            currentsteerAngle = maxSteeringAngle * horizontalInput;
            GetWheel(WheelsPosition.FrontLeft).WheelCollider.steerAngle = currentsteerAngle; 
            GetWheel(WheelsPosition.FrontRight).WheelCollider.steerAngle = currentsteerAngle;
        }

        private void UpdateWheels()
        {
            //Animate wheels model
            foreach (var wheel in wheels)
            {
                UpdateSingleWheel(wheel.WheelCollider, wheel.WheelModel);
            }
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            //Get position and rotation of wheel collider the world
            wheelCollider.GetWorldPose(out var position, out var rotation);
            wheelTransform.rotation = rotation;
            wheelTransform.position = position;
        }

        private void GroundCheck()
        {
            //Ground check
            RaycastHit hitRaceTrack;
            RaycastHit hitAir;
            RaycastHit hitCarFlipped;
            isCarOnTrack = Physics.Raycast(transform.position, -transform.up, out hitRaceTrack, raceTrackLayer);
            isCarOnAir = Physics.Raycast(transform.position, -transform.up, out hitAir, 15f, airLayer);
            isCarFlipped = Physics.Raycast(transform.position, transform.up, out hitCarFlipped, 5f, groundLayer);

            if (isCarOnTrack && isCarOnAir != true)
            {
                rigibody.drag = TrackDrag;
                Time.timeScale = 1;
            }
            else
            {
                rigibody.drag = GroundDrag;
                Time.timeScale = 1;
            }

            if (isCarOnAir)
            {
                rigibody.drag = AirDrag;
                TireTrail.Instance.HideTrial();//Hide Trail Render between car jump in the air
                Time.timeScale = 0.4f;
                //Slow bgm and debug some car sound during in the air
                SoundManager.Instance.SlowMotionSound(SoundManager.Sound.InGameBGM);
                SoundManager.Instance.Stop(SoundManager.Sound.CarDrift);
                SoundManager.Instance.Stop(SoundManager.Sound.Break);
                SoundManager.Instance.Stop(SoundManager.Sound.Turbo);
            }
            else
            {
                //set bgm to normal speed
                SoundManager.Instance.NormalSpeedSound(SoundManager.Sound.InGameBGM);
            }
        }
        
        private void HandleMotor()
        {
            //Apply front and rear motor force
            GetWheel(WheelsPosition.FrontLeft).WheelCollider.motorTorque = verticaInput * FrontWheelMotorForce;
            GetWheel(WheelsPosition.FrontRight).WheelCollider.motorTorque = verticaInput * FrontWheelMotorForce;
            GetWheel(WheelsPosition.RearLeft).WheelCollider.motorTorque = verticaInput * RearWheelMotorForce;
            GetWheel(WheelsPosition.RearRight).WheelCollider.motorTorque = verticaInput * RearWheelMotorForce;

            //Apply breaking system
            if (isBreaking)
            {
                //Control Sound to play 1 time
                if (playBreakSoundOnce == false)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.Break);
                }
                playBreakSoundOnce = true;
                currentbreakForce = BreakForce;
                ApplyBreaking();
            }
            else
            {
                playBreakSoundOnce = false;
                SoundManager.Instance.Stop(SoundManager.Sound.Break);
                currentbreakForce = 0f;
                ApplyBreaking();
                // ApplyBreaking();
            }
        }

        private void ApplyBreaking()
        {
            //TODO:[Drift]Implement next time [maybe this can ref the sideways function
            WheelHit ground;
            GetWheel(WheelsPosition.FrontLeft).WheelCollider.GetGroundHit(out ground);
            GetWheel(WheelsPosition.FrontRight).WheelCollider.GetGroundHit(out ground);
            GetWheel(WheelsPosition.RearLeft).WheelCollider.GetGroundHit(out ground);
            GetWheel(WheelsPosition.RearRight).WheelCollider.GetGroundHit(out ground);
            //TODO:[Drift]
            
            //Break system
            GetWheel(WheelsPosition.RearLeft).WheelCollider.brakeTorque = currentbreakForce;
            GetWheel(WheelsPosition.RearRight).WheelCollider.brakeTorque = currentbreakForce;
        }

        private void RotateCar()
        {
            //Check was car Flipped?
            if (rotateCar && isCarFlipped)
            {
                rigibody.angularVelocity = rigibody.inertiaTensor ;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Turbo()
        {
            //Turbo boost
            if (turbo)
            {
                if (playTurboSoundOnce == false)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.Turbo);
                }

                playTurboSoundOnce = true;
                rigibody.AddForce(transform.forward * TurboEngine, ForceMode.Acceleration);
            }
            else
            {
                playTurboSoundOnce = false;
            }
        }

        private void AddDownForce()
        {
            //Check RPM of frontLeftWheel to create down force
            if (GetWheel(WheelsPosition.FrontLeft).WheelCollider.rpm > 0 && isCarOnTrack)
            {
                //DownForce
                var addDownForce = Vector3.Project(rigibody.velocity, transform.forward);
                rigibody.AddForce(-transform.up * (addDownForce.magnitude * DownForce));
            }
        }

        private void TrailController()
        {
            if (horizontalInput > 0 || horizontalInput < 0 || isBreaking)
            {
                if (playDriftSoundOnce == false && isBreaking == false)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.CarDrift);
                }
                playDriftSoundOnce = true; 
                TireTrail.Instance.ShowTrail();
            }
            else
            {
                playDriftSoundOnce = false;
                SoundManager.Instance.Stop(SoundManager.Sound.CarDrift);
                TireTrail.Instance.HideTrial();
            }

            if (isCarOnAir)
            {
                TireTrail.Instance.HideTrial();
            }
        }
    }
}
//Please Give A to 1620701795 senPai :)