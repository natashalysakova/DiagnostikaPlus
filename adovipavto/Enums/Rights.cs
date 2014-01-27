using System.ComponentModel;

namespace adovipavto.Enums
{
    internal enum Rights
    {
        [Description("Администратор")]
        Administrator,
        [Description("Оператор")]
        Operator,
        [Description("Уволен")]
        Locked
    }
}