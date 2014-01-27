using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using adovipavto.Enums;

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

        public static string[] NormativesTitles =
        {
            /*0*/"Общая удельная тормозная сила рабочей ТС (доли ед.)",
            /*1*/"Общая удельная тормозная сила стояночной ТС (доли ед.)",
            /*2*/"Отн. разность тормозных сил (1-я ось)(доли ед.)",
            /*3*/"Отн. разность тормозных сил (2-я ось)(доли ед.)",
            /*4*/"Отн. разность тормозных сил (3-я ось)(доли ед.)",
            /*5*/"Максимальное время срабатывания ТС (мс)",
            /*6*/"Контрольное усилие на орган управления (1-я ось)(Н)",
            /*7*/"Суммарный люфт (град)",
            /*8*/"Сила света фар ближнего света (Кд)",
            /*9*/"Сила света фар дальнего света (Кд)",
            /*10*/"Сила света противотуманных фар (Кд)",
            /*11*/"Частота проблесков указателей поворота (Гц)",
            /*12*/"Остаточная высота рисунка протектора (мм)",
            /*13*/"Содержание СО: минимальная частота вращения (%)",
            /*14*/"Содержание СО: повышенная частота вращения(%)",
            /*15*/"Содержание СН: минимальная частота вращения (ppm)",
            /*16*/"Содержание СН: повышенная частота вращения(ppm)",
            /*17*/"Дымность в режиме свободных ускорений (м-1)",
            /*18*/"Дымность в режиме свободных ускорений (%)",
            /*19*/"Частота вращения на минимальных оборотах (об. мин)",
            /*20*/"Частота вращения на повышеных оборотах (об. мин)",
            /*21*/"Прозрачность ветрового стекла (%)",
            /*22*/"Прозрачность переднего бокового стекла (%)",
            /*23*/"Внешний шум автомобиля (дБА)"
        };



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

        internal static int GetNormativeIndex(string title)
        {
            for (int i = 0; i < NormativesTitles.Length; i++)
            {
                if (NormativesTitles[i] == title)
                    return i;
            }

            return -1;
        }
    }
}