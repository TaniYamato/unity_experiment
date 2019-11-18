using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CSV : MonoBehaviour {

	public static void Write(float[,] result, string file_name)
    {
        StreamWriter sw;
        FileInfo fi;
        string FilePath = Application.dataPath + @"\" + file_name;
        fi = new FileInfo(FilePath);
        sw = fi.AppendText();
        sw.WriteLine("condition,mouse,RT");
        for (int i=0; i<result.GetLength(0); i++)
        {
            sw.WriteLine(result[i,0] + "," + result[i,1] + ","+ result[i, 2]);
        }
        sw.Flush();
        sw.Close();
    } 
}
