using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalomatKlassen
{
    public static class Lager
    {
        // public string FilePathForSerialization = @"C:\Users\Manni\Desktop\Build\de";

        public static string FilePathForSerialization = @"C:\k3tfs\Programm\bin\Debug\de\LokalisierungsTest";

        public static string FilePathForDeserialization = @"C:\k3tfs\Programm\bin\Debug\de\LokalisierungsTest";

        public static List<MyXtraDocument> ListOutOfDeserializedLanguageFiles { get; set; }

        public static Dictionary<string, MyUiElement> DictOfUiElementsToCompare { get; set; }

        public static Dictionary<object, object> ObjectsDictionary { get; set; }

        public static List<object> unknownobjects { get; set; }
        public static List<object> UnknownObjects { get { if (unknownobjects == null) UnknownObjects = new List<object>(); return unknownobjects; } set { unknownobjects = value; } }

        public static bool GlobalizationMode = true;
    }
}
