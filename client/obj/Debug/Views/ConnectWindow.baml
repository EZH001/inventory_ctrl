   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:clientclient  5   	        client.Views.ConnectWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:client     5   	     � Title$%  Окно подключения��5   	   $	��150��6"   $	��400��6/    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5         TextBox.Static.Border �� Color  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxStyle1 �� 
TargetType �� Property �� Value-  'System.Windows.Input.KeyboardNavigation �� 
TargetType	 ��Name
 �� CornerRadius J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName �� Triggers ]� 
Conditions �� Property �� Value  FocusVisual �� StrokeDashArray  Button.Static.Background  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border 	 Button.Pressed.Background 
 Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground ��Name��%[  &	        &	      &	 B     &	 e     7   7  7  &	 j    &	     &	 3    &	 T    &	 u    &		 �    &	
 �    &	 �    &	 �    &	     '�� ?    7  7  7  7  7  7 	 7 
 7  7  7  �� 5   
   $ Black��68   �� 5   
   $ 	#FF7EB4EA��6;   �� 5   
   $ 	#FF569DE5��67   �� 5   
   " ��6&   .�5      ��  � ��# � ��6+   �� 5       � ��8   6,   �� 5       � ��# � ��6+   �� 5       � ��$ 1q�60   �� 5       �@ TabNavigation$
 None=�6A   �� 5       � ��$
 Left=�6;   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@J�	AllowDrop . 6*   �� 5       �@��PanningMode$ VerticalFirst=�69   �� 5       �@��IsFlicksEnabled .  67   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $	 border��6!   $
 5��61   #��z��6B   #��z��6l   #��z��6�    . ._�5      �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   5        5!      P� 5"       � {� .  . �5#   "   �� $ border��6<    �@J�Opacity$
 0.56Q�6P   5$      P� 5%       �@J�IsMouseOver . . �5&   "   �� $ border��6@   8   � ��6T   5'      P� 5(       �@J�IsKeyboardFocused . . �5)   "   �� $ border��6@   8   � ��6T   5*      5,      5.       5/      ]� 50       51      �� 52      + �@��#IsInactiveSelectionHighlightEnabled . 6S   �� 53       �@��IsSelectionActive .  6A   ./�55      ��  �@��SelectionBrush# � /�67   56      58      �� 59   
   .�5:      ��  � �� 5;      �� 5<      .R�5=      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5>      5@      5A      �� 5B   
   $ White��6;   �� 5C   
   $ #E20E40��67   �� 5D   
   $ #E20E40��6>   �� 5E   
   $ #E20E40��6:   �� 5F   
   $ White��6<   �� 5G   
   $ #E40E20��68   �� 5H   
   $ 	#FFF4F4F4��6=   �� 5I   
   $ 	#FFADB2B5��69   �� 5J   
   $ 	#FF838383��6=   �� 5K   
   " ��6   .�5L      ��  �@�FocusVisualStyle8   61   �� 5M       � ��8  6+   �� 5N       � ��8  6,   �� 5O       � ��# � ��6+   �� 5P       � ��$ 1q�60   �� 5Q       � ��$ Center=�6;   �� 5R       � ��$ Center=�69   �� 5S       � ��$ 1q�6(   �� 5T       � �� 5U      �� 5V      " ��6&   .R�5W      �� $	 border��6!   $
 5��62   #��z��6C   #��z��6m   #��z��6�    . ._�5X      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5Y       5Z      P� 5[       �@��IsDefaulted . . �5\   "   �� $ border��6@   # � ��6T    � ��5]      P� 5^       �@J�IsMouseOver . . �5_   "   �� $ border��6?   8   � ��6S   �� 5`   "   $ border��6@   8   � ��6T   �� 5a   "   $ contentPresenter��6K    � ��
 �����6i   5b      P� 5c       � �� . . �5d   "   �� $ border��6?   8   � ��6S   �� 5e   "   $ border��6@   8   � ��6T   �� 5f   "   $ contentPresenter��6K    � ��
 �   �6i   5g      P� 5h       � {� .  . �5i   "   �� $ border��6?   8   � ��6S   �� 5j   "   $ border��6@   8   � ��6T   �� 5k   "   $ contentPresenter��6K   8 	  � ��6i   5l      5n      5p      5q      5r      .��5t      �  � RowDefinitions 5u   
   �� 5v      �� 5w      .+�5y   
   �� -   5         ��Name$ 	connectTB��5y   L   #���  6   $��5 0 0 0q�6:   $��20 0 0 0q�6_   $��16(�6q   $
��Left=�6   $��Center=�6�   $	��250��6�   �� 5z   
   -   5         ��Name$ exitBtn��5z      $��1��6"   $��2q�6/   $
��Left=�6C   $��Center=�6^   $��Отмена��6y   $��20 0 0 0q�6�   $��3q�6�   6�   �� 5{   
   -   5        $ 
connectBtn��5{      $��1��6%   $��2q�62   $��Right=�6F   $��Center=�6b   $��Подключиться��6}   $��0 0 20 0q�6�   $��3q�6�   6�   5|      5}      