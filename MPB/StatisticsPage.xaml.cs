using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace MPB
{
    public partial class StatisticsPage : ContentPage
    {
        IFirestore firestore;
        public StatisticsPage()
        {
            InitializeComponent();
            firestore = DependencyService.Get<IFirestore>();
            getChart();

        }


        private async void getChart()
        {
            float totEarnings = await firestore.SumEarnings();
            float totOutflows = await firestore.SumOutflows();

            var entries = new[]
           {

                new Entry(totEarnings)
                {
                Label = "Entrate",
                ValueLabel = totEarnings.ToString(),
                Color = SKColor.Parse("#68B9C0")
                },
                new Entry(totOutflows)
                {
                Label = "Uscite",
                ValueLabel = totOutflows.ToString(),
                Color = SKColor.Parse("#90D585")
                }
            };

            var chart = new LineChart() { Entries = entries };

            this.LineChart.Chart = chart;

        }
    }
}
