using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class UILocalization
    {
        private static readonly Dictionary<string, Dictionary<string, string>> packs = new Dictionary<string, Dictionary<string, string>>
        {
            {"English", new Dictionary<string, string>
                {
                {"Delete all selected positions", "Delete all selected positions"},
                {"Title", "Title" },
                {"Description", "Description"},
                {"PositionSkills", "PositionSkills"},
                {"Error Page", "Error Page"},
                {"SIGN IN", "SIGN IN"},
                {"Email address", "Email address"},
                {"Password", "Password"},
                {"Sign Up", "Sign Up"},
                {"Sign in", "Sign in"},
                {"Remember this device?", "Remember this device?"},
                {"SIGN UP", "SIGN UP"},
                {"Role", "Role"},
                {"Skills", "Skills"},
                {"Profile Skills", "Profile Skills"},
                {"Missed Skills", "Missed Skills"},
                {"Publish", "Publish"},
                {"Save", "Save"},
                {"Position Name", "Position Name"},
                {"Access Rules(Filters)", "Access Rules(Filters)"},
                {"Max count of projects", "Max count of projects"},
                {"Project tags", "Project tags"},
                {"Generate CV", "Generate CV"},
                {"First Name", "First Name"},
                {"Last Name", "Last Name"},
                {"Birth Day", "Birth Day"},
                {"Name", "Name"},
                {"Like", "Like"},
                {"Max count of projects(0 if not restrictions)", "Max count of projects(0 if not restrictions)"},
                {"Rules", "Rules"},
                {"Add new position", "Add new position"},
                {"Potencial value", "Potencial value" },
                {"For OneOfMany potencial value is list of options(example: \"first;second;...;last\").For other types is placeholder.",
                    "For OneOfMany potencial value is list of options(example: \"first;second;...;last\").For other types is placeholder."},
                {"Category name", "Category name"},
                {"Date Period", "Date Period"},
                {"Add", "Add"},
                {"CVs", "CVs"},
                {"Positions", "Positions"},
                {"Home", "Home"},
                {"Profile", "Profile"},
                {"Skills and Projects", "Skills and Projects"},
                {"Logout", "Logout"},
                {"Edit Skills", "Edit Skills"},
                {"Add Skill", "Add Skill"},
                {"Add Category", "Add Category"},
                {"Type name", "Type name" },
                {"Skill name", "Skill name" },
                {"Project Tags", "Project Tags" },
                {"Skill", "Skill" },
                {"Edit", "Edit" },
                {"ViewCandidates", "ViewCandidates" },
                {"Delete all selected candidates", "Delete all selected candidates" },
                {"Block all selected candidates", "Block all selected candidates" },
                {"Unblock all selected candidates", "Unblock all selected candidates" },
                {"Rule", "Rule" },
                {"Projects", "Projects" },
                {"Gender", "Gender" },
                {"Phone", "Phone" },
                {"General account for our services", "General account for our services" },
                {"Inspector email", "Inspector email" },
                {"Priority", "Priority" },
                {"Support ticket", "Support ticket" }
                }
            },
            {"Русский", new Dictionary<string, string>
                {
                {"Delete all selected positions", "Удалить все выделенные позиции"},
                {"Title", "Заголовок" },
                {"Description", "Описание"},
                {"PositionSkills", "Требуемые навыки"},
                {"Error Page", "Ошибка"},
                {"SIGN IN", "ВОЙТИ"},
                {"Email address", "Почта"},
                {"Password", "Пароль"},
                {"Sign Up", "Зарегестрироваться"},
                {"Sign in", "Войти"},
                {"Remember this device?", "Запомнить данное устроиство?"},
                {"SIGN UP", "РЕГИСТРАЦИЯ"},
                {"Role", "Роль"},
                {"Skills", "Навыки"},
                {"Profile Skills", "Заполненные навыки"},
                {"Missed Skills", "Пропущенные навыки"},
                {"Publish", "Опубликовать"},
                {"Save", "Сохранить"},
                {"Position Name", "Позиция"},
                {"Access Rules(Filters)", "Правила доступа(фильтры)"},
                {"Max count of projects", "Максимальное количество проектов"},
                {"Project tags", "Теги проектов"},
                {"Generate CV", "Сгенерировать резюме"},
                {"First Name", "Имя"},
                {"Last Name", "Фамилия"},
                {"Birth Day", "Дата рождение"},
                {"Name", "Имя"},
                {"Like", "Нравится"},
                {"Max count of projects(0 if not restrictions)", "Максимальное количество проектов(0 если нет ограничений)"},
                {"Rules", "Правила"},
                {"Add new position", "Добавить позицию"},
                {"Potencial value", "Потенциальное значение" },
                {"For OneOfMany potencial value is list of options(example: \"first;second;...;last\").For other types is placeholder.",
                    "Для типа \"OneOfMany\" потенциальное значение это список значений(например: \"первый;второй;...;последний\").Для других типов это плейсхолдер"},
                {"Category name", "Категория"},
                {"Date Period", "Временной период"},
                {"Add", "Добавить"},
                {"CVs", "Список резюме"},
                {"Positions", "Позиции"},
                {"Home", "Главная"},
                {"Profile", "Профиль"},
                {"Skills and Projects", "Навыки и проекты"},
                {"Logout", "Выйти"},
                {"Edit Skills", "Изменить навыки"},
                {"Add Skill", "Добавить навык"},
                {"Add Category", "Добавить категорию"},
                {"Type name", "Тип" },
                {"Skill name", "Название навыка" },
                {"Project Tags", "Теги проектов" },
                {"Skill", "Навык" },
                {"Edit", "Изменить" },
                {"ViewCandidates", "Кандидаты" },
                {"Delete all selected candidates", "Удалить всех выделенных кандидатов" },
                {"Block all selected candidates", "Заблокировать всех выделенных кандидатов" },
                {"Unblock all selected candidates", "Разблокировать всех выделенных кандидатов" },
                {"Rule", "Правило" },
                {"Projects", "Проекты" },
                {"Gender", "Пол" },
                {"Phone", "Телефон" },
                {"General account for our services", "Общий аккаунт для наших сервисов" },
                {"Inspector email", "Почта проверяющего" },
                {"Priority", "Приоритет" },
                {"Support ticket", "Книга жалоб" }


                }
            }
        };
        public static string GetSentence(string language, string sentenceCode)
        {
            return packs[language][sentenceCode];
        }
        public static IEnumerable<string> GetSupportedLanguages()
        {
            return packs.Keys;
        }
    }
}
