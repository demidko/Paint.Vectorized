using System.Windows.Controls;
using Time;
using DrawingCollege;
using System;

class Manager
{
    private readonly Canvas Workspace;
    private readonly Machine BackupServer;
    private Expert Slave;

    private void Enable<T>() where T : Expert, new()
    {
        Slave?.Disable();
        Slave = new T();
        Slave.Enable(Workspace, BackupServer);
    }

    public void MovesProduction() => Enable<MoveExpert>();

    public void ResizeProduction() => Enable<ResizeExpert>();

    public void LinesProduction() => Enable<LineExpert>();

    public void PolylinesProduction() => Enable<PolylineExpert>();

    public void PolygonProduction() => Enable<PolygonExpert>();

    public Manager(in Canvas workspace, in Machine backupServer) => 
        (Workspace, BackupServer) = (workspace, backupServer);

    public void BezierProduction() => Enable<BezierExpert>();

    internal void EllipseProduction() => Enable<EllipseExpert>();

    internal void RectangleProduction() => Enable<RectangleExpert>();
}
