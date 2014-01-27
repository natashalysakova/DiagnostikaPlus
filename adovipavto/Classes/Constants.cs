using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
using adovipavto.Enums;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    public static class Constants
    {
        public const string GroupTableName = "CarGroup";
        public const string NormativesTableName = "Normatives";
        public const string ProtocolsTableName = "Protocols";
        public const string MechanicsTableName = "Mechanics";
        public const string OperatorsTableName = "Operators";
        public const string LogsTableName = "Logs";
        public const string MesuresTableName = "Mesures";



        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetFullPath(string part)
        {
            return Properties.Settings.Default.FilesDirectory + part;
        }

    }
}