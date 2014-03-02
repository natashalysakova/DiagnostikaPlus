using System;
using System.ComponentModel;
using System.Reflection;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    public static class Constants
    {
        public const string GroupTableName = "Group";
        public const string NormativesTableName = "Normatives";
        public const string ProtocolsTableName = "Protocols";
        public const string MechanicsTableName = "Mechanics";
        public const string OperatorsTableName = "Operators";
        public const string MesuresTableName = "Mesures";


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof (DescriptionAttribute),
                    false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public static string GetFullPath(string part)
        {
            return Settings.Instance.FilesDirectory + part;
        }
    }
}