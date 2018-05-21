using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataPersistence
{
    public static class Serializer
    {
        private static IFormatter formatter = new BinaryFormatter();


        public static void SaveToFile(object obj)
        {
            Stream stream = null;
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location,
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
                OverwritePrompt = false,
            };

            if(sfd.ShowDialog() == true)
            {
                try
                {
                    if ((stream = sfd.OpenFile()) != null)
                    {
                        Serialize(obj, stream);
                    }
                }
                catch
                {
                    MessageBox.Show("Could not save.", "Error");
                }
            }
        }

        public static object LoadFromFile()
        {
            Stream stream = null;
            object result = null;

            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location,
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    if ((stream = ofd.OpenFile()) != null)
                    {
                        result = Deserialize(stream);
                    }
                }
                catch
                {
                    MessageBox.Show("Could not load.", "Error");
                }
            }

            return result;
        }
        public static void Serialize(object obj, string address)
        {
            Stream stream = new FileStream(address, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static void Serialize(object obj, Stream stream)
        {
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static object Deserialize(string address)
        {
            Stream stream = new FileStream(address, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            Object obj = formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }

        public static object Deserialize(Stream stream)
        {
            Object obj = formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }
    }
}
