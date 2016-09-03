using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

[System.Serializable]
public class CardImage
{
    public int number;
    public string name;
}

[System.Serializable]
public class ImageDataList
{
    public CardImage[] CardImages;
}
