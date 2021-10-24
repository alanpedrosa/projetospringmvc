using OfficeOpenXml;
using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoMVC01.Reports.Excel
{
    /// <summary>
    /// Classe para geração do relatorio em formato EXCEL
    /// </summary>
    public class TarefasReportExcel
    {
        /// <summary>
        /// Método para gerar um relatório do tipo EXCEL para tarefas
        /// </summary>
        /// <param name="tarefas">listagem de tarefas que será impressa no relatório</param>
        /// <returns>O arquivo do relatório em bytes (em memória)</returns>
        public byte[] GerarRelatorio(List<Tarefa> tarefas)
        {
            //definindo o uso da versão free do epplus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //montar o arquivo xlsx
            using (var excel = new ExcelPackage())
            {
                //criando a primeira planilha do arquivo..
                var planilha = excel.Workbook.Worksheets.Add("Tarefas");

                //titulo da planilha
                planilha.Cells["A1"].Value = "Relatório de Tarefas";

                //subtitulo na planilha
                planilha.Cells["A2"].Value = "Sistema de controle de tarefas - COTI Informática";

                //cabeçalhos das colunas
                planilha.Cells["A4"].Value = "Nome da tarefa";
                planilha.Cells["B4"].Value = "Data";
                planilha.Cells["C4"].Value = "Hora";
                planilha.Cells["D4"].Value = "Descrição";
                planilha.Cells["E4"].Value = "Prioridade";

                //imprimindo as tarefas na planilha
                var linha = 5;
                foreach (var item in tarefas)
                {
                    planilha.Cells[$"A{linha}"].Value = item.Nome;
                    planilha.Cells[$"B{linha}"].Value = item.Data.ToString("dd/MM/yyyy");
                    planilha.Cells[$"C{linha}"].Value = item.Hora.ToString();
                    planilha.Cells[$"D{linha}"].Value = item.Descricao;
                    planilha.Cells[$"E{linha}"].Value = item.Prioridade.ToString();

                    linha++; //incrementando a linha
                }

                //ajustando a largura das colunas da planilha
                planilha.Cells["A:E"].AutoFitColumns();

                //retornando o conteudo do arquivo..
                return excel.GetAsByteArray();
            }
        }
    }
}
