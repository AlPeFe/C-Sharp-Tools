using GamFacturacion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GamFacturacion.Common
{
    public class ExcelClient
    {
        string Path { get; set; }
        
        string FileName { get; set; }
     
        public ExcelClient(string path, string fileName)
        {
            Path = path;
            FileName = FileName;
        }

        public string AddCsvProveedores(List<Proveedor> proveedores, bool requiresHeader)
        {
            string header = "";
            string previousContent = "";
            string csv = "";

            if (!requiresHeader)
            {
                previousContent = OpenCsv();
            }
            else
            {
                header = String.Join(",", typeof(Proveedor).
                   GetProperties().ToArray<string>()) + Environment.NewLine;
            }

            foreach (var item in proveedores)
            {
                csv += String.Join(";", item.GetType().GetProperties().Select(c=> c.GetValue(item, null)));   //creo que aqui al ser de una lista ya mete el environment new line el solo
            }
            var fileContent = header + previousContent + csv;

            return fileContent;
        }

        private void SaveCsv(string csv)
        {
           


        }

        private string OpenCsv()
        {
            var path = Path + FileName;

            var content = File.ReadAllText(@path);

            return content;
        }

        private void DeleteCsv(string csv)
        {


        }
    }
}
