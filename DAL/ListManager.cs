using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DAL
{
    [Serializable]
    public class ListManager<T> : IListManager<T>
    {

        private List<T> m_list;

        public event EventHandler CollectionChanged;

        public ListManager()
        {
            m_list = new List<T>();
        }

        public int Count => throw new NotImplementedException();

        public List<T> List { get => m_list; }

        public bool Add(T aType)
        {
            if (aType != null)
            {
                m_list.Add(aType);
                return true;
            }
            else
            {
                Debug.Write("There is no object to add");
                return false;
            }

        }

        public bool BinaryDeSerialize(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool BinarySerialize(string fileName)
        {
            FileStream fileStream;
            string errorMsg;

            try
            {
                using (fileStream = new FileStream(fileName, FileMode.Create))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    //b.Serialize(fileStream);
                }
                return true;
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return false;
            }

        }

        JsonSerializerSettings options = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
        };

        public bool JsonSerialize(string fileName)
        {
            string errorMsg;



            // Farid code
            try
            {

                string? jsonString = JsonConvert.SerializeObject(m_list, options);
                File.WriteAllText(fileName, jsonString);
                Debug.WriteLine("JsonString content: " + jsonString);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool JsonDeserialize(string fileName)
        {

            // Farid code

            try
            {
  
                string? jsonString = File.ReadAllText(fileName);
                m_list = JsonConvert.DeserializeObject<List<T>>(jsonString, options);
                Debug.WriteLine(jsonString);
                return true;
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine("JsonSerializationException: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Other Exception: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);
                return false;
            }
        }

        public bool ChangeAt(T aType, int anIndex)
        {
            if (anIndex >= 0 && anIndex < m_list.Count)
            {
                m_list[anIndex] = aType;
                return true;
            }
            else
            {
                Debug.WriteLine("The index is out of bounds");
                return false;
            }
        }

        public bool CheckIndex(int index)
        {
            if (index >= 0 && index < m_list.Count)
            {
                return true;
            }
            else
            {
                Debug.WriteLine("The index is out of bounds");
                return false;
            }
        }

        public void DeleteAll()
        {
            if (m_list.Count > 0)
            {
                foreach (T item in m_list)
                {
                    m_list.Remove(item);
                }
            }
            else
            {
                Debug.WriteLine("The list is already empty");
            }
        }

        public bool DeleteAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                m_list.RemoveAt(anIndex);
                return true;
            }
            else
            {
                Debug.WriteLine("The index is out of bounds");
                return false;
            }
        }

        public T GetAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                return m_list[anIndex];
            }
            else
            {
                Debug.WriteLine("The index is out of bounds");
                return default;
            }
        }

        public string[] ToStringArray()
        {
            if (m_list == null)
            {
                return null;
            }

            string[] strings = new string[m_list.Count];

            for (int i = 0; i < m_list.Count; i++)
            {
                strings[i] = m_list[i].ToString();
            }
            return strings;
        }

        public List<string> ToStringList()
        {
            if (m_list == null)
            {
                return null;
            }

            List<string> strings = new List<string>();

            for (int i = 0; i < m_list.Count; i++)
            {
                strings.Add(m_list.ElementAt(i).ToString());
            }
            return strings;
        }

        public bool XMLSerialize(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
