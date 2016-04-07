using System.IO;
using System.Xml.Serialization;

static public class Serializer
{
    public static void SerializeXML<T>(T t, string a_filename, string a_path)
    {
        if (Directory.Exists(a_path))
        {
            using (FileStream fs = File.Create(a_path + a_filename + ".xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, t);
                fs.Close();
            }
        }
        else
        {
            Directory.CreateDirectory(a_path);
        }

    }

    /// <summary>
    /// deserialize from a path
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="s"></param>
    /// <returns></returns>
    public static T DeserializeXML<T>(string s)
    {
        T t;    

        using (FileStream fs = File.OpenRead(s + ".xml"))
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            t = (T)deserializer.Deserialize(fs);
            fs.Close();
        }

        return t;
    }
}
