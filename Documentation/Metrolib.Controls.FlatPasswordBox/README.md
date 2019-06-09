# FlatPasswordBox  

FlatPasswordBox is a TextBox which allows a user to enter a password.

**Namespace**: Metrolib.Controls  
**Assembly**: Metrolib (in Metrolib.dll)  

```C#
[System.Windows.TemplatePart]
[System.Windows.TemplatePart]
public class FlatPasswordBox : System.Windows.Controls.Control
```

Inheritance Object -> DispatcherObject -> DependencyObject -> Visual -> UIElement -> FrameworkElement -> Control -> FlatPasswordBox
### Unfocused

```xaml
<Metrolib:FlatPasswordBox Watermark="Enter password..." />

```
![Image of FlatPasswordBox, Unfocused](Unfocused.png)

### Focused

```xaml
<Metrolib:FlatPasswordBox Watermark="Enter password..." />

```
![Image of FlatPasswordBox, Focused](Focused.png)

### Password, Focused

```xaml
<Metrolib:FlatPasswordBox Password="Secret" Watermark="Enter password..." />

```
![Image of FlatPasswordBox, Password, Focused](Password__Focused.png)

### Password, Unfocused

```xaml
<Metrolib:FlatPasswordBox Password="Secret" Watermark="Enter password..." />

```
![Image of FlatPasswordBox, Password, Unfocused](Password__Unfocused.png)

### Disabled

```xaml
<Metrolib:FlatPasswordBox Password="Secret" Watermark="Enter password..." IsEnabled="False" />

```
![Image of FlatPasswordBox, Disabled](Disabled.png)

# Properties  

**Watermark**: System.String  
The watermark that is displayed for as long as no password has been entered.

**Password**: System.String  
The password that has been entered by the user.

