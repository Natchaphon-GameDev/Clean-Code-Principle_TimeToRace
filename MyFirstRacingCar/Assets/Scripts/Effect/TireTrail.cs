using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public class TireTrail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer Lefttrail;
        [SerializeField] private TrailRenderer Righttrail;

        public static TireTrail Instance { get; private set; }

        private void Awake()
        {
            Debug.Assert(Lefttrail != null,"Left trail Can't be null");
            Debug.Assert(Righttrail != null,"Right trail Can't be null");

            if (Instance == null)
            {
                Instance = this;
            } 
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowTrail()
        {
            Lefttrail.emitting = true;
            Righttrail.emitting = true;
        }

        public void HideTrial()
        {
            Lefttrail.emitting = false;
            Righttrail.emitting = false;
        }
    }
}
//Please Give A to 1620701795 senPai :)