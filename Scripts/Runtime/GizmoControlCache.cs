using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace PUCPR.GizmoControl
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class GizmoControlCache
    {
        private static readonly HashSet<IGizmoControl> _clients = new();

        public static IEnumerable<IGizmoControl> Clients => _clients;

        public static void Register(IGizmoControl client)
        {
            if (client != null)
                _clients.Add(client);
        }

        public static void Unregister(IGizmoControl client)
        {
            if (client != null)
                _clients.Remove(client);
        }

#if UNITY_EDITOR
        static GizmoControlCache()
        {
            EditorApplication.playModeStateChanged += _ => Rebuild();
            EditorApplication.hierarchyChanged += Rebuild;
        }

        public static void Rebuild()
        {
            _clients.Clear();
            foreach (var control in Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                if (control is IGizmoControl gizmo)
                    _clients.Add(gizmo);
        }
#endif

    }
}
