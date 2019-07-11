using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

partial class MainWindow : Window
{
    public MainWindow()
    {
        
        InitializeComponent();
        PM = new Manager(Workspace, BackupServer);
    }

    private readonly Time.Machine BackupServer = new Time.Machine();
    private readonly Manager PM;

    private void UndoButtonClick(object sender, RoutedEventArgs e) => BackupServer.Undo();

    private void RedoButtonClick(object sender, RoutedEventArgs e) => BackupServer.Redo();

    private void LineButtonClick(object sender, RoutedEventArgs e) => PM.LinesProduction();

    private void PolylineButtonClick(object sender, RoutedEventArgs e) => PM.PolylinesProduction();

    private void MoveButtonClick(object sender, RoutedEventArgs e) => PM.MovesProduction();

    private void PolygonButtonClick(object sender, RoutedEventArgs e) => PM.PolygonProduction();

    private void BezierButtonClick(object sender, RoutedEventArgs e) => PM.BezierProduction();

    private void ResizeButtonClick(object sender, RoutedEventArgs e) => PM.ResizeProduction();

    private void EllipseButtonClick(object sender, RoutedEventArgs e) => PM.EllipseProduction();

    private void RectangleButtonClick(object sender, RoutedEventArgs e)
    {
        PM.RectangleProduction();
    }
}
