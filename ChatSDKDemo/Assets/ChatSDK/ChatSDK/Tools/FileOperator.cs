using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class FileOperator
    {
        StreamWriter writer;
        StreamReader reader;
        List<string> allData;

        string tokenfn;
        string appkeyfn;

        public FileOperator()
        {
            tokenfn = Application.dataPath + "/token.conf";
            appkeyfn = Application.dataPath + "/appkey.conf";

            if (allData == null)
                allData = new List<string>();


            CreateEmptyFile(tokenfn);
            CreateEmptyFile(appkeyfn);
        }

        private static FileOperator _FileOperator
        {
            get;
            set;
        }

        public static FileOperator GetInstance()
        {
            if (_FileOperator == null)
            {
                _FileOperator = new FileOperator();
            }
            return _FileOperator;
        }

        public string GetTokenConfFile()
        {
            return tokenfn;
        }

        public string GetAppkeyConfFile()
        {
            return appkeyfn;
        }

        public void Test()
        {
            FileInfo file = new FileInfo(tokenfn);
            if (file.Exists)
            {
                file.Delete();
                file.Refresh();
            }
            for (int i=0; i<4; i++)
            {
                WriteData(tokenfn, "record data: " + i);
            }

            allData = new List<string>();
            ReadData(tokenfn);
            for (int i=0; i<allData.Count; i++)
            {
                Debug.Log("read data: " + allData[i]);
            }
        }

        public void CreateEmptyFile(string fn)
        {
            FileInfo file = new FileInfo(fn);
            if (!file.Exists)
            {
                writer = file.CreateText();
                writer.WriteLine("");
                writer.Dispose();
                writer.Close();
            }
        }

        public void WriteData(string fn, string data)
        {
            FileInfo file = new FileInfo(fn);
            if (!file.Exists)
            {
                writer = file.CreateText();
            }
            else
            {
                writer = file.AppendText();
            }
            writer.WriteLine(data);
            writer.Flush();
            writer.Dispose();
            writer.Close();
        }
        
        public string ReadData(string fn)
        {
            FileInfo file = new FileInfo(fn);
            reader = file.OpenText();

            string str;
            while((str = reader.ReadLine()) != null)
            {
                allData.Add(str);
            }
            if (allData == null)
            {
                Debug.Log("No data!");
            }
            //string data = "";
            //if (allData.Count != 0)
            //{
            //   data = allData[0];
            //}
            string data = allData[0];
            allData.Clear();
            reader.DiscardBufferedData();
            reader.Dispose();
            reader.Close();
            return data;
        }

    }
}

