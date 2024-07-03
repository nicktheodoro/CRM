-- Tabela "Users"
CREATE TABLE "Users" (
	"Id" UUID NOT NULL,
	"Name" VARCHAR(255) NOT NULL,
	"Email" VARCHAR(255) NOT null UNIQUE,
	"PasswordHash" VARCHAR(255) NOT NULL,
	"IsActive" BOOLEAN NOT NULL,
	"CreatedAt" TIMESTAMP NOT NULL,
	"UpdatedAt" TIMESTAMP,
	PRIMARY KEY ("Id")
);

-- Tabela "Images"
create table "Images" (
	"Id" UUID not null,
	"ContentType" VARCHAR(100),
	"Content" BYTEA not null,
	"CreatedAt" TIMESTAMP not null,
	"UpdatedAt" TIMESTAMP,
	FOREIGN KEY ("Id") REFERENCES "Users"("Id")
);

-- Tabela "ProductCategories"
CREATE TABLE "ProductCategories" (
	"Id" UUID NOT NULL,
	"Name" VARCHAR(255) NOT NULL,
	"Description" VARCHAR(255),
	"CreatedAt" TIMESTAMP NOT NULL,
	"UpdatedAt" TIMESTAMP,
	PRIMARY KEY ("Id")
);

-- Tabela "Products"
CREATE TABLE "Products" (
	"Id" UUID NOT NULL,
	"Name" VARCHAR(255) NOT NULL,
	"Description" VARCHAR(255),
	"UnitPrice" DECIMAL NOT NULL,
	"UnitInStock" DECIMAL NOT NULL,
	"CreatedAt" TIMESTAMP NOT NULL,
	"UpdatedAt" TIMESTAMP,
	"CategoryId" UUID NOT NULL,
	PRIMARY KEY ("Id"),
	FOREIGN KEY ("CategoryId") REFERENCES "ProductCategories"("Id")
);
