namespace Infrastructure.SqlServer.Transactions
{
    public static class TransactionSqlServer
    {
        public static readonly string TableName = "transactions";
        public static readonly string ColumnId = "transaction_id";
        public static readonly string ColumnEmitterId = "emitter_id";
        public static readonly string ColumnReceiverId = "receiver_id";
        public static readonly string ColumnAmount = "amount";
        public static readonly string ColumnTransactionDate = "transaction_date";
        public static readonly string ColumnDescription = "description";
        public static readonly string ColumnEmitterName = "emitter_name";
        public static readonly string ColumnReceiverName = "receiver_name";

        public static readonly string ReqQueryBase =
            $@"SELECT * FROM {TableName} 
            WHERE ({ColumnEmitterId}=@{ColumnEmitterId} OR {ColumnReceiverId}=@{ColumnReceiverId}) 
            
        ";
        public static readonly string ReqQueryDays = $@"
             AND ({ColumnTransactionDate}>= (GETDATE() - @days)) 
        ";

        public static readonly string ReqQueryOrder = $@"
             ORDER BY {ColumnTransactionDate} DESC 
        ";
        public static readonly string ReqQueryOffset = $@"
             OFFSET @offset ROWS  
        ";

        public static readonly string ReqQueryFetch = $@"
             FETCH NEXT @number ROWS ONLY 
        ";

        public static readonly string ReqCreate = 
            $@"INSERT INTO {TableName} ({ColumnId},{ColumnEmitterId},{ColumnReceiverId},{ColumnAmount},
            {ColumnTransactionDate},{ColumnDescription},{ColumnEmitterName},{ColumnReceiverName}) 
            OUTPUT INSERTED.{ColumnId} 
            VALUES 
            (NEWID(),@{ColumnEmitterId},@{ColumnReceiverId},@{ColumnAmount},@{ColumnTransactionDate},
            @{ColumnDescription},@{ColumnEmitterName},@{ColumnReceiverName})
        ";


        public static readonly string ReqCount = $@"
            SELECT COUNT({ColumnId}) FROM {TableName} 
            WHERE ({ColumnEmitterId}=@{ColumnEmitterId} OR {ColumnReceiverId}=@{ColumnReceiverId}) 
        ";
    }
}