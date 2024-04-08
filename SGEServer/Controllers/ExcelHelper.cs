
using ClosedXML.Excel;
using Microsoft.JSInterop;
using SGEServer.Models;

namespace SGEServer.Controllers
{
    public class ExcelHelper
    {
        private readonly IJSRuntime _js;

        public ExcelHelper(IJSRuntime js)
        {
            _js = js;
        }

        public async Task ExportarTabelaXlsx(string nomeArquivo, List<string> colunas, List<List<string>> linhas)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PADRÃO");

                    for (int i = 1; i <= colunas.Count(); i++)
                    {
                        worksheet.Cell(1, i).Value = colunas[i - 1];
                    }

                    var rangeHeader = worksheet.Range(worksheet.FirstCellUsed(), worksheet.Cell(1, worksheet.ColumnsUsed().Count()));
                    rangeHeader.Style.Fill.BackgroundColor = XLColor.Blue;
                    rangeHeader.Style.Font.FontColor = XLColor.White;
                    rangeHeader.Style.Font.Bold = true;

                    for (int i = 1; i <= colunas.Count(); i++)
                    {
                        worksheet.Cell(1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(1, i).Style.Border.OutsideBorderColor = XLColor.Black;
                    }

                    int nroLinha = 2;

                    foreach (var linha in linhas)
                    {
                        int nroColuna = 1;

                        foreach (var celula in linha)
                        {
                            worksheet.Cell(nroLinha, nroColuna).Value = celula;

                            worksheet.Cell(nroLinha, nroColuna).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(nroLinha, nroColuna).Style.Border.OutsideBorderColor = XLColor.Black;

                            nroColuna++;
                        }

                        if (nroLinha % 2 == 0)
                        {
                            var rangeRow = worksheet.Range(worksheet.Cell(nroLinha, 1), worksheet.Cell(nroLinha, worksheet.ColumnsUsed().Count()));
                            rangeRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                        }

                        nroLinha++;
                    }

                    worksheet.Columns().AdjustToContents();

                    workbook.SaveAs(memoryStream);
                }

                var bytes = memoryStream.ToArray();

                string dataHoraAtual = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                string nomeCompletoArquivo = $"{nomeArquivo}_{dataHoraAtual}.xlsx";

                var base64String = Convert.ToBase64String(bytes);

                await _js.InvokeVoidAsync("downloadFile", base64String, nomeCompletoArquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}
