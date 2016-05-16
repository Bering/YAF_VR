﻿using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(F4VR_Controller))]
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class F4VR_ControllerEvents : MonoBehaviour
{
	public bool isTriggerClicked = false;
	public bool isGripped = false;
	public bool isMenuPressed = false;
	public bool isPadClicked = false;
	public bool isPadTouched = false;


	[System.Serializable] public class VR_ControllerEvents_EventClass : UnityEvent<F4VR_Controller> {}
	public VR_ControllerEvents_EventClass TriggerClicked;
	public VR_ControllerEvents_EventClass TriggerUnclicked;
	public VR_ControllerEvents_EventClass Gripped;
	public VR_ControllerEvents_EventClass Ungripped;
	public VR_ControllerEvents_EventClass MenuButtonClicked;
	public VR_ControllerEvents_EventClass MenuButtonUnclicked;
	public VR_ControllerEvents_EventClass PadClicked;
	public VR_ControllerEvents_EventClass PadUnclicked;
	public VR_ControllerEvents_EventClass PadTouched;
	public VR_ControllerEvents_EventClass PadUntouched;


	private SteamVR_Controller.Device controller_SteamVR;
	private F4VR_Controller controller;


	void Start()
	{
		// TODO: Is it true that index can change at runtime?
		SteamVR_TrackedObject.EIndex controllerIndex = GetComponent<SteamVR_TrackedObject> ().index;
		if (controllerIndex == 0)
		{
			Debug.LogError ("F4VR_ControllerEvents doesn't watch for events on the HMD... Use F4VR_HmdEvents instead.");
			Destroy (this);
			return;
		}

		controller_SteamVR = SteamVR_Controller.Input((int)controllerIndex);

		controller = GetComponent<F4VR_Controller> ();
	}


	void Update()
	{
		bool currentlyPressed;

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.Trigger);
		if (currentlyPressed && !isTriggerClicked) {
			isTriggerClicked = true;
			TriggerClicked.Invoke (controller);
		}
		else if(!currentlyPressed && isTriggerClicked) {
			isTriggerClicked = false;
			TriggerUnclicked.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.Grip);
		if (currentlyPressed && !isGripped) {
			isGripped = true;
			Gripped.Invoke (controller);
		}
		else if(!currentlyPressed && isGripped) {
			isGripped = false;
			Ungripped.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.ApplicationMenu);
		if (currentlyPressed && !isMenuPressed) {
			isMenuPressed = true;
			MenuButtonClicked.Invoke (controller);
		}
		else if(!currentlyPressed && isMenuPressed) {
			isMenuPressed = false;
			MenuButtonUnclicked.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.Touchpad);
		if (currentlyPressed && !isPadClicked) {
			isPadClicked = true;
			PadClicked.Invoke (controller);
		}
		else if(!currentlyPressed && isPadClicked) {
			isPadClicked = false;
			PadUnclicked.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetTouch (SteamVR_Controller.ButtonMask.Touchpad);
		if (currentlyPressed && !isPadTouched) {
			isPadTouched= true;
			PadTouched.Invoke (controller);
		}
		else if(!currentlyPressed && isPadTouched) {
			isPadTouched= false;
			PadUntouched.Invoke (controller);
		}

	}


}
