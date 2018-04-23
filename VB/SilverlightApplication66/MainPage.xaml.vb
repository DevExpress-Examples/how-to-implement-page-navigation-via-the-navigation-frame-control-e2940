Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.NavBar

Namespace SilverlightApplication66
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
		End Sub





		' After the Frame navigates, ensure the BarButton representing the current page is checked
		Private Sub ContentFrame_Navigated(ByVal sender As Object, ByVal e As NavigationEventArgs)
			For Each group As NavBarGroup In navBarControl1.Groups
				For Each item As MyNavBarItem In group.Items
					If (item.Tag.ToString()).Equals(e.Uri.ToString()) Then
						item.Group.SelectedItem = item
					End If
				Next item
			Next group
		End Sub

		' If an error occurs during navigation, show an error window
		Private Sub ContentFrame_NavigationFailed(ByVal sender As Object, ByVal e As NavigationFailedEventArgs)
			e.Handled = True
			Dim errorWin As ChildWindow = New ErrorWindow(e.Uri)
			errorWin.Show()
		End Sub


		Private Sub NavigationPaneView_ItemSelected(ByVal sender As Object, ByVal e As NavBarItemSelectedEventArgs)
			ContentFrame.Navigate(New Uri((TryCast(e.Item, MyNavBarItem)).Tag.ToString(), UriKind.Relative))
		End Sub


		Private Sub ContentFrame_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim f As Frame = TryCast(sender, Frame)
			ContentFrame = f
		End Sub

		Private Sub navBarControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim nb As NavBarControl = TryCast(sender, NavBarControl)
			navBarControl1 = nb
		End Sub
	End Class

	Public Class MyNavBarItem
		Inherits NavBarItem


		Public Property Tag() As String
			Get
				Return CStr(GetValue(TagProperty))
			End Get
			Set(ByVal value As String)
				SetValue(TagProperty, value)
			End Set
		End Property

		' Using a DependencyProperty as the backing store for Tag.  This enables animation, styling, binding, etc...
		Public Shared ReadOnly TagProperty As DependencyProperty = DependencyProperty.Register("Tag", GetType(String), GetType(MyNavBarItem), Nothing)


	End Class
End Namespace