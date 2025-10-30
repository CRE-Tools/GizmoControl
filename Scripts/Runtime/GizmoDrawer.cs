#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;


namespace PUCPR.GizmoControl
{
    public static class GizmoDrawer
    {
        static GizmoDrawer() => SceneView.duringSceneGui += _ => CleanUpNulls();

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        static void DrawAllGizmos(IGizmoControl _, GizmoType gizmoType)
        {
            foreach (var client in GizmoControlCache.Clients)
            {
                if (client == null) continue;

                switch (client.DrawMode)
                {
                    case GizmoDrawMode.Always:
                        client.DrawGizmos();
                        break;
                    case GizmoDrawMode.SelectedOnly:
                        if ((gizmoType & GizmoType.Selected) != 0)
                            client.DrawGizmos();
                        break;
                    case GizmoDrawMode.NotSelected:
                        if ((gizmoType & GizmoType.NotInSelectionHierarchy) != 0)
                            client.DrawGizmos();
                        break;
                }
            }
        }

        private static void CleanUpNulls()
        {
            var toRemove = new List<IGizmoControl>();
            foreach (var client in GizmoControlCache.Clients)
            {
                if (client == null) toRemove.Add(client);
            }

            foreach (var client in toRemove)
            {
                GizmoControlCache.Unregister(client);
            }
        }
    }
}
#endif
