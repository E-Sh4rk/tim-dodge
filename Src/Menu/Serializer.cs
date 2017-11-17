using System;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace tim_dodge
{
	/// <summary>
	/// Some utility functions to load and save xml (for exemple : scores).
	/// </summary>
	public static class Serializer<T>
	{
		public static T Load(string path)
		{
			using (TextReader reader = new StreamReader(path))
			{
				XmlSerializer xml = new XmlSerializer(typeof(T));
				return (T)xml.Deserialize(reader);
			}
		}
		public static void Save(string path, T instance)
		{
			using (TextWriter writer = new StreamWriter(path))
			{
				XmlSerializer xml = new XmlSerializer(typeof(T));
				xml.Serialize(writer, instance);
			}
		}
	}
	public static class BinarySerializer
	{
		public static object Load(string path)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			object obj = formatter.Deserialize(stream);
			stream.Close();
			return obj;
		}
		public static void Save(string path, object instance)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, instance);
			stream.Close();
		}
	}
}
