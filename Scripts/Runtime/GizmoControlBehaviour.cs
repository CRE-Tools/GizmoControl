using UnityEngine;

namespace PUCPR.GizmoControl
{
    public abstract class GizmoControlBehaviour : MonoBehaviour, IGizmoControl
    {
        public abstract GizmoDrawMode DrawMode { get; }
        public abstract void DrawGizmos();

        protected virtual void OnEnable() => GizmoControlCache.Register(this);
        protected virtual void OnDisable() => GizmoControlCache.Unregister(this);
        protected virtual void OnDestroy() => GizmoControlCache.Unregister(this);
    }

}
