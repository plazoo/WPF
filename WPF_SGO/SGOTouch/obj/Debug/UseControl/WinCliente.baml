   M S B A M L   `   `   `  ����C  ?SGOTouch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SGOTouch.UseControlSGOTouch.UseControl  5	   	   $    SGOTouch.UseControl.WinCliente   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:SGOTouch.UseControl     5	   	     � Title$  
WinCliente��5   	   $��513.956��6   $��1130.992��6-    � Icon$; 5/SGOTouch;component/Asset/images/iconos/ic_delete.gif��6>    � WindowStartupLocation$ CenterScreen=�6{    � WindowStyle$
 None=�6�   .��5      � $��0,0,2,2q�6   .+�5/   
   �� -   5         ��Name$ label1��5/       $��0��6   $��30(�60   $��Cliente��6>   $
��Left=�6P   $��	42,58,0,0q�6k   $	��Top=�6~   $��42��6�   $	��127��6�   �� 50   
   -   5         ��Name$ txtDesc��50   ;   $��30(�6   $
��Left=�6L   $��42��6g   $��
196,58,0,0q�6s    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��402��6�   $��3��6�   � 5<   
   .+�5=      �� -   5         ��Name$ 	btnBuscar��5=   $   $��30(�6   $��Buscar��67   $
��Left=�6H   $��
630,50,0,0q�6c   $	��Top=�6w   $	��196��6�    J� RenderTransformOrigin$ 
1.03,0.259&�6�   $��49��6�   $��2��6�   6�   �� 5>      -   5        $ btnSalir��5>   $   $��30(�6   $��Salir��66   $
��Left=�6F   $��
862,50,0,0q�6a   $	��Top=�6u   $	��196��6�   $ 
1.03,0.259&�6�   $��49��6�   $��2��6�   6�   �� 5E      -   5        	 ��Name$	 	sPanelOne��5E      $��	0,0,0,-31q�6,   .+�5F      �� -   5        
 ��Name$
 listViewCliente��5F      $��Hidden=�64   $��30(�6h   $��
28,115,0,0q�6v   $	��310��6�    �� SelectionMode$ Single=�6�   $
��Left=�6�   $	��Top=�6�   $
��1047��6�    �� View 5G      �� 5H      .P�5I      �� $�� ��6-    �� Width$ 0��6j    �� DisplayMemberBinding 67   ��  �� Path$ IDCLIENTESGO�6j   �� 5J      $��Cliente��6-    6>   �� $ CLIENTE��� 5K      $��	Tipo Doc.��6-    6@   �� $ TIPO_DOCUMENTO��� 5L      $��	Nro. Doc.��6-    6@   �� $ DOCIDENTIDAD��� 5M      $��Departamento��6-    6C   �� $ DEPARTAMENTO��� 5N      $��	Provincia��6-    6@   �� $ 	PROVINCIA��� 5O      $��Distrito��6-    6?   �� $ DISTRITO�5P      5R      5T      5V      5[      5]      