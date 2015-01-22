using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;


namespace HawkFileOperation
{
    /// <summary>
    /// 读取.hex文件
    /// </summary>
    public class HawkClassHexFile    
    {
        /// <summary>
        /// 源文件路径（不带文件名）
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// 目的文件路径（不带文件名）
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// 源文件路径（带文件名）
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// 目的文件路径（带文件名）
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// 读取文件
        /// </summary>
        /// 

        /// <summary>
        /// 超始地址
        /// </summary>
        public UInt16 StartAdd=0;

        /// <summary>
        /// 结束地址
        /// </summary>
        public UInt16 EndAdd = 0xFFFF;

        /// <summary>
        /// 数据长度
        /// </summary>
        public Int32 DataLength = 1024;

        /// <summary>
        /// Debug模式
        /// </summary>
        public bool DebugFlag = false;

        /// <summary>
        /// 实际读取数据长度
        /// </summary>
        public Int32 ReadCodeNum = 0;


        /// <summary>
        /// 读取文件
        /// </summary>
        virtual public byte[] ReadFile()
        {
            byte[] DataArray = new byte[DataLength];
            FileStream DataPoint = new FileStream(SourceFilePath, FileMode.Open);
            StreamReader DataStream = new StreamReader(DataPoint);
            string LineDataString = "";
            string LineCodeString = "";
            UInt16 LineAdd =0;
            UInt16 LastLineAdd =0;
            byte LineCodeLength ;
            byte LineCheckSum ;
            byte DataType;
            int i = 0;
            int LineNum = 0;
            bool CodeReadFinish = false;
            while (true) //读出的hex的数据
            {
                if ((LineDataString = DataStream.ReadLine()) != null)
                {
                    if (LineDataString.Contains(":"))
                    {
                        LastLineAdd = LineAdd;
                        LineAdd = Convert.ToUInt16(LineDataString.Substring(3, 4), 16); ;
                        LineCodeLength = Convert.ToByte(LineDataString.Substring(1, 2),16);
                        LineCheckSum = Convert.ToByte(LineDataString.Substring(LineDataString.Length - 2, 2),16);
                        LineCodeString = LineDataString.Substring(9,2*LineCodeLength);
                        DataType = Convert.ToByte(LineDataString.Substring(7, 2), 16);
                        

                        if (DebugFlag == true)
                        {
                            MessageBox.Show("LineAdd:" + LineAdd.ToString("X"));
                            MessageBox.Show("LineCodeLength:" + LineCodeLength.ToString("X"));
                            MessageBox.Show("LineCheckSum:" + LineCheckSum.ToString("X"));
                            MessageBox.Show(LineCodeString);
                            MessageBox.Show("LineNum:"+LineNum.ToString());
                            MessageBox.Show("DataType:" + DataType.ToString());
                            LineNum++;
                        }

                        if(LineAdd < LastLineAdd)
                        {
                            CodeReadFinish = true;
                        }

                        if (DataType == 0 && CodeReadFinish == false && (LineAdd >= StartAdd && LineAdd <= EndAdd))   //为数据才写入数组,且代码段读取未完成
                        {
                            ReadCodeNum = LineAdd + LineCodeLength - StartAdd;
                            for (i = 0; i < LineCodeLength; i++)
                            {
                                DataArray[LineAdd - StartAdd + i] = Convert.ToByte(LineCodeString.Substring(i * 2, 2), 16);
                                if (DebugFlag == true)
                                {
                                    MessageBox.Show("i:" + (LineAdd - StartAdd + i).ToString());
                                    MessageBox.Show(DataArray[LineAdd - StartAdd + i].ToString("X"));

                                }
                            }
                        }
                        

                        
                        
                    }
                }
                else
                    break;
            }
            DataPoint.Close();
            DataStream.Close();
            return DataArray;
            
        }
    }
    /// <summary>
    /// 读取.bin文件
    /// </summary>
    public class HawkClassBinFile        
    {
        /// <summary>
        /// 源文件路径（不带文件名）
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// 目的文件路径（不带文件名）
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// 源文件路径（带文件名）
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// 目的文件路径（带文件名）
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// 数据长度
        /// </summary>
        public Int32 DataLength = 1024;

        /// <summary>
        /// 读取文件
        /// </summary>
        virtual public byte[] ReadFile()
        {
            FileStream FilePoint = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader DataStream = new BinaryReader(FilePoint);
            byte[] DataArray = DataStream.ReadBytes(DataLength);
            return DataArray;
        }
    }
    /// <summary>
    /// 读取.dat文件
    /// </summary>
    public class HawkClassDatFile     
    {
        /// <summary>
        /// 源文件路径（不带文件名）
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// 目的文件路径（不带文件名）
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// 源文件路径（带文件名）
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// 目的文件路径（带文件名）
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// 读取文件
        /// </summary>
        virtual public void ReadFile()
        {
            MessageBox.Show(SourceFilePath);
        }
    }
    /// <summary>
    /// 读取.txt文件
    /// </summary>
    public class HawkClassTxtFile       
    {
        /// <summary>
        /// 源文件路径（不带文件名）
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// 目的文件路径（不带文件名）
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// 源文件路径（带文件名）
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// 目的文件路径（带文件名）
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// 读取文件
        /// </summary>
        virtual public void ReadFile()
        {
            MessageBox.Show(SourceFilePath);
        }
    }

    public class HawkClassOutPutFile
    {

        public int Raw = 10;
        public int Column = 10;
        public int DataLength = 10;
        public string IsolateString = " ";
        public bool IsContain0x = true;
        public string AlphabetFormat = "X";
        public bool OpenFileFlag = false;
        /// <summary>
        /// 目的文件路径（不带文件名）
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// Debug模式
        /// </summary>
        public bool DebugFlag = false;


        /// <summary>
        /// 按指定格式输出文件
        /// </summary>
        /// 
        virtual public void OutFile(byte[] ArrayData)
        {
            int i = 0,j=0;
            string TempString = "";
            FileStream SavePoint = new FileStream(DestinationFilePath, FileMode.Create);
            StreamWriter SaveStream = new StreamWriter(SavePoint);
            
            for(i=0;i<Raw;i++)
            {
                TempString = "";
                for (j = 0; j < Column; j++)
                {
                    if (AlphabetFormat.ToLower() == "x" && IsContain0x == true)
                        TempString += "0x";
                        TempString += ArrayData[i * Column + j].ToString(AlphabetFormat);
                        TempString += IsolateString;
                }
                TempString += "\r";
               
                SaveStream.WriteLine(TempString);
                
            }
            if (DebugFlag == true)
                MessageBox.Show(TempString);
            SaveStream.Close();
            SavePoint.Close();

            if (OpenFileFlag == true)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = DestinationFilePath;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start(); //打开文件
            }
        }
    }

}
