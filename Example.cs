using UnityEngine;
using PUCPR.GizmoControl;

public class Example : MonoBehaviour, IGizmoControl
{
    public GizmoDrawMode drawMode;

    public GizmoDrawMode DrawMode => drawMode;

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
