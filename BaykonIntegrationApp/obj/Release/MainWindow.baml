   M S B A M L   `   `   `  ���� Y  UBaykon Integration Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BaykonIntegrationAppBaykonIntegrationApp  i9clr-namespace:BaykonIntegrationApp.Resources.Localization+BaykonIntegrationApp.Resources.Localization  H DCommonLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullrIclr-namespace:CommonLibrary.Resources.Localization;assembly=CommonLibrary$CommonLibrary.Resources.Localization Z=clr-namespace:CommonLibrary.Converters;assembly=CommonLibraryCommonLibrary.Converters %    BaykonIntegrationApp.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:BaykonIntegrationApp     NresLocal9clr-namespace:BaykonIntegrationApp.Resources.Localization     dresCommonLocalIclr-namespace:CommonLibrary.Resources.Localization;assembly=CommonLibrary     Rconverts=clr-namespace:CommonLibrary.Converters;assembly=CommonLibrary     :   4BaykonIntegrationApp.Resources.Localization.Language    TitleNameMainWindow � Title# Z  $	��600��$
��1100�� � WindowStyle$ SingleBorderWindow=� � WindowState$ 	Maximized=�$��Times New Roman)�$��18(� � 	Resources .  (CommonLibrary.Converters.DoubleConverter   DoubleConverter��%   &	          .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $	��100 ��� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0��$��30��.+�o� $��2q�.@�m� $��2 2 2 2q����� 3  -CommonLibrary.Resources.Localization.Language   File#��Z .@�m� -      Close#��Z  m� Icon �� $��Images\quit.png��$��15��$��15��m� $��14(�$��2 2 2 2q�$y�	Collapsed=����� 	   Service#��Z	 m� $y�	Collapsed=����� 
   Help#��Z
 � $��1��$��40��.+�c� $y�Visible=�.@��� -   $��2 2 2 2q�.����  �� Orientation$ 
Horizontal=�.+��� $��/Images/options.png��$��20��$��20��$��0 0 5 0q���    SettingCOMPort#��Z �� -    ��Name$ TBNew��$��2 2 2 2q�.���� $ 
Horizontal=�.+��� $��/Images/options.png��$��20��$��20��$��0 0 5 0q���    SettingPrinter#��Z �� -   $ TBOpen��$��2 2 2 2q�$y�	Collapsed=�.���� $ 
Horizontal=�.+��� $��Images\open.png��$��20��$��20��$��0 0 5 0q��� 
Disconnect� $��2��.+��� $��Center=�$��Center=�$��90(����� *
ScaleTxt+� $��3�� � ColumnDefinitions �� $��* ��� $
��Auto �.+�� $��5q� �� $��* ��� .+��  �� $
��125* ��� $
��264* � �� �� �� �� �� �� .+���    Date#��Z $��Right=�$��Center=�$��5q���    Nomenklatura#��Z $��1��$��Right=�$��Center=�$��5q���    Smena#��Z $��2��$��Right=�$��Center=�$��5q���    	ViewOfBox#��Z $��3��$��Right=�$��Center=�$��5q��� $��O'rtacha og'irlik��$��4��$��Right=�$��Center=�$��5q�$
��Wrap=�(  "System.Windows.Controls.DatePicker  $��1��$��Center=�$��10 0 0 0q� �� TextAlignment$ Right=�   SelectedDate �� *Date+� $��1��$��1�� �� �� $
��Auto �.+���  �� DisplayMemberPath$
 Name��$��Center=�$��10 0 0 0q����� *Nomens+ �� SelectedValue �� *
SelNomen+�� -   $��20��$��1��$��2q�
�������.���� $��/Images/add.png��� $��1��$��2�� �� �� $
��Auto �.+��� $
 Name��$��Center=�$��10 0 0 0q����� *Shifts+ �� *
SelShift+�� -   $��20��$��1��$��2q�
�������.���� $��/Images/add.png��� $��1��$��3�� �� �� $
��Auto �.+��� $
 Name��$��Center=�$��10 0 0 0q����� *PackageViews+ �� *SelPackageView+�� -   $��20��$��1��$��2q�
�������.���� $��/Images/add.png��� $��1��$��4�� �� �� $
��Auto �.+��� $��Center=�$��10 0 0 0q����� *
MeanWeight+� $��1��$��5�� �� �� $
��Auto �.+��� $(��"Standart og'irlik bo'yicha ishlash��$��Center=�$��10 0 0 0q� d� 	IsChecked �� *
IsStandart+� $��1������  �� $
��125* ��� $
��264* � �� �� �� �� �� �� .+���    Numberoffirstbox#��Z $��0��$��Right=�$��Center=�$��5q���    Weightofbox#��Z $��1��$��Right=�$��Center=�$��5q��� 
   Lot#��Z $��2��$��Right=�$��Center=�$��5q�$
��Wrap=���    Colorofconus#��Z $��3��$��Right=�$��Center=�$��5q���    Print#��Z $��4��$��Right=�$��Center=�$��5q��� $��Standart og'irlik��$��5��$��Right=�$��Center=�$��5q��� $��1��$��0��$��Center=�$��22��$��10 0 0 0q� �� TextAlignment$ Right=����� *SelLot.LastNumber+�� $��1��$��1��$��Center=�$��22��$��10 0 0 0q�$ Right=����� *SelPackageView.Weight+ �� 	Converter# [  � $��1��$��2�� �� �� $
��Auto �.+��� $
 Name��$��Center=�$��10 0 0 0q����� *Lots+ �� *SelLot+�� -   $��20��$��1��$��2q�
�������.���� $��/Images/add.png��� $��1��$��3�� �� $
��213* ��� $	��59* ��� $
��Auto �.+��� $
 Name��$��Center=�$��10,3,0,3q�$��2������ *ConeColours+ �� *SelConeColour+�� -	   $��20��$��2��$	��2,4q�
�������.���� $��/Images/add.png���� $��1��$��4��$��Center=�$��10,3,0,3q����� *
PrintRow+�� $��1��$��5��$��Center=�$��10,3,0,3q����� *StandartWeight+� $��1��.+��� .+��� -
   $	��200��$��40��$��5q�	    Ok#��Z  $��20(��� -   $	��200��$��40��$��5q�!   OkWithPrint#��Z! $��20(��� -   $	��200��$��40��$��5q�"   PrintAll#��Z" $��20(��� -   $	��200��$��40��$��5q�#��Z $��20(�� $��4��$y�	Collapsed=�.+�&   System.Windows.Controls.DataGrid  $��1��$��5,5,5,5q�#   AutoGenerateColumns# .  $   CanUserAddRows$ .  %   FrozenColumnCount$% 5������ *list+&   ColumnHeaderStyle& �� ' �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"'  .��� ( �� Property( � ��) �� Value) �� .R��� $
��Wrap=�$��Center=�$ Center=����� *   Columns* 0  *System.Windows.Controls.DataGridTextColumn  +   Number,  &System.Windows.Controls.DataGridColumn,   Header#, Z+ -   Width5	  /System.Windows.Controls.DataGridLengthConverter$- 80	 .   
IsReadOnly. . 1
  +System.Windows.Controls.DataGridBoundColumn/ 
  Binding/ �� 0 �� Path$0 Number�  1   Date#, Z1 $	- 100	 . . / �� $
0 Date�2 �� StringFormat$2 d��  3   
ViewOfBox2#, Z3 $	- 100	 . . / �� $0 ViewBox.Name�  4   Colorofconus2#, Z4 $	- 100	 . . / �� $0 ColorCone.Name�  5   NumberofBox#, Z5 $	- 120	 . . / �� $0 	NumberBox�  6   Lot2#, Z6 $	- 100	 . . / �� $0 Lot.Name�  7   
WeightNeto#, Z7 $	- 100	 . . / �� $0 WeightNetto�# [    8   	WeightBox#, Z8 $	- 100	 . . / �� $0 	WeightBox�# [    9   WeightBrutto#, Z9 $	- 100	 / �� $0 WeightBrutto�# [  � $��5��$��5 0 5 5q�$y�	Collapsed=�.+��� $��Right=�