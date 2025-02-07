

using Aspose.Pdf;

namespace Order.Infrastructure.GenerateRaport.FileManagmet
{
    public static class PDFCreator
    {
        public static void GenerateDoc(IEnumerable<Order.Domain.Enities.Order> orders, string tempPath)
        {
            Document document = new Document();
            Page page = document.Pages.Add();
            Table table = new Table();
            table.Border = new BorderInfo(BorderSide.All, .5f, Color.FromRgb(System.Drawing.Color.LightGray));
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.FromRgb(System.Drawing.Color.LightGray));
            Row hederRow = table.Rows.Add();
            //better to create some report configuration than hard-code
            hederRow.Cells.Add("Time");
            hederRow.Cells.Add("Product Name");
            hederRow.Cells.Add("Quantity");
            hederRow.Cells.Add("Price per item");
            foreach (var order in orders)
            {
                var row = table.Rows.Add();
                foreach (var item in order.OrderDetails.Products)
                {
                    row.Cells.Add(order.CreatedTime.ToString());
                    row.Cells.Add(item.Name);
                    row.Cells.Add(item.Quantity.ToString());
                    row.Cells.Add(item.price.ToString());
                }

            }
            page.Paragraphs.Add(table);
            document.Save(tempPath);
        }
    }
}
