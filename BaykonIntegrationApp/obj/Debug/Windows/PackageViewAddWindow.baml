   M S B A M L   `   `   `  ����Y  UBaykon Integration Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:BaykonIntegrationApp.WindowsBaykonIntegrationApp.Windows  5   	   H DCommonLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullZ=clr-namespace:CommonLibrary.Converters;assembly=CommonLibraryCommonLibrary.Converters 5   	   7    1BaykonIntegrationApp.Windows.PackageViewAddWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:BaykonIntegrationApp.Windows     5   	   Rconverts=clr-namespace:CommonLibrary.Converters;assembly=CommonLibrary     5   	     � Title$  Ma'lumotlarni kiriting��5	   	   $	��230��6(   $	��300��65    � 	Resources 5      .  (CommonLibrary.Converters.DoubleConverter   DoubleConverter��%   &	          5   
   6#   5      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Right=�6   $��Nomi: ��60   $��Center=�6>   �� 5   
   $��1��6(   $��4q�68   ��6   �� *Name+68   �� 5   
   $��1��6   $��Right=�6!   $��Center=�6>   
Og'irligi:6f   �� 5   
   $��1��6   $��1��6"   $��4q�6s   ��6/   �� *Weight+ �� 	Converter# [  6s   �� 5!   
   $��2��6   $��Right=�6!   $��Center=�6>   Turi:6`   �� 5"   
   -   5        $��cmbItems��5"      $��1��6#   $��2��63   $��4q�6@    �� SelectedIndex 6K   �� *Type+�� 5$   
   $��3��6   $��Right=�6!   $��Center=�6>   Etiketkadagi qiymati:6p   �� 5%   
   $��1��6   $��3��6"   $��4q�6F   ��6/   �� *Value+6F   �� 5'   
   -   5        $��Cancel��5'   (   $��4��69   $	��100��6F   $��Center=�6R   $��Center=�6o   $��25��6�   $��2q�6�   �� 5(   
   -   5        $��Ok��5(   &   $��4��63   $��1��6@   $	��100��6P   $��Center=�6\   $��Center=�6y   $��25��6�   $��2q�6�   5)      5*      