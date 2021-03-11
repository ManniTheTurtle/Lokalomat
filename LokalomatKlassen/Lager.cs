using DevExpress.XtraEditors;
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

        private static List<object> unknownobjects;
        public static List<object> UnknownObjects { get { if (unknownobjects == null) unknownobjects = new List<object>(); return unknownobjects; } set { unknownobjects = value; } }

        public static bool GlobalizationMode = true;

        private static Dictionary<string, Type> typesdictionary;
        public static Dictionary<string, Type> TypesDictionary { get { if (typesdictionary == null) typesdictionary = new Dictionary<string, Type>(); return typesdictionary; } set { typesdictionary = value; } }


        private static Dictionary<string, XtraForm> xtraformsdictionary;
        public static Dictionary<string, XtraForm> XtraFormsDictionary { get { if (xtraformsdictionary == null) xtraformsdictionary = new Dictionary<string, XtraForm>(); return xtraformsdictionary; } set { xtraformsdictionary = value; } }
    }
}
