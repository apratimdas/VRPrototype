using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZEffects;

public class gunController : MonoBehaviour {

    public GameObject controllerRight;

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    //private SteamVR_TrackedController controller;

    public EffectTracer TracerEffect;
    public Transform muzzleTransform;

    public bool toggle;

    public GameObject leftPortal;
    public GameObject rightPortal;
    public GripManager manager;

	// Use this for initialization
	void Start () {
        //controller = controllerRight.GetComponent<SteamVR_TrackedController>();
        //controller.TriggerClicked += TriggerPressed;
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
	}

    private void TriggerPressed(object sender, ClickedEventArgs e)
    {
        if(manager.weapon)
            ShootWeapon();
    }

    public void ShootWeapon()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(muzzleTransform.position, muzzleTransform.forward);

        device = SteamVR_Controller.Input((int)trackedObj.index);
        device.TriggerHapticPulse(750);
        TracerEffect.ShowTracerEffect(muzzleTransform.position, muzzleTransform.forward, 250f);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag=="portal wall")
            {
                toggle = !toggle;

                Quaternion hitObjRotation = Quaternion.LookRotation(hit.normal);
                if (toggle)
                {
                    leftPortal.transform.position = hit.point;
                    leftPortal.transform.rotation = hitObjRotation;
                }
                else
                {
                    rightPortal.transform.position = hit.point;
                    rightPortal.transform.rotation = hitObjRotation;
                }

            }
        }
    }

	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            ShootWeapon();
    }
}
