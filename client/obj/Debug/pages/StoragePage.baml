   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:client.pagesclient.pages  5          client.pages.StoragePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:client.pages     5        N� Title$  StoragePage��5	       � 	Resources 5
         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1 �� 
TargetType �� 
TargetType	 ��Name
 �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual1��%r  &	        &	 �     &	 �     &	 �     &	     &	 ,    &	 K    &	 l    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 &	 (    �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5      5      5      �� 5   
   $ White��6;   �� 5   
   $ #e40e20��67   �� 5   
   $ #e40e20��6>   �� 5   
   $ #e40e20��6:   �� 5   
   $ white��6<   �� 5   
   $ #e40e20��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5        � ��8  6,   �� 5!       � ��# � ��6+   �� 5"       � ��$ 1q�60   �� 5#       � ��$ Center=�6;   �� 5$       � ��$ Center=�69   �� 5%       � ��$ 1q�6(   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� $	 border��6!   $
 5��61   #��z��6B   #��z��6l   $��2q�6�    . ._�5*      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5+       5,      P� 5-       �@��IsDefaulted . . �5.   "   �� $ border��6@   # � ��6T    � ��5/      P� 50       �@J�IsMouseOver . . �51   "   �� $ border��6?   8   � ��6S   �� 52   "   $ border��6@   8   � ��6T   �� 53   "   $ contentPresenter��6K    � ��
 �����6i   54      P� 55       � �� . . �56   "   �� $ border��6?   8   � ��6S   �� 57   "   $ border��6@   8   � ��6T   �� 58   "   $ contentPresenter��6K    � ��
 �   �6i   59      P� 5:       � {� .  . �5;   "   �� $ border��6?   8   � ��6S   �� 5<   "   $ border��6@   8   � ��6T   �� 5=   "   $ contentPresenter��6K   8 	  � ��6i   5>      5@      5B      5C      �� 5D   
   .�5E      ��  � �� 5F      �� 5G      .R�5H      �� $��2q�6$   $	 1 2R�6/   #��� ��6E    . $��1��6�   5I      5K      5L      5M      .��5O      � ���#ffffff � RowDefinitions 5P   
   �� 5Q      $	��11* �6   �� 5R      $	��33* �6   �� 5S      $��6* �6   .+�5U   
   �� $!��Места хранения��6   $��48(�6*   $
��Bold"�68   �� 5V   
   -   5         ��Name$ storagesListView��5V      $��1��6.   $��16(�6�    �� SelectedItem 6;   �� *SelectedStorage+��6d   �� *
Storages+ �� View 5W      �� 5X      .P�5Y      �� $$��Стеллаж и ячейка��6%    �� Width$	 150��6f    �� DisplayMemberBinding 6?   �� *Title+6f   �� 5Z      $4��.Категория места хранения��6%    6G   �� *Category.Title+�� 5[      $��Действия��6%   $	 200��67    �� CellTemplate 5\      �� 5]      .R�5^   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5_   &   �� $��Изменить��6-   $	��5,0q�6@   ��6M   �� **(DataContext.OpenEditStorageWindowCommand+ �� RelativeSource ��  �� AncestorType$ ListViewL���6�   �� �� 5`   &   $��Удалить��6-   $	��5,0q�6?   ��6L   �� *" DataContext.DeleteStorageCommand+ �� $ ListViewL���6�   �� 5a   #   5b      5d      5e      5g      �� 5h   
   #��� 
 6   $��2��68   $��Добавить��6E   $
��12 8q�6X   $��Center=�6g   $��Center=�6�   $��16(�6�   ��6�   �� *OpenAddStorageWindowCommand+5i      5j      