   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:client.Viewsclient.Views  5   	       client.Views.StaffWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:client.Views     5   	     � Title$)  #Выбрать сотрудника��5   	   $	��450��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�6=    � 
ResizeMode$ NoResize=�6b    � 	Resources 5	         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1	 �� 
TargetType
 �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual1��%r  &	        &	 �     &	 �     &	 �     &	     &	 ,    &	 K    &	 l    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 &	 (    �� 5
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
   .�5D      ��  � �� 5E      �� 5F      .R�5G      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5H      5J      5K      5L      .��5N      � ���#ffffff � RowDefinitions 5O   
   �� 5P      $	��11* �6   �� 5Q      $	��33* �6   �� 5R      $��6* �6   .+�5T   
   �� $��Сотрудники��6   $��48(�6&   $
��Bold"�64   �� 5U   
   -   5         ��Name$ staffListView��5U      $��1��6+   $��16(�6~    �� SelectedItem 68   �� *SelectedStaff+��6_   �� *Staffs+ �� View 5V      �� 5W      .P�5X      �� $��Фамилия��6%    �� Width$	 255��6a    �� DisplayMemberBinding 66   �� *	last_name+6a   �� 5Y      $��Имя��6%    62   �� *
first_name+�� 5Z      $��Должность��6%    68   �� *post+5[      5]      �� 5^   
   #��� 
 6   $��2��68   $��Выбрать��6E   $
��12 8q�6W   $��Center=�6f   $��Center=�6�   $��16(�6�   ��6�   �� *	SendStaff+5_      5`      