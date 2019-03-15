/************************************************************************************

Copyright   :   Copyright 2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/
using UnityEngine;
using System.Collections;

namespace dpn
{
    [RequireComponent(typeof(Renderer))]
    public class ControllerRay : MonoBehaviour
    {
        // Private members
        private Material materialComp;

        // Use this for initialization
        void Start()
        {
            CreateRayVertices();

            transform.localPosition = new Vector3(0,0,0);

            materialComp = gameObject.GetComponent<Renderer>().material;
            materialComp.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.15f));
            materialComp.renderQueue = 4000;
        }

        private void CreateRayVertices()
        {
            Mesh mesh = new Mesh();
            gameObject.AddComponent<MeshFilter>();
            GetComponent<MeshFilter>().mesh = mesh;

            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(-0.001f, 0, 0);
            vertices[1] = new Vector3(-0.001f, 0, -1);
            vertices[2] = new Vector3(0.001f, 0, -1);
            vertices[3] = new Vector3(0.001f, 0, 0);

            int indices_count = 3 * 2;
            int[] indices = new int[indices_count];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            mesh.vertices = vertices;
            mesh.triangles = indices;
            mesh.RecalculateBounds();
            ;
        }

        // Update is called once per frame
        void Update()
        {
            if (DpnPointerManager.Pointer == null)
                return;

            Vector3 Direction = transform.position - DpnPointerManager.Pointer.GetPointerTransform().position;
            transform.rotation = Quaternion.LookRotation(Direction.normalized, transform.up);

            float length = Direction.magnitude;
            length -= 0.07f;
            if (length < 0)
            {
                length = 0;
            }
            materialComp.SetFloat("_Length", length);
        }
    }
}
