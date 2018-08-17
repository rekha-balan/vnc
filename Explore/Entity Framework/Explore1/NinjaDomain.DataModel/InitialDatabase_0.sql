﻿DECLARE @CurrentMigration [nvarchar](max)

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'NinjaDomain.DataModel.Migrations.Configuration'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '201808162150233_InitialDatabase'
BEGIN
    CREATE TABLE [dbo].[Clans] (
        [Id] [int] NOT NULL IDENTITY,
        [ClanName] [nvarchar](max),
        CONSTRAINT [PK_dbo.Clans] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Ninjas] (
        [Id] [int] NOT NULL IDENTITY,
        [ClanId] [int] NOT NULL,
        [DateOfBirth] [datetime] NOT NULL,
        [DateOfDeath] [datetime],
        [Name] [nvarchar](max),
        [ServedInOniwaban] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.Ninjas] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_ClanId] ON [dbo].[Ninjas]([ClanId])
    CREATE TABLE [dbo].[NinjaEquipments] (
        [Id] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max),
        [Type] [int] NOT NULL,
        [Ninja_Id] [int] NOT NULL,
        CONSTRAINT [PK_dbo.NinjaEquipments] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_Ninja_Id] ON [dbo].[NinjaEquipments]([Ninja_Id])
    ALTER TABLE [dbo].[Ninjas] ADD CONSTRAINT [FK_dbo.Ninjas_dbo.Clans_ClanId] FOREIGN KEY ([ClanId]) REFERENCES [dbo].[Clans] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[NinjaEquipments] ADD CONSTRAINT [FK_dbo.NinjaEquipments_dbo.Ninjas_Ninja_Id] FOREIGN KEY ([Ninja_Id]) REFERENCES [dbo].[Ninjas] ([Id]) ON DELETE CASCADE
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'201808162150233_InitialDatabase', N'NinjaDomain.DataModel.Migrations.Configuration',  0x1F8B0800000000000400ED5ACD6EE33610BE17E83B083AB54536CACF651BD8BBC83A491174132FE2ECB6B78096C60E5B8AD28A54D646D127EBA18FD457E850FF22455B769C20582C7288CD9F8FC3996F86C3A1FFFBE7DFC1DB45C89C0748048DF8D03DDC3F701DE07E14503E1FBAA99CBD7AEDBE7DF3FD7783F3205C389FCA71C76A1CCEE462E8DE4B199F789EF0EF2124623FA47E12896826F7FD28F44810794707073F7B87871E20848B588E33B849B9A421645FF0EB28E23EC43225EC2A0A8089A21D7B2619AA734D421031F161E85E53FE07398B4242F9FE1991249BE13AA78C121466026CE63A84F3481289A29E7C14309149C4E793181B08BB5DC680E366840928B670520FEFBB9B8323B51BAF9E5842F9A99028DB668087C7857A3C7DFA564A762BF5A102CF51D172A9769D2971E88E1841587DA193114BD4A0B67E71AC1020D47FBEE774F4EC559440E6A8BF3D679432992630E490CA84B03DE7433A65D4FF1596B7D19FC0873C65AC2921CA887DAD066CFA90443124727903B342EECBC075BCF63C4F9F584D6BCCC97775C9E5F191EB5CE3E264CAA022404303131925F00B70488884E003911212AE302053A1B1BAB696D291FA54AE88AC431F729D2BB2780F7C2EEF872E7E749D0BBA80A06C29A4F8C829BA1C4E92490AC642D7E481CE3319B525338308D7B90196758B7B1AE72EB09F75DDE5B6BE48A2F02662E584ACF5EE96247390286C64744DA234F13531065E4DA495F4CA7036E557D6F68D60EB09B67EBDD5181830613C7B4713795F02A9A65BAA78BB15D619902EACD5539FC64FB44526903C4070C9C79C7E2153E508F982EF2224BCFABA6EBF56BFCBDD6A1BAF2B5DABC3EB4A87EC2BC4F9E794C6215267FC85436017A71A7757B8A62698D6DF1D18F4418F0F1115E256B1A29AFD2D68BC004F53F0D5228A789579F29EAD1DAD60ECAEA8DDE97D36FEF7A3761A3688AD6DFB525C3032AF93CDCD78DE02DB29CDD1B801246C896468D2B06D932B08A7909406C690E93A9F084BF1CBA161BFD6D8DF80C42A712D461FAD1E3D4EE517204935FCD8547CAEE266E3A910914F331D36899207DEF662E73C70EC51B8666C41A02BD41F8D516368ECA1FB93217B275C15316BB8FC0868A31DBA7A3819F3336020C139F5F3BBC288089F04A683A01682760B4620485408200C2F4F028D4DB934C315E53E8D09B3CAACCDE819E2944415B6DE73063170159DAC7AEFB36899EC980B57F89A8AD66964E03568D3834D7A705949046BA4D118D638F536A49AF5985EC3E1DDB1CEAAC03C36A2D225AABCF2EC4C1AD5088BAE331EEFE545F813C521A36F5F814E40360881579D3A0EB73CCDD05D7B7279513266172A5B33BD61B56E84C6000DAAA1324D9C224D6C0C309348DD742BA3592575BD5FC37B56C6AF0640A16D9D10EDDDF4DDA9714C5BF6BCD2E3FAFB5C631B56BBF4F7AFB55A5DA195326DA85CA3AE6B797961AB2C8079960AD8E08AC4319ED38D8A58D1E24CF272D8E8D564F32251986378BEE8A81555D2562B610A4AE6A0F5E2D228E9054D845465B8295129CE28088D61AD406071B472A9A6AF9B062B5DAF1CAD3E3778A61705F7BB7CA8D6E0056E4A193CDB1F68E437A765C548C248D291CD8F229686DC7E5CDA67D755A32646DD6A220D3C4D7EE38434146584F5B6D67BD9A420FFE38DD2E9DB3DAC6299F77466D1116C29891DA5556D6942B53A36C52B2A2E265ED1D11FCF249E8D743604B3C6D244337B5F0C9DEB9361478CB602F6A5F60A80A7E1F8E3AD9F5F739B0879CBF35BB97D0477442E2307D9CCA8E5ACCD2CA7728CAE8A8625D730B5D6CBEA3946F765A921C096B259EF6C5B33D22E959E4A99A636322A7D4845B42AB3D232A84191CDAC7F6834D29B7C88EBA0EC0F3450A9CD64292484196BF6279FD988D12CD72C075C114E6720645E7B718F0E0E8FB487CA97F368E80911B01E2F87CF5E23A54AA36BABA01BBE5DE88F75FC8124FE3D497E08C9E2C7269A59FEDCF025ECEBD197B68471A3BFE4012C86EE5FD99413E7F2F7BB7CD69E334ED0194E9C03E7EF5D3C5805D82477F460D585D5A7E2BD2577FA3F524DA95CB3BF6D9F5BBE0E42EECC00CDB78B4CD24D05A94EDFCD7CA39CB78177D82CBE55393CAF3E3D67C95A2BB86C537EDFAAEA6DA9023C4DA1FB6B2C6E3F6791794561EE51D5F4EDDF4B9E8D3AAB6E90E68AABB2FE9DB3A8BBC26FD6FFD697F0AD15FC3CB9C613791AA1A1F318D85D85EE2CEEDB6BFB5DC0964A6E27F226C57FEB5AD51873D1A77A21686ABB2E6BAEAD839B15EF17FE0CB05EE09531C57E997F923ABF79FB44B76CFC18164383A0F31A42FD349683DF72C86ACC259F456564D0242A8768D9CA154882392F394D249D115F62B70F42643F0B295EE0CFC3699690A6324E256E19C2296BFDCC44C59755EB678F196D9907E338FBFDC62EB680625295B68FF9BB94B2A092FBA22361B240A8C055E4A2CA9652E5A4F36585741DF19E4085FAAA787B0B61CC104C8CF9843CC036B27D14F01EE6C45F9645043BC87A43B4D53E38A3649E90501418F57CFC8A1C0EC2C59BFF011095CA0C132E0000 , N'6.2.0-61023')
END
