using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineFreeLook freeLookCam;

    // Getting an Instance of Main Shared RCC Settings.
    #region RCC Settings Instance

    private RCC_Settings RCCSettingsInstance;
    private RCC_Settings RCCSettings
    {
        get
        {
            if (RCCSettingsInstance == null)
            {
                RCCSettingsInstance = RCC_Settings.Instance;
                return RCCSettingsInstance;
            }
            return RCCSettingsInstance;
        }
    }

    #endregion


    internal float orbitX = 0f;
    public float orbitXSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
    freeLookCam.m_XAxis.Value += ControlFreak2.CF2Input.GetAxis(RCCSettings.mouseXInput) * orbitXSpeed * .02f;
    }
}
