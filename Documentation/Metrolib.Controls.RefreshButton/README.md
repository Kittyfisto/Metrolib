# RefreshButton

A button that can be used to refresh things.

Shows a circular progress indicator while being refreshed.

### Unfocused

```xaml
<Metrolib:RefreshButton />
```
![Image of RefreshButton, Unfocused](Unfocused.png)

### Disabled

```xaml
<Metrolib:RefreshButton IsEnabled="False" />
```
![Image of RefreshButton, Disabled](Disabled.png)

# Properties

**IsRefreshing**: System.Boolean  
When set to true, the button will show a Metrolib.CircularProgressBar with
                IsIndeterminate
                set to true.

