using System.IO;
using System.Xml;
using System.Xml.Serialization;

static public class Serializer
{
    /// <summary>
    /// Save
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="a_filename"></param>
    /// <param name="a_path"></param>
    public static void SerializeXML<T>(T t, string a_filename, string a_path)
    {
        if (Directory.Exists(a_path))
        {
            using (FileStream fs = new FileStream(a_path + a_filename + ".xml", FileMode.OpenOrCreate, FileAccess.Write))
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
    /// Load
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="s"></param>
    /// <returns></returns>
    public static T DeserializeXML<T>(string a_path)
    {
        T t;    

        using (FileStream fs = new FileStream(a_path + ".xml", FileMode.OpenOrCreate, FileAccess.Read))
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            t = (T)deserializer.Deserialize(fs);
            fs.Close();
        }

        return t;
    }
}
