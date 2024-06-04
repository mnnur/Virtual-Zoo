using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffmapBoundary : MonoBehaviour
{
    [SerializeField] GameObject preventOffmap;
    [SerializeField] float boundaryMaxX;
    [SerializeField] float boundaryMinX;
    [SerializeField] float boundaryMaxY;
    [SerializeField] float boundaryMinY;
    [SerializeField] float boundaryMaxZ;
    [SerializeField] float boundaryMinZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (
            preventOffmap.transform.position.x > boundaryMaxX
            || preventOffmap.transform.position.x < boundaryMinY
            || preventOffmap.transform.position.y > boundaryMaxY
            || preventOffmap.transform.position.y < boundaryMinY
            || preventOffmap.transform.position.z > boundaryMaxZ
            || preventOffmap.transform.position.z < boundaryMinZ
            )
        {
            preventOffmap.transform.position = new Vector3(0, 0.1f, 1);
        }
    }
}
