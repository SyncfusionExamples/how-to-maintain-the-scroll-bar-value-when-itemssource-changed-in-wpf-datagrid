# How to maintain the scroll bar value when ItemsSource changed in WPF DataGrid (SfDataGrid)

The sample show cases how to maintain the scroll bar value when ItemsSource changed in [WPF DataGrid](https://www.syncfusion.com/wpf-ui-controls/datagrid) (SfDataGrid)?

# About the sample

The [WPF DataGrid](https://www.syncfusion.com/wpf-ui-controls/datagrid) (SfDataGrid) cannot maintain the scrollbar position when ItemsSource changed. But you can achieve this by get and set the scrollbar position using [SfDataGrid.ItemsSourceChanged](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfGrid.WPF~Syncfusion.UI.Xaml.Grid.SfDataGrid~ItemsSourceChanged_EV.html) event.

```c#
this.dataGrid.ItemsSourceChanged += DataGrid_ItemsSourceChanged;

private void DataGrid_ItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
{
    if (columnName.Count > 0)
    {
        foreach (var col in columnName)
        {
            this.dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription() { ColumnName = col });
        }
        foreach (Group group in dataGrid.View.Groups)
        {
            var isExpandGroup = group;
            var key = expandedGroups.FirstOrDefault(colu => colu.Key.ToString() == isExpandGroup.Key.ToString());
            do
            {
                if (key != null)
                    dataGrid.ExpandGroup(isExpandGroup);

                if (isExpandGroup.Groups != null)
                {
                    isExpandGroup = isExpandGroup.Groups[0];
                    key = expandedGroups.FirstOrDefault(col => col.Groups[0].Key.ToString() == group.Groups[0].Key.ToString());
                }
                else
                    isExpandGroup = null;
            } while (isExpandGroup != null);
        }
    }
    VisualContainer container = this.dataGrid.GetVisualContainer();
    container.ScrollRows.ScrollBar.Value = this.Scrollbarvalue;
    container.InvalidateMeasureInfo();
}

private void Button_Click_1(object sender, RoutedEventArgs e)
{
    var groups = dataGrid.View.Groups;
    foreach (Group group in groups)
    {
        if (group.IsExpanded)
            expandedGroups.Add(group);
    }
    foreach (GroupColumnDescription groupColumnDescriptions in dataGrid.GroupColumnDescriptions)
        columnName.Add(groupColumnDescriptions.ColumnName);
    VisualContainer container = this.dataGrid.GetVisualContainer();
    double scrollValue = container.ScrollRows.ScrollBar.Value;
    this.Scrollbarvalue = scrollValue;
    //change Items source
    this.dataGrid.ItemsSource = viewModel.Ordersnew;
}
```

KB article - [How to maintain the scroll bar value when ItemsSource changed in WPF DataGrid (SfDataGrid)](https://www.syncfusion.com/kb/11858/how-to-maintain-the-scroll-bar-value-when-itemssource-changed-in-wpf-datagrid-sfdatagrid)

## Requirements to run the demo
 Visual Studio 2015 and above versions
