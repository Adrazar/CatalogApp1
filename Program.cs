using System;
using System.IO;
using System.Data;
using System.Xml.Schema;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace EjercicioCatalog
{
    class Program
    {
        public static String path = Directory.GetCurrentDirectory();
        static void Main(String[] args)
        {
            //el reader of the categorias csv
            var MyReader = new Microsoft.VisualBasic.FileIO.TextFieldParser("C:/Users/Marcos/source/repos/CatalogApp1/Categories.csv");
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            MyReader.SetDelimiters(new string[] { ";" });

        var tablaCat = new DataTable();
        tablaCat.TableName = "MyCategories";//thetable
        tablaCat.Columns.Add("Id", typeof(string));//datatype string
        tablaCat.Columns.Add("Name", typeof(string));//datatype string
        tablaCat.Columns.Add("Description", typeof(string));//datatype string
             if(!MyReader.EndOfData)
                {
                MyReader.ReadLine();
            }
                
            while (!MyReader.EndOfData)
            {
            tablaCat.Rows.Add(MyReader.ReadFields());
            /* reads to table*/
        }

 


        //el reader of the categorias csv
        var PrReader = new Microsoft.VisualBasic.FileIO.TextFieldParser("C:/Users/Marcos/source/repos/CatalogApp1/Products.csv");
        PrReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
        PrReader.SetDelimiters(new string[] { ";" });

        var tablaProd = new DataTable();
        tablaProd.TableName = "MyProducts";//thetable
        tablaProd.Columns.Add("Id", typeof(string));//datatype string
        tablaProd.Columns.Add("CategoryId", typeof(string));//datatype string
        tablaProd.Columns.Add("Name", typeof(string));//datatype string
        tablaProd.Columns.Add("Price", typeof(string));//datatype string
             if (!PrReader.EndOfData)
                 {
                 PrReader.ReadLine();
             }
            while (!PrReader.EndOfData)
            {
            tablaProd.Rows.Add(PrReader.ReadFields());
            /* reads to table*/
        }

                       
            DataTable catalog = tablaCat.Copy();
            //DataRow row = new DataRow;
            catalog.TableName = "MyCatalog";//thetable
            
            catalog.Columns.Add("Products", typeof(DataTable));//datatype string


            foreach (DataRow dc in catalog.Rows) {
                DataTable dt = new DataTable();
                dt.TableName = "ProductsOfCategory";
                dt.Columns.Add("Id", typeof(string));//datatype string
                dt.Columns.Add("CategoryId", typeof(string));//datatype string
                dt.Columns.Add("Name", typeof(string));//datatype string
                dt.Columns.Add("Price", typeof(string));//datatype string

                dt.Clear();
                foreach (DataRow dp in tablaProd.Rows)
                {
                    if (dc[0].ToString() == dp[1].ToString()) {
                        
                        dt.Rows.Add(dp.ItemArray);
                      
                    }

                }
                                 


                dc["Products"] = dt;
                    

            }
          


            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(catalog);

            StreamWriter writer1 = new StreamWriter("Resultado.json");
            writer1.Write(JSONString.ToString());
            writer1.Close();


                      
            System.IO.StringWriter writer2 = new System.IO.StringWriter();
            catalog.WriteXml("Resultado.xml");





        }
    }
}
