using System;
using System.Xml.Serialization;
using System.IO;

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
}
