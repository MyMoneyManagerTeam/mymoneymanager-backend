namespace Infrastructure.SqlServer.Auth
{
    public static class UserSqlServer
    {
        public static readonly string TableName = "users";
        public static readonly string JoinTableName = "accounts";
        public static readonly string JoinTableAccountIdArgument = "account_id";
        public static readonly string JoinTableBalanceArgument = "balance";
        public static readonly string ColumnId = "user_id";
        public static readonly string ColumnMail = "mail";
        public static readonly string ColumnPassword = "password";
        public static readonly string ColumnFirstName = "first_name";
        public static readonly string ColumnLastName = "last_name";
        public static readonly string ColumnCity = "city";
        public static readonly string ColumnAddress = "address";
        public static readonly string ColumnZipCode = "zip";
        public static readonly string ColumnArea = "area";
        public static readonly string ColumnCountry = "country";
        public static readonly string ColumnConfirmed = "confirmed";
        public static readonly string ColumnAdmin = "admin";
        public static readonly string ColumnPicture = "picture";

        public static readonly string ReqQuery =
            $@"SELECT * FROM {TableName} INNER JOIN {JoinTableName} ON {TableName}.{ColumnId} = {JoinTableAccountIdArgument};
        ";
        
        public static readonly string ReqGet =
            $@"SELECT * FROM {TableName} WHERE {ColumnMail}=@{ColumnMail} AND {ColumnPassword}=@{ColumnPassword}
        ";
        
        public static readonly string ReqCreate = 
            $@"INSERT INTO {TableName}
            ({ColumnId},{ColumnMail},{ColumnPassword},{ColumnFirstName},{ColumnLastName},{ColumnCountry},{ColumnArea},
            {ColumnAddress},{ColumnZipCode},{ColumnCity},{ColumnPicture},{ColumnConfirmed},{ColumnAdmin})
            OUTPUT INSERTED.{ColumnId}            
            VALUES (NEWID(),@{ColumnMail},@{ColumnPassword},@{ColumnFirstName},@{ColumnLastName},@{ColumnCountry},
            @{ColumnArea},@{ColumnAddress},@{ColumnZipCode},@{ColumnCity},NULL,1,0)
        ";

        public static readonly string ReqUpload = $@"
            UPDATE {TableName} SET
            {ColumnPicture} = @{ColumnPicture}
            WHERE {ColumnId} = @{ColumnId}
        ";

        public static readonly string ReqUpdate = $@"UPDATE {TableName} SET 
            {ColumnAdmin} = @{ColumnAdmin}, 
            {ColumnConfirmed} = @{ColumnConfirmed} 
            WHERE {ColumnId} = @{ColumnId}";
    }
}