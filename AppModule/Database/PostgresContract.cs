namespace ConsoleApp1.Database
{
    /// <summary>
    ///     Класс-контракт со для базы данных PostgreSQL.
    ///
    ///     Внимание!!!
    ///     User должен обладать правами на запись в БД по указанному адресу;
    ///     Имя базы данных не должно совпадать с уже существующими по указанному адресу;
    ///     Порт указываем соответствующий порту БД по указанному адресу.
    /// </summary>
    public static class PostgresContract
    {
        /// <summary>Host - адрес базы данных для подключения.</summary>
        public static string Host = "localhost";
        /// <summary>User - имя юзера в БД по указанному адресу. Должен обладать правами на запись.</summary>
        public static string User = "postgres";
        /// <summary>Password - пароль юзера в БД по указанному адресу</summary>
        public static string Password = "admin";
        /// <summary>DBname - имя таблицы в БД по указанному адресу, в которую будут сохраняться данные.
        ///     Должно быть уникальным.</summary>
        public static string DBname = "worms";
        /// <summary>Port - порт БД по указанному адресу.</summary>
        public static string Port = "5432";
    }
}