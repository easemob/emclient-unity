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

        public FileOperator()
        {
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

        public void Test()
        {
            FileInfo file = new FileInfo(Application.dataPath + "/token.conf");
            if (file.Exists)
            {
                file.Delete();
                file.Refresh();
            }
            for (int i=0; i<4; i++)
            {
                WriteData("record data: " + i);
            }

            allData = new List<string>();
            ReadData();
            for (int i=0; i<allData.Count; i++)
            {
                Debug.Log("read data: " + allData[i]);
            }
        }

        public void WriteData(string data)
        {
            FileInfo file = new FileInfo(Application.dataPath + "/token.conf");
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
        
        public List<string> ReadData()
        {
            if(allData == null)
                allData = new List<string>();

            FileInfo file = new FileInfo(Application.dataPath + "/token.conf");
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
            reader.Dispose();
            reader.Close();
            return allData;
        }

    }
}

