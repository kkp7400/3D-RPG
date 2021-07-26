using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public bool startDissolve = false;

    public float speed = .5f;

    private void Start(){
        meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private float t = 0.0f;
    private void Update()
    {
        //if (startDissolve)
        {
            Material[] mats = meshRenderer.materials;

            float maxAlpha = mats[0].GetFloat("_Cutoff");

            mats[0].SetFloat("_Cutoff", Mathf.Sin(t * speed));
            t += Time.deltaTime;

            // Unity does not allow meshRenderer.materials[0]...
            meshRenderer.materials = mats;
            //if (mats[0].GetFloat("_Cutoff") >= 0f)
            //{
            //    mats[0].SetFloat("_Cutoff", maxAlpha);
            //    startDissolve = false;
            //} 
        }

    }
}
