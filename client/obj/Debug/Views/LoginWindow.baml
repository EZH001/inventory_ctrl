   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:client.ViewModelsclient.ViewModels  5   P       client.Views.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .vmclr-namespace:client.ViewModels     6P     � Title$  Авторизация��5   	   $	��400��6   $	��350��6*    � WindowStartupLocation$ CenterScreen=�66    � 
ResizeMode$ NoResize=�6[    � 	Resources 5         TextBox.Static.Border �� Color  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxStyle1 �� 
TargetType �� Property �� Value-  'System.Windows.Input.KeyboardNavigation �� 
TargetType	 ��Name
 �� CornerRadius J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName �� Triggers ]� 
Conditions �� Property �� Value  TextBox.Static.Border1  TextBox.MouseOver.Border1  TextBox.Focus.Border1  PasswordBoxStyle1  FocusVisual �� StrokeDashArray 	 Button.Static.Background 
 Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  ButtonStyle1 ��Name��%�  &	        &	      &	 B     &	 e     7   7  7  &	 j    &	 �    &	 �    &	 �    7  7  7  &	     &		 �    &	
 �    &	 �    &	     &	 2    &	 S    &	 t    &	 �    &	 �    &	 �    7  7 	 7 
 7  7  7  7  7  7  7  �� 5	   
   $ Black��68   �� 5
   
   $ 	#FF7EB4EA��6;   �� 5   
   $ 	#FF569DE5��67   �� 5   
   " ��6&   .�5      ��  � ��# � ��6+   �� 5       � ��8   6,   �� 5       � ��# � ��6+   �� 5       � ��$ 1q�60   �� 5       �@ TabNavigation$
 None=�6A   �� 5       � ��$
 Left=�6;   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@J�	AllowDrop . 6*   �� 5       �@��PanningMode$ VerticalFirst=�69   �� 5       �@��IsFlicksEnabled .  67   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $	 border��6!   $
 5��61   #��z��6B   #��z��6l   #��z��6�    . ._�5      �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   5       5      P� 5       � {� .  . �5   "   �� $ border��6<    �@J�Opacity$
 0.56Q�6P   5       P� 5!       �@J�IsMouseOver . . �5"   "   �� $ border��6@   8   � ��6T   5#      P� 5$       �@J�IsKeyboardFocused . . �5%   "   �� $ border��6@   8   � ��6T   5&      5(      5*       5+      ]� 5,       5-      �� 5.      + �@��#IsInactiveSelectionHighlightEnabled . 6S   �� 5/       �@��IsSelectionActive .  6A   ./�51      ��  �@��SelectionBrush# � /�67   52      54      �� 55   
   $ Black��69   �� 56   
   $ 	#FF7EB4EA��6<   �� 57   
   $ 	#FF569DE5��68   �� 58   
   " G�6*   .�59      ��  �@G�PasswordChar$	 ●��6-   �� 5:       � ��# � ��6+   �� 5;       � ��8   6,   �� 5<       � ��# � ��6+   �� 5=       � ��$ 1q�60   �� 5>       �@ TabNavigation$
 None=�6A   �� 5?       � ��$
 Left=�6;   �� 5@       �@�FocusVisualStyle 61   Z� �� 5A       �@J�	AllowDrop . 6*   �� 5B       �@��PanningMode$ VerticalFirst=�69   �� 5C       �@��IsFlicksEnabled .  67   �� 5D       � �� 5E      �� 5F      " G�6&   .R�5G      �� $	 border��6!   $
 5��61   #��z��6B   #��z��6l   #��z��6�    . ._�5H      �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   5I       5J      P� 5K       � {� .  . �5L   "   �� $ border��6<    �@J�Opacity$
 0.56Q�6P   5M      P� 5N       �@J�IsMouseOver . . �5O   "   �� $ border��6@   8   � ��6T   5P      P� 5Q       �@J�IsKeyboardFocused . . �5R   "   �� $ border��6@   8   � ��6T   5S      5U      5W       5X      ]� 5Y       5Z      �� 5[      + �@G�#IsInactiveSelectionHighlightEnabled . 6S   �� 5\       �@G�IsSelectionActive .  6A   ./�5^      ��  �@G�SelectionBrush# � /�67   5_      5a      �� 5b   
   .�5c      ��  � �� 5d      �� 5e      .R�5f      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5g      5i      5j      �� 5k   
   $ #E40E20��6;   �� 5l   
   $ #E20E40��67   �� 5m   
   $ White��6>   �� 5n   
   $ #E20E40��6:   �� 5o   
   $ #E40E20��6<   �� 5p   
   $ #E40E20��68   �� 5q   
   $ 	#FFF4F4F4��6=   �� 5r   
   $ 	#FFADB2B5��69   �� 5s   
   $ 	#FF838383��6=   �� 5t   
   " ��6%   .�5u      ��  �@�FocusVisualStyle8   61   �� 5v       � ��8  6+   �� 5w       � ��8  6,   �� 5x       � ��# � ��6+   �� 5y       � ��$ 1q�60   �� 5z       � ��$ Center=�6;   �� 5{       � ��$ Center=�69   �� 5|       � ��$ 1q�6(   �� 5}       � �� 5~      �� 5      " ��6&   .R�5�      �� $	 border��6!   $
 5��61   #��z��6B   #��z��6l   #��z��6�    . ._�5�      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5�       5�      P� 5�       �@��IsDefaulted . . �5�   "   �� $ border��6@   # � ��6T    � ��5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   �� 5�   "   $ contentPresenter��6K    � ��
 �   �6i   5�      P� 5�       � �� . . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   5�      P� 5�       � {� .  . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   �� 5�   "   $ contentPresenter��6K   8 	  � ��6i   5�      5�      5�      5�      5�      .��5�      �  � RowDefinitions 5�   
   �� 5�      �� 5�      �� 5�      �� 5�      .+�5�   
   �� $��16(�6   $��
Логин��6"   $��Center=�6/   $	��Top=�6L   $��1��6d   �� 5�   
   $��16(�6   $��Пароль��6"   $��Center=�60   $	��Top=�6M   $��2��6e   �� 5�   
   $��Вход��6   $��Center=�6    $��Center=�6;   $��32(�6X   $��Times New Roman)�6f   �� 5�   
   -   5         ��Name$ loginTB��5�   �   #���  6   $��1��6y   $��5 0 0 0q�6�   $��16(�6�   $��Center=�6�   $��Center=�6�   $	��250��6�   ��6:   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6�   G� 5�   
   -   5         G�Name$ passTB��5�   �   #���  6   $��2��6k   $��5 0 0 0q�6x   $��16(�6�   $��Center=�6�   $��Center=�6�   $	��250��6�   �� 5�   
   -   5         ��Name$ loginBtn��5�   |   #���  6   $	��250��6�   $��16(�6�   $��40��6�   
�������$��3��6�   $��2q�6�   $��Center=�6�   $��Center=�6  $��
Войти��6   $
��Bold"�60  ��68   �� *LoginCommand+{�6Y   �� *!IsLoggingIn+60  5�      5�      