   M S B A M L   `   `   `  ����C  ?SGOTouch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SGOTouch.UseControlSGOTouch.UseControl  5   	   )    #SGOTouch.UseControl.WinCertificados   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:SGOTouch.UseControl     5   	   -   5          � Title$  WinCertificados��5   	   $��533.978��6!   $	��839��62   $��Stretch=�6>   $
��Left=�6c    � WindowStyle$
 None=�6~    � WindowStartupLocation$ CenterScreen=�6�   .��5	      � $��0,0,0,2q�6   $
��Left=�6   $	��829��67   .+�5
   
   �� $��Hidden=�6   $��Hidden=�6>    � 	Resources 5       �� 
TargetType �� Property �� Value��%R   '��       �� 5      " ��6   .�5      ��  � ��$ 50��6.   5      5      .��5      �� -   5         ��Name$ 	sPanelOne��5      .+�5      �� -   5         ��Name$ groupBox��5      $��30(�6-   $��Certificados por Cliente��6;   $
��Left=�6]   $	��465��6x   $��	10,35,0,0q�6�   $	��Top=�6�   $	��809��6�   .��5      �� -   5        	 ��Name$	 listView��5      
 �� SelectionMode$
 Multiple=�61   $
��Left=�6J   $	��275��6e   $��	30,55,0,0q�6r   $	��Top=�6�   $	��740��6�    �� View 5      �� 5      .P�5   "   ��  �� Width$ 0��6j    �� DisplayMemberBinding 63   ��  �� Path$ IDCERTIFICADOVSP�5   #   �� 5   "   $��Certificado��61    6F   �� $ DESCRIPCION��� 5   "   $��Cosecha��61    6B   �� $ COSECHA��� 5   "   $
��Tope��61    6?   �� $
 TOPE��� 5   "   $��	Operacion��61   $ 0��6t    6D   �� $ 	OPERACION�6t   �� 5   "   $��Limite��61   $ 0��6n    6A   �� $ LIMITE�6n   �� 5   "   $��Saldo��61    6@   �� $ SALDO_CALCULADO�5      5      5       5"      5#      �� 5$   
   -   5         ��Name$ label_Copy1��5$      $��30(�6   $��Saldo :��63   $
��Left=�6E   $��
45,419,0,0q�6`   $	��Top=�6t   $��45��6�   $	��110��6�    J� RenderTransformOrigin$ 0.533,0&�6�   �� 5%   
   -   5        $ lblSaldo��5%      $��30(�6   $��0��60   $
��Left=�6<   $��180,423,0,0q�6W   $	��Top=�6l   $��52��6�   $	��325��6�   $ 0.533,0&�6�   �� 5&   
   -   5         ��Name$ btnSalir��5&      $��30(�6#   $��Salir��61   $
��Left=�6A   $��640,423,0,0q�6\   $	��Top=�6q   $	��130��6�   $��41��6�   6�   �� 5(   
   -   5         ��Name$ chkSelecAll��5(      $��30(�6(   $��Seleccionar Todo��66   $
��Left=�6Q   $��48��6l   $��
525,68,0,0q�6x   $	��Top=�6�   $	��260��6�   6�   5*      5+      