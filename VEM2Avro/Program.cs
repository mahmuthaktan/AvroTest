using AvroTestApp.VEMEntities;
using Microsoft.Hadoop.Avro;
using Microsoft.Hadoop.Avro.Container;
using Microsoft.Hadoop.Avro.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Vem2Avro.Data;
using VEM2Avro.VEMEntities;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace VEM2Avro
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Environment.NewLine;

            string fileName = "Messages.avro";
            string filePath = null;
            if (Environment.NewLine.Contains("\r"))
            {
                filePath = new DirectoryInfo(".") + @"\" + fileName;
            }
            else
            {
                filePath = new DirectoryInfo(".") + @"/" + fileName;
            }

             

            //Hasta:
            List<VEM_HASTA> listHasta = GetInserted<VEM_HASTA>("HASTA",
                                                    1).ToList();

            listHasta.AddRange(GetUpdated<VEM_HASTA>("HASTA",
                                                 new DateTime(2019, 09, 01)).ToList());

            using (var dataStream = new MemoryStream())
            //using (var dataStream = new FileStream(filePath, FileMode.Create))
            {
                using (var avroWriter = AvroContainer.CreateWriter<VEM_HASTA>(dataStream, Codec.Deflate))
                {
                    using (var seqWriter = new SequentialWriter<VEM_HASTA>(avroWriter, listHasta.Count))
                    {
                        listHasta.ForEach(seqWriter.Write);
                    }
                }

                dataStream.Seek(0, SeekOrigin.Begin);
                //using (var dataStream = new MemoryStream())
                //    var reader = DataFileReader<VEM_HASTA>.OpenReader(memoryStream);

               // using (var dataStreamOrj = new MemoryStream())
                //using (var dataStream = new FileStream(filePath, FileMode.Open))
                //{
                //    var avroOku = AvroContainer.CreateReader<VEM_HASTA>(dataStream);
                //}
                var avroOku = AvroContainer.CreateReader<VEM_HASTA>(dataStream);



                dataStream.Dispose();
            }

          




            // string StringSchema = "{\"type\":\"record\", \"name\":\"n\", \"fields\":[{\"name\":\"HASTA_KODU\", \"type\":\"int\"}]}";
            //string StringSchema = "{\"type\":\"record\", \"name\":\"n\", \"fields\":[" +
            //    "{\"name\":\"HASTA_KODU\", \"type\":\"int\"}," +
            //    "{\"name\":\"REFERANS_TABLO_ADI\", \"type\":\"string\"}," +
            //    "{\"name\":\"TC_KIMLIK_NUMARASI\", \"type\":\"string\"}" +
            //    "]}";


            //Generic Test
            // SQLDataAccess d = new SQLDataAccess();

            //DataTable dt = d.GetDataSetReader("select * from VEM_HASTA", null).Tables[0];

            //string StringSchema = "{\"type\":\"record\", \"name\":\"n\", \"fields\":[";

            //StringSchema = StringSchema + GetAvroSchema(dt) + "]}";

            //using (var stream = new MemoryStream())
            //{
            //    var serializer = AvroSerializer.CreateGeneric(StringSchema);
            //    using (var streamWriter = AvroContainer.CreateGenericWriter(StringSchema, stream, Codec.Null))
            //    {
            //        using (var writer = new SequentialWriter<object>(streamWriter, 24))
            //        {
            //            var expected = new List<AvroRecord>();
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                AvroRecord record = new AvroRecord(serializer.WriterSchema);
            //                for (int i = 0; i < dt.Columns.Count; i++)
            //                {
            //                    if (dt.Columns[i].DataType == typeof(DateTime))
            //                    {
            //                        if (dr[i] != null && !string.IsNullOrWhiteSpace(dr[i].ToString()))
            //                            record[i] = ConvertDateTimeToPosixTime((DateTime)dr[i]);
            //                        else
            //                            record[i] = ConvertDateTimeToPosixTime(new DateTime(1900, 01, 01));
            //                    }
            //                    else
            //                        record[i] = dr[i];
            //                }
            //                //record.HASTA_KODU = record["HASTA_KODU"];
            //                expected.Add(record);
            //            }

            //            expected.ForEach(writer.Write);
            //            writer.Flush();

            //            stream.Seek(0, SeekOrigin.Begin);
            //            dynamic actual = serializer.Deserialize(stream);

            //            var deserializer = AvroSerializer.CreateGenericDeserializerOnly(StringSchema, StringSchema);
            //            deserializer.Deserialize(stream);


            //        }
            //    }
            //}



        }
        public static IEnumerable<T> GetInserted<T>(string TableName, int LastInserted)
        {
            return DatabaseAccess.Query<T>(@"select top 1000 * from VEM_" + TableName + " (nolock)",
                " WHERE  " + TableName + "_KODU>" + LastInserted.ToString(), null);
            //                new { fkCalisan = _fkCalisan });
        }
        public static IEnumerable<T> GetUpdated<T>(string TableName, DateTime LastUpdateDate)
        {
            return DatabaseAccess.Query<T>(@"select top 1000 * from VEM_" + TableName + " (nolock)",
                " WHERE GUNCELLEME_ZAMANI > @LastUpdateDate ",
                new { LastUpdateDate = LastUpdateDate });
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ConvertDateTimeToPosixTime(DateTime value)
        {
            var truncated = new DateTime(value.Ticks - (value.Ticks % TimeSpan.TicksPerSecond));
            var different = truncated - UnixEpoch;
            return (long)different.TotalSeconds;
        }
        private static string GetAvroSchema(DataTable dt)
        {
            string schema = "";
            foreach (DataColumn dc in dt.Columns)
            {
                string tip = "";

                if (dc.DataType == typeof(int))
                    tip = "{ \"name\": \"" + dc.ColumnName + "\",  \"type\": [\"int\", \"null\"]},";
                else if (dc.DataType == typeof(Int64))
                    tip = "  { \"name\":\"" + dc.ColumnName + "\",\"type\":[\"null\",\"long\"]},";
                //tip = "{\"name\": \"" + dc.ColumnName + "\",  \"type\": [\"null\",{\"type\" : \"long\"}]},";

                else if (dc.DataType == typeof(string))
                    tip = "{ \"name\": \"" + dc.ColumnName + "\",  \"type\": [\"string\", \"null\"]},";
                else if (dc.DataType == typeof(DateTime))
                    tip = "{\"name\":\"" + dc.ColumnName + "\", \"type\":[\"null\",{\"type\" : \"long\",\"logicalType\": \"date\"}]},";
                else
                    tip = "{ \"name\": \"" + dc.ColumnName + "\",  \"type\": [\"string\", \"null\"]},";


                schema = schema + tip;


                // if (dc.DataType == typeof(int))
                //     tip = "int";
                // else if (dc.DataType == typeof(string))
                //     tip = "{ \"name\": \"" + dc.ColumnName + "\", \"type\": [\"string\", \"null\"], \"default\": \"\" },";
                // else if (dc.DataType == typeof(DateTime))
                //     tip = "[\"null\",{\"type\" : \"long\",\"logicalType\": \"date\"}]";
                //else if (dc.DataType == typeof(DateTime))
                //     schema += "{\"name\":\"" + dc.ColumnName + "\", \"type\":" + tip + "},";
                // else
                //     schema += "{\"name\":\"" + dc.ColumnName + "\", \"type\":\"" + tip + "\"},";
            }
            return schema.Remove(schema.Length - 1);
            ;
        }
    }
}
