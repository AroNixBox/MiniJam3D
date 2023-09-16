using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CatView))]
public class CatViewGizmo : Editor
{
    private void OnSceneGUI()
    {
        CatView view = (CatView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(view.transform.position, Vector3.up, Vector3.forward, 360f, view.radius);

        Vector3 viewAngle01 = DirectionFromAngle(view.transform.eulerAngles.y, -view.range / 2);
        Vector3 viewAngle02 = DirectionFromAngle(view.transform.eulerAngles.y, view.range / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(view.transform.position, view.transform.position + viewAngle01 * view.radius);
        Handles.DrawLine(view.transform.position, view.transform.position + viewAngle02 * view.radius);

        if (view.InView)
        {
            Handles.color = Color.green;
            Handles.DrawLine(view.transform.position, view.Mouse.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
