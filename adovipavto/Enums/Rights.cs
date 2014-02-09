using System.ComponentModel;

namespace adovipavto.Enums
{
    public enum Rights
    {
        [Description("Администратор")] Administrator,
        [Description("Оператор")] Operator,
        [Description("Уволен")] Locked
    }
}