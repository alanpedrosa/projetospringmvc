using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetoMVC01.Reports.Pdf
{
    /// <summary>
    /// Classe para geração de relatórios em formato PDF
    /// </summary>
    public class TarefasReportPdf
    {
        /// <summary>
        /// Método para geração de relatorio de tarefas em PDF
        /// </summary>
        /// <param name="tarefas">Listagem de tarefas para impressão no relatorio</param>
        /// <returns>O arquivo do relatório em bytes (em memória)</returns>
        public byte[] GerarRelatorio(List<Tarefa> tarefas)
        {
            //criando um documento PDF
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            //abrindo o conteudo do arquivo
            using (var document = new Document(pdf))
            {
                //adicionando uma imagem no documento
                var img = ImageDataFactory.Create("https://www.cotiinformatica.com.br/imagens/logo-coti-informatica.png");
                document.Add(new Image(img));

                //formatação do titulo:
                var fmtTitulo = new Style();
                fmtTitulo.SetFontSize(26);
                fmtTitulo.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(9, 69, 136)));

                //adicionando um titulo ao documento
                document.Add(new Paragraph("Relatório de Tarefas").AddStyle(fmtTitulo));

                //formatação do titulo:
                var fmtSubtitulo = new Style();
                fmtSubtitulo.SetFontSize(15);
                fmtSubtitulo.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(9, 69, 136)));

                //adicionando um subtitulo ao documento
                document.Add(new Paragraph("Sistema de controle de tarefas - COTI Informática\n\n").AddStyle(fmtSubtitulo));

                //desenhando uma tabela para exibir as tarefas
                var table = new Table(5);

                //células de cabeçalho da tabela
                table.AddHeaderCell("Nome da tarefa");
                table.AddHeaderCell("Data");
                table.AddHeaderCell("Hora");
                table.AddHeaderCell("Descrição");
                table.AddHeaderCell("Prioridade");

                //imprimir o conteudo da tabela
                foreach (var item in tarefas)
                {
                    table.AddCell(item.Nome);
                    table.AddCell(item.Data.ToString("dd/MM/yyyy"));
                    table.AddCell(item.Hora.ToString());
                    table.AddCell(item.Descricao);
                    table.AddCell(item.Prioridade.ToString());
                }

                //imprimindo a tabela no documento PDF
                document.Add(table);

                //imprimindo a quantidade de tarefas
                document.Add(new Paragraph($"Quantidade de tarefas: {tarefas.Count}"));
            }

            //retornando o relatorio em formato byte[]
            return memoryStream.ToArray();
        }
    }
}
