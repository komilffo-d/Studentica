using Studentica.Common.Attributes;

namespace Studentica.UI.Common.Enums
{
    public enum StatusProject : sbyte
    {
        [StringValue("Новый")]
        NEW,
        [StringValue("Открытый")]
        OPEN,
        [StringValue("Анализ")]
        ANALIS,
        [StringValue("Разработка")]
        DEVELOPMENT,
        [StringValue("Тест")]
        TEST,
        [StringValue("Развёртывание")]
        DEPLOY,
        [StringValue("Выполнен")]
        COMPLETED,
        [StringValue("Отложен")]
        DELAY
    }
}
