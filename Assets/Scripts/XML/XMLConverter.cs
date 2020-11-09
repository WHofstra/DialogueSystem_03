using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class XMLConverter : MonoBehaviour
{
	public static void Serialize(object item, string path, string fileName)
	{
		XmlSerializer serializer = new XmlSerializer(item.GetType());
		StreamWriter writer      = new StreamWriter(path + "/" + fileName);

		serializer.Serialize(writer.BaseStream, item);
		writer.Close();
	}

	public static T Deserialize<T>(string path, string fileName)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		StreamReader reader      = new StreamReader(path + "/" + fileName);

		//Any Type
		T deserialized = (T)serializer.Deserialize(reader.BaseStream);
		reader.Close();

		return deserialized;
	}
}
