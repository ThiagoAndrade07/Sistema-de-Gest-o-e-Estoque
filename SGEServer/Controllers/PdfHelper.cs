using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Globalization;
using Microsoft.JSInterop;
using SGEServer.Controllers;
using SGEServer.Models;
using System.Net.NetworkInformation;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace SGEServer.Controllers
{
    public class PdfHelper
    {
        private readonly IJSRuntime _js;

        // Construtor para injetar IJSRuntime
        public PdfHelper(IJSRuntime js)
        {
            _js = js;
        }

        private void AdicionarParagrafoSeExistir(string texto, float xPos, float yPos, PdfWriter writer, Font fonte)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                PdfContentByte canvas = writer.DirectContent;
                Phrase phrase = new Phrase(texto, fonte);
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, phrase, xPos, yPos, 0);
            }
        }

        public async void GerarPDFHelper(string NomeFantasia, string NumCotacao, int CodEmpresa, List<ProdutosModel> listProdutos, decimal ValorTotalCotacao, int PrazoEntregaRetornado, int FaturamentoRetornado)
        {
            ClientesController _Clientes = new ClientesController();

            MemoryStream memoryStream = new MemoryStream();

            Document document = new Document(PageSize.A4);


            string nomeArquivo = $"{NumCotacao}_{NomeFantasia}.pdf";
            
            // Prepara o documento PDF para a escrita
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            document.Open();

            // Adicionar o logo no topo esquerdo
            byte[] logoBytes = _Clientes.PuxaLogoEmpresa(CodEmpresa);
            if (logoBytes != null)
            {
                Image logo = Image.GetInstance(logoBytes);
                logo.SetAbsolutePosition(document.Left, document.Top - 70); // Ajuste a posição conforme necessário
                logo.ScaleToFit(140, 100); // Ajuste o tamanho conforme necessário
                document.Add(logo);
            }

            // Adicionar o número da cotação no topo
            Font fonteNumeroCotacao = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Phrase fraseNumeroCotacao = new Phrase("N. COTAÇÃO: " + NumCotacao, fonteNumeroCotacao);
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_RIGHT, fraseNumeroCotacao, document.Right - 36, document.Top - 25, 0); // Ajuste a posição conforme necessário

            // Espaçamento após o cabeçalho
            document.Add(new Paragraph(new Chunk(" ", fonteNumeroCotacao)));

            // Adicionar informações da empresa
            DataRow dadosEmpresa = _Clientes.PuxaDadosEmpresa(CodEmpresa);
            if (dadosEmpresa != null)
            {
                Font fonteInformacoes = FontFactory.GetFont("Arial", 10, Font.NORMAL);
                float yPos = document.Top - 95; // Ajuste essa posição Y conforme necessário

                AdicionarParagrafoSeExistir("Razão Social: " + dadosEmpresa["RazaoSocial"].ToString(), document.Left, yPos, writer, fonteInformacoes);
                AdicionarParagrafoSeExistir("Cnpj: " + dadosEmpresa["Cnpj"].ToString(), document.Left, yPos -= 15, writer, fonteInformacoes);
                AdicionarParagrafoSeExistir("Endereço: " + dadosEmpresa["Endereco"].ToString(), document.Left, yPos -= 15, writer, fonteInformacoes);
                AdicionarParagrafoSeExistir("Cidade: " + dadosEmpresa["Cidade"].ToString(), document.Left, yPos -= 15, writer, fonteInformacoes);
                AdicionarParagrafoSeExistir("CEP: " + dadosEmpresa["Cep"].ToString(), document.Left, yPos -= 15, writer, fonteInformacoes);
                AdicionarParagrafoSeExistir("Telefone: " + dadosEmpresa["Telefone"].ToString(), document.Left, yPos -= 15, writer, fonteInformacoes);
            }

            // Espaçamento entre dados da empresa e a tabela
            Paragraph espaco = new Paragraph(" ", fonteNumeroCotacao);
            espaco.SpacingBefore = 140f; // Ajuste o valor conforme necessário
            document.Add(espaco);


            // Adiciona os dados da tabela
            PdfPTable table = new PdfPTable(7); // Número de colunas
            table.WidthPercentage = 100; // Faz a tabela ocupar 100% da largura do documento

            // Cabeçalhos da tabela
            table.AddCell("Qtde");
            table.AddCell("Item");
            table.AddCell("Un.Medida");
            table.AddCell("NCM");
            table.AddCell("CA");
            table.AddCell("Valor Un.");
            table.AddCell("Valor Total");

            // Adiciona dados
            foreach (var item in listProdutos)
            {
                table.AddCell(item.QtdeItens.ToString());
                table.AddCell(item.Item);
                table.AddCell(item.UnidadeMedida);
                table.AddCell(item.Ncm);
                table.AddCell(item.CaEpi);
                table.AddCell(item.ValorUnitario.ToString("F2"));
                table.AddCell(item.ValorTotalProduto.ToString("F2"));
            }

            document.Add(table);

            DateTime DataCriacao = DateTime.Now;
            string mesExtenso = DataCriacao.ToString("MMMM", new CultureInfo("pt-BR"));
            string dataFormatada = $"{DataCriacao.Day} de {mesExtenso} de {DataCriacao.Year}";

            string cidade = dadosEmpresa["Cidade"].ToString();

            string dataECidade = $"{cidade}, {dataFormatada}";

            // Adicionar o número Valor Total Cotação
            Font ValorTotalCotacaoPdf = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Phrase ValorTotalCotacaoPdfFrase = new Phrase("Valor Total Cotação: R$ " + ValorTotalCotacao.ToString("F2"), ValorTotalCotacaoPdf);
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_RIGHT, ValorTotalCotacaoPdfFrase, document.Right - 36, document.Bottom + 45, 0); // Ajuste a posição conforme necessário

            // Adicionar o número do Prazo De Entrega
            Font PrazoEmtrega = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Phrase PrazoEmtregaFrase = new Phrase("Prazo de Entrega: " + PrazoEntregaRetornado, PrazoEmtrega);
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, PrazoEmtregaFrase, document.Left + 36, document.Bottom + 45, 0); // Ajuste a posição conforme necessário

            // Adicionar o número de faturamento
            Font Faturamento = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Phrase FaturamentoFrase = new Phrase("Faturamento: " + FaturamentoRetornado, Faturamento);
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, FaturamentoFrase, document.Left + 36, document.Bottom + 25, 0); // Ajuste a posição conforme necessário

            // Adicionar o dia de criação da cotação
            Font fonteData = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Phrase fraseData = new Phrase(dataECidade, fonteData);
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, fraseData, document.Left + 36, document.Bottom + 5, 0);

            // Fecha o documento
            document.Close();

            byte[] pdfBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(pdfBytes);


            
            await _js.InvokeVoidAsync("downloadFile", base64String, nomeArquivo, "application/pdf");

        }
    }
}
