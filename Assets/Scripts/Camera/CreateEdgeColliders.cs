using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEdgeColliders : MonoBehaviour
{

    public LayerMask PlayerLayer;

    void Awake()
    {
        var cam = Camera.main;

        var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0,0,cam.nearClipPlane));
        var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,0,cam.nearClipPlane));
        var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0,cam.pixelHeight,cam.nearClipPlane));
        var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,cam.nearClipPlane));

        var edge = GetComponent<EdgeCollider2D>();
        //var edgePoints = new Vector2[5] {bottomLeft, topLeft, topRight, bottomRight};

        Vector2[] points = edge.points;

        points.SetValue(bottomLeft, 0);
        points.SetValue(topLeft, 1);
        points.SetValue(topRight, 2);
        points.SetValue(bottomRight, 3);
        points.SetValue(bottomLeft, 4);

        edge.points = points;
    }

}
