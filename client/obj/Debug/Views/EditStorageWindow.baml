   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:client.Viewsclient.Views  5   	   $    client.Views.EditStorageWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:client.Views     5   	     � Title$G  AОкно редактирования места хранения��5   	   $	��350��64   $	��600��6A    � WindowStartupLocation$ CenterScreen=�6M    � 
ResizeMode$ NoResize=�6r    � 	Resources 5	         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1	 �� 
TargetType
 �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual1  TextBox.Static.Border  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxStyle1-  'System.Windows.Input.KeyboardNavigation ��Name �� Triggers ]� 
Conditions �� Property �� Value��%  &	        &	 �     &	 �     &	 �     &	     &	 ,    &	 K    &	 l    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 &	 (    &	 �    &	 �    &	     &	 7    7  7  7  �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5      5      5      �� 5   
   $ White��6;   �� 5   
   $ #e40e20��67   �� 5   
   $ #e40e20��6>   �� 5   
   $ #e40e20��6:   �� 5   
   $ white��6<   �� 5   
   $ #e40e20��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   "	 ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5       � ��8  6,   �� 5        � ��# � ��6+   �� 5!       � ��$ 1q�60   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�69   �� 5$       � ��$ 1q�6(   �� 5%       � �� 5&      �� 5'      "
 ��6&   .R�5(      �� $ border��6!   $ 5��61   #��z��6B   #��z��6l   $��2q�6�    . ._�5)      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5*       5+      P� 5,       �@��IsDefaulted . . �5-   "   �� $ border��6@   # � ��6T    � ��5.      P� 5/       �@J�IsMouseOver . . �50   "   �� $ border��6?   8   � ��6S   �� 51   "   $ border��6@   8   � ��6T   �� 52   "   $ contentPresenter��6K    � ��
 �����6i   53      P� 54       � �� . . �55   "   �� $ border��6?   8   � ��6S   �� 56   "   $ border��6@   8   � ��6T   �� 57   "   $ contentPresenter��6K    � ��
 �   �6i   58      P� 59       � {� .  . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8 	  � ��6i   5=      5?      5A      5B      �� 5C   
   .�5D      ��  � �� 5E      �� 5F      .R�5G      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5H      5J      5K      �� 5L   
   $ Black��68   �� 5M   
   $ 	#FF7EB4EA��6;   �� 5N   
   $ 	#FF569DE5��67   �� 5O   
   "	 ��6&   .�5P      ��  � ��# � ��6+   �� 5Q       � ��8   6,   �� 5R       � ��# � ��6+   �� 5S       � ��$ 1q�60   �� 5T       �@ TabNavigation$
 None=�6A   �� 5U       � ��$
 Left=�6;   �� 5V       �@�FocusVisualStyle 61   Z� �� 5W       �@J�	AllowDrop . 6*   �� 5X       �@��PanningMode$ VerticalFirst=�69   �� 5Y       �@��IsFlicksEnabled .  67   �� 5Z       � �� 5[      �� 5\      "
 ��6&   .R�5]      �� $ border��6!   $ 5��61   #��z��6B   #��z��6l   #��z��6�    . ._�5^      �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   5_       5`      P� 5a       � {� .  . �5b   "   �� $ border��6<    �@J�Opacity$
 0.56Q�6P   5c      P� 5d       �@J�IsMouseOver . . �5e   "   �� $ border��6@   8   � ��6T   5f      P� 5g       �@J�IsKeyboardFocused . . �5h   "   �� $ border��6@   8   � ��6T   5i      5k      5m       5n      ]� 5o       5p      �� 5q      + �@��#IsInactiveSelectionHighlightEnabled . 6S   �� 5r       �@��IsSelectionActive .  6A   ./�5t      ��  �@��SelectionBrush# � /�67   5u      5w      5x      .��5y      �  � ColumnDefinitions 5z   
   �� 5{      �� 5|       � RowDefinitions 5~   
   �� 5      �� 5�      .+�5�   
   �� $��16(�6   $$��Стеллаж и ячейка��6"   $��Center=�6:   $	��Top=�6W   $
��0 40q�6o   �� 5�   
   $��16(�6   $��Категория��6"   $��1��63   $��Center=�6C   $	��Top=�6`   $
��0 40q�6x   �� 5�   
   -   5         ��Name$ storageTitleTB��5�   :   #���  6    �� 	MaxLength$ 5��6R   $��Center=�6`   $��Center=�6{   $��16(�6�   $��70��6�   ��6�   �� *Storage.Title+ �� UpdateSourceTrigger$ PropertyChanged=��� 5�   
   $��1��6    �� DisplayMemberPath$ Title��6#    �� SelectedValuePath$ Id��6=   $��Center=�6T   $��Center=�6o   $��16(�6�   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�    �� SelectedIndex$ 0��6�     �� SelectedValue  6�   �� *Storage.category_id+��6�   �� *DataContext.Categories+! �� RelativeSource! �� " �� AncestorType$" WindowL��� 5�   
   #��� 
 6   $��1��68   $
��12 8q�6E   $��2��6T   $��Сохранить��6h   $��Center=�6|   $��Center=�6�   $��16(�6�   ��6�   �� *
SaveEdit+5�      5�      