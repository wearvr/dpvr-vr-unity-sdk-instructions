using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dpn;

[RequireComponent(typeof(MeshRenderer))]
public class DPVRInputExample : MonoBehaviour
{
	private Color originalColor;
	private MeshRenderer meshRenderer;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		originalColor = meshRenderer.material.color;
	}

	public void Update()
	{
		//Poll input from the DPVR manager
		bool triggerDown = Input.GetMouseButton(0) || DpnDaydreamController.TriggerButton || Input.GetKey(KeyCode.Joystick1Button0);
		bool otherButtonDown = DpnDaydreamController.BackButton || DpnDaydreamController.ClickButton;

		if(otherButtonDown || triggerDown)
		{
			if(otherButtonDown)
			{
				ActiveFalse();
			}
			else
			{
				Activate();
			}
		}
		else
		{
			DeActivate();
		}
	}

	private void Activate()
	{
		meshRenderer.material.color = Color.green;
	}

	private void ActiveFalse()
	{
		meshRenderer.material.color = Color.red;
	}

	private void DeActivate()
	{
		meshRenderer.material.color = originalColor;
	}
}
