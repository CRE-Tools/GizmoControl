#if UNITY_EDITOR
using UnityEditor;
#endif


namespace PUCPR.GizmoControl
{
    public static class GizmoDrawer
    {
#if UNITY_EDITOR
        [DrawGizmo(GizmoType.NonSelected)]
        static void DrawNotSelected(IGizmoControl client, GizmoType gizmoType) =>
            DrawForClient(client, GizmoDrawMode.NotSelected);

        [DrawGizmo(GizmoType.Selected)]
        static void DrawSelected(IGizmoControl client, GizmoType gizmoType) =>
            DrawForClient(client, GizmoDrawMode.SelectedOnly);

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        static void DrawActive(IGizmoControl client, GizmoType gizmoType) =>
            DrawForClient(client, GizmoDrawMode.Always);
        static void DrawForClient(IGizmoControl client, GizmoDrawMode drawMode)
        {
            if (client.DrawMode != drawMode) return;

            client.DrawGizmos();
        }
#endif
    }
}
