using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using VNC;

namespace ExploreReDimPerformance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        struct payload
        {
            Int32 a;
            Int32 b;
            Int32 c;
            Int32 d;
            Int32 e;
            Int32 f;
            Int32 g;
            Int32 h;
            Int32 i;
            Int32 j;
            Int32 k;
            Int32 l;
            Int32 m;
            Int64 n;
            Int64 o;
            Int64 p;
            Int64 q;
            Int64 r;
            Int64 s;
            Int64 t;
            Int64 u;
            Int64 v;
            Int64 w;
            Int64 x;
            Int64 y;
            Int64 z;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnArray_Click(object sender, RoutedEventArgs e)
        {
            btnArray.IsEnabled = false;
            btnList.IsEnabled = false;

            int iterations = Int32.Parse(txtIterations.Text);
            txtTime.Text = "?";

            Stopwatch stopWatch = Stopwatch.StartNew();

            Int32[] arrayOfThings = new Int32[1];

            unsafe
            {             
                int count = arrayOfThings.Count();

                for (int i = 0; i < iterations; i++)
                {
                    fixed (Int32* arrayOfInt32Address = &arrayOfThings[0])
                    {
                    }

                    Array.Resize(ref arrayOfThings, i + 1);
                    arrayOfThings[i] = i;
                }

                count = arrayOfThings.Count();
            }
            long duration = stopWatch.ElapsedMilliseconds;

            txtTime.Text = duration.ToString();


            btnArray.IsEnabled = true;
            btnList.IsEnabled = true;
        }

        private void btnArrayStruct_Click(object sender, RoutedEventArgs e)
        {
            btnArray.IsEnabled = false;
            btnList.IsEnabled = false;

            int iterations = Int32.Parse(txtIterations.Text);
            txtTime.Text = "?";

            Stopwatch stopWatch = Stopwatch.StartNew();

            payload[] arrayOfThings = new payload[1];

            unsafe
            {
                int count = arrayOfThings.Count();

                for (int i = 0; i < iterations; i++)
                {
                    fixed (payload* arrayOfInt32Address = &arrayOfThings[0])
                    {
                    }

                    Array.Resize(ref arrayOfThings, i + 1);
                    arrayOfThings[i] = new payload();
                }

                count = arrayOfThings.Count();
            }
            long duration = stopWatch.ElapsedMilliseconds;

            txtTime.Text = duration.ToString();


            btnArray.IsEnabled = true;
            btnList.IsEnabled = true;
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            btnArray.IsEnabled = false;
            btnList.IsEnabled = false;

            int iterations = Int32.Parse(txtIterations.Text);
            txtTime.Text = "?";

            Stopwatch stopWatch = Stopwatch.StartNew();

            List<Int32> listOfInt32 = new List<Int32>();

            int count = listOfInt32.Count();

            for (int i = 0; i < iterations; i++)
            {
                listOfInt32.Add(i);
            }

            count = listOfInt32.Count();

            long duration = stopWatch.ElapsedMilliseconds;

            txtTime.Text = duration.ToString();

            btnArray.IsEnabled = true;
            btnList.IsEnabled = true;
        }

        private void btnListStruct_Click(object sender, RoutedEventArgs e)
        {
            btnArray.IsEnabled = false;
            btnList.IsEnabled = false;

            int iterations = Int32.Parse(txtIterations.Text);
            txtTime.Text = "?";

            Stopwatch stopWatch = Stopwatch.StartNew();

            List<payload> listOfPayload = new List<payload>();

            int count = listOfPayload.Count();

            for (int i = 0; i < iterations; i++)
            {
                listOfPayload.Add(new payload());
            }

            count = listOfPayload.Count();

            long duration = stopWatch.ElapsedMilliseconds;

            txtTime.Text = duration.ToString();

            btnArray.IsEnabled = true;
            btnList.IsEnabled = true;

            listOfPayload = null;
        }
    }
}
