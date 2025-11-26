using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using MudBlazor;
using Portfolio.Models;
using Portfolio.Models.Model;

namespace Portfolio.Services
{
    public class OrderServices
    {
        public readonly AppDbContext context;
        public readonly IJSRuntime runtime;

        public OrderServices(AppDbContext context, IJSRuntime runtime)
        {
            this.context = context;
            this.runtime = runtime;
        }

        public async Task<List<OrderModel>?> GetAllOrders()
        {
            var orders = await context.Orders
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync();

            return orders;
        }

        public async Task<OrderModel?> GetOrderById(int id)
        {
            var order = await context.Orders.FindAsync(id);
            return order == null ? null : order;
        }

        public async Task<bool?> AddOrder(OrderModel record)
        {
            try
            {
                context.Orders.Add(record);
                var status = await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> UpdateOrder(OrderModel record)
        {
            try
            {
                var existing = await context.Orders.FindAsync(record.Id);
                if (existing is null)
                {
                    return false;
                }
                context.Entry(existing).CurrentValues.SetValues(record);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> DeleteOrder(int record)
        {
            try
            {

                var existing = await context.Orders.FindAsync(record);
                if (existing is null) { return false; }
                context.Orders.Remove(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<byte[]> DownloadTemplate()
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("OrderTemplate");
                //set columns
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Symbol";
                worksheet.Cell(1, 3).Value = "BuyPrice";
                worksheet.Cell(1, 4).Value = "Quantity";

                //Style the sheet.
                var header = worksheet.Range(1, 1, 1, 4);
                header.Style.Fill.SetBackgroundColor(XLColor.CoolBlack);
                header.Style.Font.SetFontColor(XLColor.Yellow);
                header.Style.Font.SetBold(true);
                header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Columns().AdjustToContents();
                // Convert to byte array
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var fileBytes = stream.ToArray();

                // Download the file
                var fileName = "OrderTemplate.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                await Task.Delay(1000);
                //await runtime.InvokeVoidAsync("downloadFile", fileName, contentType, fileBytes);

                return fileBytes;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        
    }
}
