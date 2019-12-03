using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
 
namespace WpfApp1
{
    /// <summary>
    /// UCCircle.xaml 的交互逻辑
    /// </summary>
    public partial class UCCircle : UserControl
    {
        public static readonly DependencyProperty DrawingCountProperty = DependencyProperty.Register("DrawingCount", typeof(int), typeof(UCCircle), new PropertyMetadata(DrawingCountOnValueChanged));
        public int DrawingCount
        {
            get { return (int)GetValue(DrawingCountProperty); }
            set { SetValue(DrawingCountProperty, value); }
        }
        private const double angle = 360;
        private const double rotateAngle = 3.6;//每片弧的角度
        private double r = 0;//半径
        private double width = 0;
        private double height = 0;
        private Point circleCenter = new Point();//圆心
        private List<Path> pathList = new List<Path>();//弧线集合
        private int strokeThickness = 50;

        public UCCircle()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        { 
            Init();
            //Drawing(100);
        }
        public void Init()
        {
            width = this.ActualWidth;
            height = this.ActualHeight;
            circleCenter.X = width / 2;
            circleCenter.Y = height / 2;
            r = (Math.Min(height, width) / 2) - strokeThickness;//半径赋值
            try
            { 
                Point? prePoint = null; 
                for (double i = rotateAngle; i <= angle + 1; i = i + rotateAngle)
                { 
                    var drawingAngle = i;
                    if (drawingAngle >= angle)//处理最后一个点的图像
                    {
                        drawingAngle = 359.99;
                    }
                    Path path = new Path();

                    path.StrokeThickness = strokeThickness;

                    if (drawingAngle < 90)//大于90 就是大于25%
                    {
                        path.Stroke = new SolidColorBrush(Colors.Green);
                    }
                    else if (drawingAngle < 270)//小于270 就是小于75%
                    {
                        path.Stroke = new SolidColorBrush(Colors.Yellow);
                    }
                    else
                    {
                        path.Stroke = new SolidColorBrush(Colors.Red);
                    }


                    var pg = new PathGeometry();
                    var pf = new PathFigure();
                    if (prePoint == null)
                    {
                        pf.StartPoint = new Point(circleCenter.X, circleCenter.Y - r);
                        prePoint = pf.StartPoint;
                    }
                    else
                    {
                        pf.StartPoint = prePoint.Value;
                    }

                    ArcSegment ags = new ArcSegment();
                    ags.Size = new Size(r, r);//设置X轴和Y轴半径 就是字符串的前两个参数
                    double endPointX = Math.Sin(Math.PI * drawingAngle / 180.0) * r;//Math.Sin(Math.PI * drawingAngle / 180.0)这样才能获取真正的Sin值
                    double endPointY = Math.Cos(Math.PI * drawingAngle / 180.0) * r;
                    ags.Point = new Point(circleCenter.X + endPointX, circleCenter.Y - endPointY);//设置弧的终结点就是 字符串的最后俩参数
                    prePoint = ags.Point;
                    ags.SweepDirection = SweepDirection.Clockwise;//正向绘制圆 字符串的第五个参数
                    pf.Segments.Add(ags);

                    pg.Figures.Add(pf);
                    path.Data = pg;
                    pathList.Add(path); 
                    //< Path Width = "100" Stroke = "Black" StrokeThickness = "1"  Data = "M 50,0 A 50,50,0,0,1,100,50" >
                }
               
            }
            catch (Exception ex)
            {

            }
        }
        public void Drawing(int maxCount)
        {

            if (maxCount > 100)
            {
                maxCount = 100;
            }
            if (pathList.Count >= maxCount)
            {
                Clear();
                var list = pathList.Take(maxCount);
                foreach (var item in list)
                {
                    gdCon.Children.Add(item);
                }
            } 
        }

        public void Clear()
        {
            gdCon.Children.Clear();
        }

        private static void DrawingCountOnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                UCCircle owner = sender as UCCircle; 
                if(owner.IsLoaded)
                {
                    int maxCount = (int)e.NewValue; 
                    owner.Drawing(maxCount);
                } 
            }
            // 当只发生改变时回调的方法
        }
    }
}
