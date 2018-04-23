using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.NavBar;

namespace SilverlightApplication66
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }



             
        
        // After the Frame navigates, ensure the BarButton representing the current page is checked
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (NavBarGroup group in navBarControl1.Groups) {
                foreach (MyNavBarItem item in group.Items)
                    if ((item.Tag.ToString()).Equals(e.Uri.ToString())) {
                        item.Group.SelectedItem = item;
                    }
            }
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }


        private void NavigationPaneView_ItemSelected(object sender, NavBarItemSelectedEventArgs e) {
            ContentFrame.Navigate(new Uri((e.Item as MyNavBarItem).Tag.ToString(), UriKind.Relative));
        }


        private void ContentFrame_Loaded(object sender, RoutedEventArgs e) {
            Frame f = sender as Frame;
            ContentFrame = f;
        }

        private void navBarControl1_Loaded(object sender, RoutedEventArgs e) {
            NavBarControl nb = sender as NavBarControl;
            navBarControl1 = nb;
        }
    }

    public class MyNavBarItem : NavBarItem {


        public string Tag {
            get { return (string)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Tag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register("Tag", typeof(string), typeof(MyNavBarItem), null);

        
    }
}