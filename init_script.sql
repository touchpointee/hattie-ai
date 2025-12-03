CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "AppUsers" (
    "Id" uuid NOT NULL,
    "Username" text NOT NULL,
    "Email" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "TenantId" text NOT NULL,
    CONSTRAINT "PK_AppUsers" PRIMARY KEY ("Id")
);

CREATE TABLE "ChatSessions" (
    "Id" uuid NOT NULL,
    "UserId" text NOT NULL,
    "Title" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "TenantId" text NOT NULL,
    CONSTRAINT "PK_ChatSessions" PRIMARY KEY ("Id")
);

CREATE TABLE "Tenants" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "SystemInstruction" text NOT NULL,
    "KnowledgeBaseText" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "TenantId" text NOT NULL,
    CONSTRAINT "PK_Tenants" PRIMARY KEY ("Id")
);

CREATE TABLE "ChatMessages" (
    "Id" uuid NOT NULL,
    "ChatSessionId" uuid NOT NULL,
    "Role" text NOT NULL,
    "Content" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "TenantId" text NOT NULL,
    CONSTRAINT "PK_ChatMessages" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ChatMessages_ChatSessions_ChatSessionId" FOREIGN KEY ("ChatSessionId") REFERENCES "ChatSessions" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_ChatMessages_ChatSessionId" ON "ChatMessages" ("ChatSessionId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251201142415_InitialCreate', '8.0.0');

COMMIT;

