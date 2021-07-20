using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaCamera : MonoBehaviour
{
    //Renderer ObstacleRenderer;
    //public GameObject Character;
    //private void Start()
    //{
    //    Character = GameObject.FindGameObjectWithTag("Player");
    //}
    //void Update()
    //{

    //    float Distance = Vector3.Distance(transform.position, Character.transform.position);

    //    Vector3 Direction = (Character.transform.position - transform.position).normalized;

    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position, Direction, out hit, Distance))

    //    {

    //        // 2.맞았으면 Renderer를 얻어온다.
    //        ObstacleRenderer = hit.transform.GetComponentInChildren<Renderer>();

    //        if (ObstacleRenderer != null)

    //        {
    //            // 3. Metrial의 Aplha를 바꾼다.

    //            Material Mat = ObstacleRenderer.material;

    //            Color matColor = Mat.color;

    //            matColor.a = 0.5f;

    //            Mat.color = matColor;

    //        }

    //    }
    //}
}
