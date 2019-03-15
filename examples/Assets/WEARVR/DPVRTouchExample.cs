using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dpn;

[RequireComponent(typeof(MeshRenderer))]
public class DPVRTouchExample : MonoBehaviour
{
	private const float COOLDOWN_TIME = 2.0f;

	private Color originalColor;
	private MeshRenderer meshRenderer;

	private float coolDown;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		originalColor = meshRenderer.material.color;
	}

	public void Update()
	{
		//Poll input from the DPVR manager
		bool swipeUp = DpnDaydreamController.TouchGestureUp;
		bool otherSwipeDirection = DpnDaydreamController.TouchGestureLeft || DpnDaydreamController.TouchGestureRight || DpnDaydreamController.TouchGestureDown;

		coolDown += Time.deltaTime;

		if (swipeUp || otherSwipeDirection)
		{
			coolDown = 0.0f;

			if (otherSwipeDirection)
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
			if (coolDown > COOLDOWN_TIME)
			{
				DeActivate();
			}
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
