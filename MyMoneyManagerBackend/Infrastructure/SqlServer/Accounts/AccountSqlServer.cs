namespace Infrastructure.SqlServer.Accounts
{
    public static class AccountSqlServer
    {
        public static double _defaultBalance = 500;
        public static readonly string TableName = "accounts";
        public static readonly string ColumnId = "account_id";
        public static readonly string ColumnBalance = "balance";

        public static readonly string ReqGet = 
            $@"SELECT * FROM {TableName} WHERE {ColumnId}=@{ColumnId}
        ";

        public static readonly string ReqCreate = 
            $@"INSERT INTO {TableName} ({ColumnId},{ColumnBalance}) VALUES (@{ColumnId},@{ColumnBalance})
        ";

        public static readonly string ReqModifyBalance = 
            $@"UPDATE {TableName} SET {ColumnBalance}=CASE WHEN {ColumnBalance} IS NULL OR {ColumnBalance}=0 THEN @{ColumnBalance}  
            ELSE @{ColumnBalance} END WHERE {ColumnId}=@{ColumnId}  
            ";
    }
}