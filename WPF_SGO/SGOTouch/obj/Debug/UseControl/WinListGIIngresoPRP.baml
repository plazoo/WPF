   M S B A M L   `   `   `  ����C  ?SGOTouch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SGOTouch.UseControlSGOTouch.UseControl  5   	   -    'SGOTouch.UseControl.WinListGIIngresoPRP   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:SGOTouch.UseControl     5   	     � Title$  WinListGIIngresoPRP��5   	   $	��499��6%   $
��1084��62    � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�6U   ��.  � WindowStyle$
 None=�6�   .��5	      � $��0,0,2,2q�6   .+�5
   
   �� $��Hidden=�6   $��Hidden=�6>    � 	Resources 5       �� 
TargetType �� Property �� Value��%y   '��       '� H     �� 5      " ��6   .�5      ��  � ��$ 50��6.   5      �� 5      " �6   64   5      .��5      �� -   5         ��Name$ 	sPanelOne��5      $
��1072��6,   $	��474��69   .+�5      �� -   5        	 ��Name$	 cboOfOrigen��5   C   
 �� Text$
  ��6    �� SelectedIndex$ 2��6#   $��30(�65   $
��Left=�6X   $��
185,40,0,0q�6s   $	��Top=�6�   $	��230��6�   $��53��6�   $��1��6�   6�   �� 5      -   5        $	 cboGR��5   C   $
  ��6   $ 2��6#   $��30(�65   $
��Left=�6R   $��675,-50,0,0q�6m   $	��Top=�6�   $	��335��6�   $��53��6�   $��1��6�   6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ 
dataGridGR��5      $��Hidden=�6/      AutoGenerateColumns .  $��25(�6   $
��Left=�6�   $��	25,54,0,0q�6�   $	��Top=�6�   $	��250��6�   $	��985��6�      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
Visibility$ Hidden=�6M      Header$  ��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *IDRESULTADO+   ClipboardContentBinding 6a   Z� 6�     5      $ Hidden=�6K   $  ��6�    6-   �� *	IDCLIENTE+ 6_   Z� 6�     5      $ Cliente��6l    6-   �� *	CLIENTE+ 6I   Z� 6l     5      $
 Saco��6k    6-   �� *GRSACO+ 6H   Z� 6k     5      $ Kg bruto��6n    6-   �� *	GRKGBRUTO+ 6K   Z� 6n     5      $
 Tara��6k    6-   �� *GRTARA+ 6H   Z� 6k     5      $ Kg Neto��6m    6-   �� *
GRKGNETO+ 6J   Z� 6m     5      $ Hidden=�6M   $ Certificacion��6�    6-   �� *CERTIFICADO+ 6a   Z� 6�     5      $ Hidden=�6O   $ idCert��6�    6-   �� *IDCERTIFICADO+ 6c   Z� 6�   5!      5#      5$      �� 5&   
   -   5         ��Name$ label_Copy5��5&      $��30(�6   $��
Of. Origen��63   $
��Left=�6H   $��	23,39,0,0q�6c   $	��Top=�6v   $��54��6�   $	��208��6�   �� 5'   
   -   5        $ label_Copy9��5'      $��30(�6   $��G. Remision SGO��63   $
��Left=�6M   $��
420,40,0,0q�6h   $	��Top=�6|   $��54��6�   $	��250��6�   �� 5(   
   -   5        $ label1_Copy��5(      $!��Detalle guía remisión SGO��6%   $��29(�6I   $
��Left=�6W   $��	25,93,0,0q�6r   $	��Top=�6�   $��49��6�   $	��415��6�   �� 5)   
   -   5         ��Name$ 	btnCerrar��5)       $��30(�6   $��Cerrar��63   $
��Left=�6D   $��755,416,0,0q�6_   $	��Top=�6t   $	��253��6�    J� RenderTransformOrigin$ 
1.03,0.259&�6�   $��63��6�   $��7��6�   6�   5+      5,      