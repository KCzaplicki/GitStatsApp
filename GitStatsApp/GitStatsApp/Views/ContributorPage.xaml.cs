using GitStatsApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GitStatsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContributorPage : ContentPage
    {
        public ContributorPage(ContributorDto contributor)
        {
            InitializeComponent();
        }
    }
}
