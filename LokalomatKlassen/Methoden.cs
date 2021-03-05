using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LokalomatKlassen
{
    public class Methoden
    {

        public static Dictionary<Object, string> dictionary = new Dictionary<object, string>();

        // Assemblysuche
        public List<Assembly> LadeAssemblies(string Verzeichnis)
        {
            List<Assembly> AssemblyList = new List<Assembly>();

            if (Verzeichnis != null)
            {
                foreach (string Datei in Directory.GetFiles(Verzeichnis).Where(s => !s.Contains("DevExpress.") && s.EndsWith(".dll") || s.EndsWith(".exe")))
                {
                    try
                    {
                        AssemblyList.Add(Assembly.LoadFile(Datei));
                    }
                    catch
                    {
                    }
                }
            }

            return AssemblyList;
        }

        // Typesuche
        public List<Type> LadeTypes(Assembly assembly, List<Type> TypesList)
        {
            if (assembly != null)
            {
                try
                {
                    TypesList.AddRange((from t in assembly.GetTypes()
                    .Where(t => typeof(XtraForm).IsAssignableFrom(t) || typeof(Form).IsAssignableFrom(t)
                    || typeof(XtraUserControl).IsAssignableFrom(t) || typeof(UserControl).IsAssignableFrom(t))
                                        select t).ToList());
                }
                catch (Exception)
                {
                }
            }
            return TypesList;
        }

        // Types unterscheiden und Xtra Forms von abstract zu concrete konvertieren
        public (List<XtraForm>, List<XtraUserControl>) SortiereTypes(List<Type> TypesList, List<XtraForm> xtraFormList, List<XtraUserControl> xtraUserControlList)
        {
            foreach (var item in TypesList)      // --> Types sortieren
            {
                if (item != null)
                {
                    try
                    {
                        if (typeof(XtraForm).IsAssignableFrom(item))
                        {
                            xtraFormList.Add((XtraForm)Activator.CreateInstance(item));
                        }
                        else if (typeof(XtraUserControl).IsAssignableFrom(item))
                        {
                            xtraUserControlList.Add((XtraUserControl)Activator.CreateInstance(item));
                        }
                    }
                    catch (Exception)
                    {
                    }


                }
            }
            return (xtraFormList, xtraUserControlList);
        }

        // Deserialisiere alles aus einem Verzeichnis
        public List<MyXtraDocument> DeserializeAllFilesFromFolder(string chosenfilepath)
        {
            List<MyXtraDocument> DeserializedXtraDocsList = new List<MyXtraDocument>();

            string[] filesArray = Directory.GetFiles(chosenfilepath);

            foreach (var file in filesArray)
            {
                string jsonstring = File.ReadAllText(file);

                if (file.EndsWith(".json"))
                {
                    var MyXtraDocNullTest = JsonConvert.DeserializeObject<MyXtraDocument>(jsonstring);

                    if (MyXtraDocNullTest != null)
                    {
                        DeserializedXtraDocsList.Add(MyXtraDocNullTest);    // momentan werden alle assemblys gemeinsam deserialized in eine Liste
                    }
                }
            }

            return DeserializedXtraDocsList;
        }
    }
}
