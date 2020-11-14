namespace Infrastructure.SqlServer.Jars
{
    public static class JarSqlServer
    {
        public static readonly string TableName = "jars";
        public static readonly string ColumnId = "jar_id";
        public static readonly string ColumnOwner = "owner";
        public static readonly string ColumnDescription = "description";
        public static readonly string ColumnName = "name";
        public static readonly string ColumnMax = "max";
        public static readonly string ColumnBalance = "balance";
        
        public static readonly string ReqQuery = 
            $@"SELECT * FROM {TableName} WHERE {ColumnOwner}=@{ColumnOwner}
        ";
        
        
    }
}