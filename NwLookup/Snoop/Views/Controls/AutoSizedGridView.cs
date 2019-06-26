using System;
using System.Windows.Controls;

namespace NwLookup.Snoop.Views.Controls
{
    public class AutoSizedGridView : GridView
    {
        /* AutoSizedGridView class created from resources found at:
         * https://stackoverflow.com/a/15745082/7015777 */
        protected override void PrepareItem(ListViewItem item)
        {
            foreach (GridViewColumn column in Columns)
            {
                // Setting NaN for the column width automatically determines the required
                // width enough to hold the content completely.

                // If the width is NaN, first set it to ActualWidth temporarily.
                if (double.IsNaN(column.Width))
                    column.Width = column.ActualWidth;

                // Finally, set the column with to NaN. This raises the property change
                // event and re computes the width.
                column.Width = double.NaN;
            }
            base.PrepareItem(item);
        }
    }
}
