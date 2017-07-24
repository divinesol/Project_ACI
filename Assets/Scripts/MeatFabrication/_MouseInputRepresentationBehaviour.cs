using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class _MouseInputRepresentationBehaviour : MonoBehaviour
{

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Foreground";
    }

    Vector2 mouseStart;
    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit != false && hit.collider != null)
        {
            if (hit.collider.tag == "MeatFabrication")
            {
                if(MeatFabManager.Instance.numOfCuts > 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }

                    if (Input.GetMouseButton(0))
                    {
                        lineRenderer.enabled = true;
                        Vector2 mouseEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        lineRenderer.SetPosition(0, mouseStart);
                        lineRenderer.SetPosition(1, mouseEnd);
                    }
                    else
                    {
                        lineRenderer.enabled = false;
                    }
                }
               
            } 
        }

        
    }

}
