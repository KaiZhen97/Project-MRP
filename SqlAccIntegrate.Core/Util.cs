using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace SqlAccIntegrate.Core
{
    public class Util
    {
        public static DataTable DynamicToDataTable(dynamic TblDataSet, string TblName = "TEMP_TBL")
        {
            DataSet Tbl = new DataSet();
            Dictionary<int, object> dict = new Dictionary<int, object>();
            dynamic currField;
            DataColumn col;
            DataRow row;
            int I, J, K;

            // 1. Add table into dataset
            Tbl.Tables.Add(TblName);

            // 2. Add Columns
            for (I = 0; I <= TblDataSet.Fields.Count - 1; I++)
            {
                dict.Add(I, TblDataSet.Fields.Items(I));
                currField = dict[I];
                col = new DataColumn(currField.FieldName);
                Tbl.Tables[TblName].Columns.Add(col);
            }

            // 3. Add Rows Data
            J = TblDataSet.RecordCount();
            TblDataSet.DisableControls();
            TblDataSet.First();
            K = 0;

            while (!TblDataSet.eof)
            {
                K += 1;
                row = Tbl.Tables[TblName].Rows.Add();

                for (I = 0; I <= TblDataSet.Fields.Count - 1; I++)
                {
                    currField = dict[I];
                    row[I] = currField.Value;
                }
                TblDataSet.Next();
            }

            // 4. Return Table
            return Tbl.Tables[TblName];
        }

        public static List<T> DataTableToList<T>(DataTable Tbl)
        {
            List<T> dataList = new List<T>();

            foreach (DataRow row in Tbl.Rows)
            {
                Type type = typeof(T);
                T rowObj = Activator.CreateInstance<T>();

                foreach (DataColumn column in Tbl.Columns)
                {
                    foreach (PropertyInfo pro in type.GetProperties())
                    {

                        if (pro.Name == column.ColumnName)
                        {
                            var fieldValue = row[column.ColumnName] == DBNull.Value ? null : Convert.ChangeType(row[column.ColumnName], pro.PropertyType);
                            pro.SetValue(rowObj, fieldValue, null);
                            goto nextupperloop;
                        }
                        else
                            continue;
                    }

                    nextupperloop:;
                }

                dataList.Add(rowObj);
            }

            return dataList;
        }

        public static List<T> DynamicToList<T>(dynamic Data)
        {
            List<T> dataList = new List<T>();
            Dictionary<int, object> dict = new Dictionary<int, object>();
            //dynamic currField;

            for (int i = 0; i <= (Data.Fields.Count - 1); i++)
            {
                dict.Add(i, Data.Fields.Items(i));
            }

            while (!Data.eof)
            {
                T rowObj = Activator.CreateInstance<T>();

                rowObj = DynamicToObject<T>(Data);

                dataList.Add(rowObj);

                Data.Next();
            }

            return dataList;
        }

        public static T DynamicToObject<T>(dynamic data)
        {
            T retVal = Activator.CreateInstance<T>();
            Type convertType = typeof(T);

            dynamic currField;

            for (int i = 0; i < data.Fields.Count; i++)
            {
                currField = data.Fields.Items(i);

                foreach (PropertyInfo prop in convertType.GetProperties())
                {
                    if (prop.Name == currField.FieldName)
                    {
                        var fieldValue = DBNull.Value.Equals(currField.Value) ? null
                            : Convert.ChangeType(currField.Value, prop.PropertyType);

                        prop.SetValue(retVal, fieldValue);

                        goto upperLoop;
                    }
                    else
                    {
                        continue;
                    }
                }
                upperLoop:;
            }

            return retVal;
        }

        public static List<T> XMLStrToList<T>(string xmlStr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlObj<T>));

            return ((XmlObj<T>)serializer.Deserialize(new StringReader(xmlStr))).DataList;
        }

        public static T XMLStrToObject<T>(string xmlStr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlObj<T>));
            var processed = ((XmlObj<T>)serializer.Deserialize(new StringReader(xmlStr)));

            return processed.DataList.FirstOrDefault();
        }
    }
}
