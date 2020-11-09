CREATE TABLE "users"(
    "user_id" uniqueidentifier NOT NULL,
    "mail" NVARCHAR(255) NOT NULL,
    "password" NVARCHAR(255) NOT NULL,
    "first_name" NVARCHAR(255) NOT NULL,
    "last_name" NVARCHAR(255) NOT NULL,
    "picture" VARBINARY(MAX) NULL,
    "country" NVARCHAR(255) NOT NULL,
    "area" NVARCHAR(255) NOT NULL,
    "address" NVARCHAR(255) NOT NULL,
    "zip" INT NOT NULL,
    "city" NVARCHAR(255) NOT NULL,
    "confirmed" BIT NOT NULL,
    "admin" BIT NOT NULL
);
ALTER TABLE
    "users" ADD CONSTRAINT "users_user_id_primary" PRIMARY KEY("user_id");
CREATE UNIQUE INDEX "users_mail_unique" ON
    "users"("mail");
CREATE TABLE "accounts"(
    "account_id" uniqueidentifier NOT NULL,
    "balance" FLOAT NOT NULL
);
ALTER TABLE
    "accounts" ADD CONSTRAINT "accounts_account_id_primary" PRIMARY KEY("account_id");
CREATE TABLE "jars"(
    "jar_id" uniqueidentifier NOT NULL,
    "owner" uniqueidentifier NOT NULL,
    "description" NVARCHAR(255) NULL,
    "name" NVARCHAR(255) NOT NULL,
    "max" FLOAT NOT NULL,
    "balance" FLOAT NOT NULL
);
ALTER TABLE
    "jars" ADD CONSTRAINT "jars_jar_id_primary" PRIMARY KEY("jar_id");
CREATE TABLE "transactions"(
    "transaction_id" uniqueidentifier NOT NULL,
    "emitter_id" uniqueidentifier NOT NULL,
    "receiver_id" uniqueidentifier NOT NULL,
    "amount" FLOAT NOT NULL,
    "transaction_date" DATE NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "emitter_name" NVARCHAR(255) NOT NULL,
    "receiver_name" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "transactions" ADD CONSTRAINT "transactions_transaction_id_primary" PRIMARY KEY("transaction_id");
ALTER TABLE
    "jars" ADD CONSTRAINT "jars_owner_foreign" FOREIGN KEY("owner") REFERENCES "accounts"("account_id");
ALTER TABLE
    "transactions" ADD CONSTRAINT "transactions_emitter_id_foreign" FOREIGN KEY("emitter_id") REFERENCES "accounts"("account_id");
ALTER TABLE
    "transactions" ADD CONSTRAINT "transactions_receiver_id_foreign" FOREIGN KEY("receiver_id") REFERENCES "accounts"("account_id");