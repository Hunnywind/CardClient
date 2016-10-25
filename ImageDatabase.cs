using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

[Serializable]
public class ImageData
{
    public int number;
    public string pictureName;
}

public class ImageDatabase : MonoBehaviour
{
    public void FileLoad()
    {
        StreamReader sr = new StreamReader("image_info");
        var loadData = JsonUtility.FromJson<ImageData>(sr.ReadToEnd());
        
    }
}