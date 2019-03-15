/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace dpn
{
/// Base implementation of IDpnPointer
///
/// Automatically registers pointer with DpnPointerManager.
/// Uses transform that this script is attached to as the pointer transform.
///
	public abstract class DpnBasePointer : MonoBehaviour, IDpnPointer
	{

		protected virtual void OnEnable()
		{
			DpnPointerManager.OnPointerCreated(this);
		}

        protected virtual void OnDisable()
        {
            DpnPointerManager.OnPointerDestroy(this);
        }

        /// Declare methods from IDpnPointer
        public abstract void OnInputModuleEnabled();

		public abstract void OnInputModuleDisabled();

		public abstract void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition,
			Ray intersectionRay, bool isInteractive);

		public abstract void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition,
			Ray intersectionRay, bool isInteractive);

		public abstract void OnPointerExit(GameObject targetObject);

		public abstract void OnPointerClickDown();

		public abstract void OnPointerClickUp();

		public abstract float GetMaxPointerDistance();

		public abstract void GetPointerRadius(out float innerRadius, out float outerRadius);

		public virtual Transform GetPointerTransform()
		{
			return transform;
		}

		public Vector2 GetScreenPosition()
        {
            return DpnCameraRig.WorldToScreenPoint(transform.position);
        }
	}
}
