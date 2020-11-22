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

        public static readonly string ReqGet = 
            $@"
                SELECT * FROM {TableName} WHERE {ColumnOwner}=@{ColumnOwner} AND {ColumnId}=@{ColumnId}
            ";
        
        public static readonly string ReqQuery = 
            $@"SELECT * FROM {TableName} WHERE {ColumnOwner}=@{ColumnOwner}
        ";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName} ({ColumnId},{ColumnOwner},{ColumnDescription},{ColumnName},{ColumnMax},{ColumnBalance})
            OUTPUT INSERTED.{ColumnId} 
            VALUES 
            (NEWID(),@{ColumnOwner},@{ColumnDescription},@{ColumnName},@{ColumnMax},@{ColumnBalance})
        ";
        public static readonly string ReqPut = $@"
            UPDATE {TableName} SET
            {ColumnBalance} = @{ColumnBalance}, 
            {ColumnMax} = @{ColumnMax},
            {ColumnName} = @{ColumnName},  
            {ColumnDescription} = @{ColumnDescription}
            WHERE {ColumnId}=@{ColumnId} AND {ColumnOwner}=@{ColumnOwner}
        ";
        public static readonly string ReqDelete = $@"
            DELETE FROM {TableName} 
            WHERE {ColumnId} = @{ColumnId} AND {ColumnOwner} = @{ColumnOwner}
        ";
        
        public static readonly string ReqSumTotalBalance = $@"
            SELECT SUM({ColumnBalance}) FROM {TableName} WHERE {ColumnOwner}=@{ColumnOwner}
        ";


    }
}