CREATE TABLE [dbo].[Budgets] (
    [Amount]    DECIMAL (18) NOT NULL,
    [YearMonth] VARCHAR (7)  NOT NULL,
    CONSTRAINT [PK_Budgets] PRIMARY KEY CLUSTERED ([YearMonth] ASC)
);