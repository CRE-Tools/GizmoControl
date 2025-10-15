namespace PUCPR.GizmoControl
{
    public interface IGizmoControl
    {
        GizmoDrawMode DrawMode { get; }
        void DrawGizmos();
    }
}
