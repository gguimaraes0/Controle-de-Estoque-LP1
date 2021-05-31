
USE [master]
GO
/****** Object:  Database [N2_curriculo]    Script Date: 05/04/2021 10:03:08 ******/
CREATE DATABASE [Controle_De_Estoque]

USE [Controle_De_Estoque]

drop table Fornecedores
CREATE TABLE Fornecedores(
	CodFornecedor int IDENTITY(1000,1)  NOT NULL PRIMARY KEY,
    CNPJFornecedor varchar(255)  NOT NULL,
	NomeFornecedor varchar(255) NULL,
	EmailFornecedor varchar(255) NULL,
	TelefoneFornecedor varchar (255) NULL,
	NumeroFornecedor varchar(255) NULL,
	ComplementoFornecedor varchar (255) NULL,
	CEPFornecedor varchar (255) NULL
)
Go


CREATE TABLE Usuarios(
	CodUsuario int IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	EmailUsuario varchar(255) NULL,
	SenhaUsuario varchar(30) NULL,
	NomeUsuario varchar(255) NULL,
	NumeroUsuario varchar(255) NULL,
	ComplementoUsuario varchar (255) NULL,
	TelefoneUsuario varchar(255) NULL,
	CEPUsuario varchar(255) NULL
)
Go

CREATE TABLE Clientes(
	CodCliente int IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	EmailCliente varchar(255) NULL,
	NomeCliente varchar(255) NULL,
	Data_NascimentoCliente varchar(255) NULL,
	NumeroCliente varchar(255) NULL,
	ComplementoCliente varchar (255) NULL,
	TelefoneCliente varchar(255) NULL,
	CEPCliente varchar(255) NULL,
	CNPJCliente varchar(255) NULL,
	CPFCliente varchar(255) NULL,
)
Go

CREATE TABLE Tipos_Produtos(
	CodTipo int IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	Descricao varchar(255) NOT NULL,
)
Go

Create Table Produtos(
	CodProduto int IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	TipoProduto int FOREIGN KEY REFERENCES Tipos_Produtos (CodTipo) NULL,
	
	CorProduto varchar(255) NULL,
	TamanhoProduto varchar(255) NULL,
	DescricaoProduto varchar(255) NULL,
	QuantidadeDisponivelProduto varchar (255) NULL,
	FotoProduto varbinary(max) Null,
	CodFornecedor int FOREIGN KEY REFERENCES Fornecedores (CodFornecedor) Null
)
Go

--drop table [Compras_Vendas]
CREATE TABLE [Compras_Vendas](
	ID int IDENTITY(1001,1) NOT NULL PRIMARY KEY,
	Tipo varchar(255) NOT NULL,
	[Data] varchar(16) NOT NULL,
	CodProdutos int FOREIGN KEY REFERENCES Produtos(CodProduto) Null,
	Quantidade varchar(10) NOT NULL,
	CodCliente int FOREIGN KEY REFERENCES Clientes (CodCliente) Null,
	CodFornecedor int FOREIGN KEY REFERENCES Fornecedores (CodFornecedor) Null,
	CodUsuario int FOREIGN KEY REFERENCES Usuarios (CodUsuario) Null
)
Go

CREATE TABLE Cores_Produtos(
	CodCores int IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	Cor varchar(255) NOT NULL,
)
Go
--====================================================================================================================================================================
--====================================================================== PROCEDURES =======================================================================
--====================================================================================================================================================================
CREATE procedure validaCPF (@CPF as varchar(11) )
as
BEGIN
-- declaração das variáveis locais
declare @n int
declare @soma int
declare @multi int
declare @digito1 int
declare @digito2 int

if len(rtrim(ltrim(@CPF))) <> 11
begin
 Return 0 -- sai da stored procedure caso o CPF esteja no tamanho incorreto
end

-- calculando o primeiro digito...
set @soma = 0
set @multi = 10

WHILE (@n <= 9 )
begin
 set @soma = @soma + cast(SUBSTRING(@cpf, @n, 1) as int) * @multi;
 set @multi = @multi -1;
 set @n = @n + 1
end

set @soma = @soma % 11 -- % -> módulo

if @soma <=1
set @digito1 = 0

else
 set @digito1 = 11 - @soma

--calculando o segundo digito...
set @soma = 0
set @multi = 11
set @n = 1

WHILE (@n <= 9 )
begin
 set @soma = @soma + cast(SUBSTRING(@cpf, @n, 1) as int) * @multi;
 set @multi = @multi -1;
 set @n = @n + 1
end

set @soma = (@soma + @digito1 * @multi);
set @soma = @soma % 11 -- % -> módulo

if @soma <=1
 set @digito2 = 0

else
 set @digito2 = 11 - @soma

--comparando os digitos digitados com os calculados...
--print 'digito 1: ' + cast( @digito1 as varchar)
--print 'digito 2: ' + cast( @digito2 as varchar)
--print char(13) -- pula uma linha!!!

if (cast(SUBSTRING(@cpf, 10, 1) as int) = @digito1) and
 (cast(SUBSTRING(@cpf, 11, 1) as int) = @digito2)
 Return 1

else
 Return 0

END

 --====================================================================================================================================================================
 --====================================================================================================================================================================
 --====================================================================================================================================================================
create procedure spDelete
(
 @id int ,
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = ' delete ' + @tabela +
 ' where id = ' + cast(@id as varchar(max))
 exec(@sql)
end
GO
--====================================================================================================================================================================
 --====================================================================================================================================================================
 --====================================================================================================================================================================

 create procedure spConsultaLogin
(
 @EmailUsuario  varchar(max),
 @SenhaUsuario varchar(max)
)
as
begin
select * from fnc_ConsultaLogin(@EmailUsuario, @SenhaUsuario)
end

create procedure spConsulta
(
 @id int ,
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = 'select * from ' + @tabela +
 ' where id = ' + cast(@id as varchar(max))
 exec(@sql)
 end 
 GO

create procedure spListagem
(
 @tabela varchar(max),
 @ordem varchar(max))
as
begin
 exec('select * from ' + @tabela +
 ' order by ' + @ordem)
end
GO


create procedure spInsert_Usuarios
(
 @EmailUsuario varchar(max),
 @NomeUsuario varchar(max),
 @CEPUsuario varchar(max),
 @NumeroUsuario varchar(max),
 @ComplementoUsuario varchar(max),
 @TelefoneUsuario varchar (max),
 @SenhaUsuario varchar(max)
)
as
begin
 insert into Usuarios
 ( 
EmailUsuario ,
NomeUsuario ,
CEPUsuario ,
NumeroUsuario ,
ComplementoUsuario ,
TelefoneUsuario ,
SenhaUsuario)
 values
 (@EmailUsuario, @NomeUsuario, @CEPUsuario, @NumeroUsuario,@ComplementoUsuario,@TelefoneUsuario, @SenhaUsuario)
end
GO

create procedure spUpdate_Usuarios
(
 @CodUsuario int, -- ???

 @EmailUsuario varchar(max),
 @NomeUsuario varchar(max),
 @CEPUsuario varchar(max),
 @NumeroUsuario varchar(max),
 @ComplementoUsuario varchar(max),
 @TelefoneUsuario varchar (max),
 @SenhaUsuario varchar(max)
)
as
begin
 update Usuarios set
 EmailUsuario = @EmailUsuario,
 NomeUsuario = @NomeUsuario,
 CEPUsuario = @CEPUsuario,
 NumeroUsuario = @NumeroUsuario,
 ComplementoUsuario = @ComplementoUsuario,
 TelefoneUsuario = @TelefoneUsuario,
 SenhaUsuario = @SenhaUsuario
 where CodUsuario = @CodUsuario
end
GO

create procedure spInsert_Fornecedores
(
 @CNPJFornecedor varchar(max),
 @EmailFornecedor varchar(max),
 @NomeFornecedor varchar(max),
 @CEPFornecedor varchar(max),
 @NumeroFornecedor varchar(max),
 @ComplementoFornecedor varchar(max),
 @TelefoneFornecedor varchar (max) 
)
as
begin
 insert into Fornecedores
 (CNPJFornecedor, EmailFornecedor, NomeFornecedor,CEPFornecedor,NumeroFornecedor,
  ComplementoFornecedor,TelefoneFornecedor)
 values
 (@CNPJFornecedor, @EmailFornecedor,@NomeFornecedor,@CEPFornecedor,@NumeroFornecedor,
 @ComplementoFornecedor,@TelefoneFornecedor)
end
GO

create procedure spUpdate_Fornecedores
(
 @CodFornecedor int, --- ?
 @CNPJFornecedor varchar(max),
 @EmailFornecedor varchar(max),
 @NomeFornecedor varchar(max),
 @CEPFornecedor varchar(max),
 @NumeroFornecedor varchar(max),
 @ComplementoFornecedor varchar(max),
 @TelefoneFornecedor varchar (max)
)
as
begin
 update Fornecedores set
 CNPJFornecedor = @CNPJFornecedor,
 EmailFornecedor = @EmailFornecedor,
 NomeFornecedor = @NomeFornecedor,
 CEPFornecedor = @CEPFornecedor,
 NumeroFornecedor = @NumeroFornecedor,
 ComplementoFornecedor = @ComplementoFornecedor,
 TelefoneFornecedor = @TelefoneFornecedor
 where CodFornecedor = @CodFornecedor
end
GO

create procedure spInsert_Clientes
(
 @CNPJCliente varchar(max),
 @CPFCliente varchar(max),
 @EmailCliente varchar(max),
 @NomeCliente varchar(max),
 @Data_NascimentoCliente varchar(max),
 @CEPCliente varchar(max),
 @NumeroCliente varchar(max),
 @ComplementoCliente varchar(max),
 @TelefoneCliente varchar (max)
)
as
begin
 insert into Clientes
 (CNPJCliente,CPFCliente, EmailCliente, NomeCliente,Data_NascimentoCliente,CEPCliente,NumeroCliente, ComplementoCliente,TelefoneCliente)
 values
 (@CNPJCliente,@CPFCliente, @EmailCliente, @NomeCliente,@Data_NascimentoCliente, @CEPCliente,@NumeroCliente,@ComplementoCliente,@TelefoneCliente)
end
GO

create procedure spUpdate_Clientes
(
 @CodCliente int, ---- ???
 @CNPJCliente varchar(max),
 @CPFCliente varchar(max),
 @EmailCliente varchar(max),
 @NomeCliente varchar(max),
 @Data_NascimentoCliente varchar(max),
 @CEPCliente varchar(max),
 @NumeroCliente varchar(max),
 @ComplementoCliente varchar(max),
 @TelefoneCliente varchar (max)
)
as
begin
 update Clientes set
 CPFCliente = @CPFCliente,
 CNPJCliente = @CNPJCliente,
 EmailCliente = @EmailCliente,
 NomeCliente = @NomeCliente,
 Data_NascimentoCliente = @Data_NascimentoCliente,
 CEPCliente = @CEPCliente,
 NumeroCliente = @NumeroCliente,
 ComplementoCliente = @ComplementoCliente,
 TelefoneCliente = @TelefoneCliente
where CodCliente = @CodCliente
end
GO

create procedure spInsert_Produtos
(
 @CorProduto varchar(max),
 @TipoProduto int,
 @TamanhoProduto varchar(max),
 @DescricaoProduto varchar(max),
 @QuantidadeDisponivelProduto varchar(max),
 @CodFornecedor int,
 @FotoProduto varbinary(max)
)
as
begin
 insert into Produtos
 values
 (@CorProduto,@TipoProduto, @TamanhoProduto, @DescricaoProduto,@QuantidadeDisponivelProduto, @CodFornecedor,@FotoProduto)
end
GO

create procedure spUpdate_Produtos
(
 @CodProduto int,
 @CorProduto varchar(max),
 @TipoProduto int,
 @TamanhoProduto varchar(max),
 @DescricaoProduto varchar(max),
 @QuantidadeDisponivelProduto varchar(max),
 @CodFornecedor int,
 @FotoProduto varbinary(max)
)
as
begin
 update Produtos set
 CorProduto = @CorProduto,
 TipoProduto = @TipoProduto,
 TamanhoProduto = @TamanhoProduto,
 DescricaoProduto = @DescricaoProduto,
 QuantidadeDisponivelProduto = @QuantidadeDisponivelProduto,
 CodFornecedor = @CodFornecedor,
 FotoProduto = @FotoProduto 
 where CodProduto = @CodProduto
end
GO

create procedure spInsert_Compras_Vendas
(
 @Data varchar(max),
 @CodProdutos int,
 @Quantidade varchar(max),
 @CodCliente int,
 @CodFornecedor int,
 @CodUsuario int,
 @Tipo varchar(max)
)
as
begin
 insert into [Compras_Vendas]
 ([Data], CodProdutos,Quantidade,CodCliente,CodFornecedor,CodUsuario, Tipo)
 values
 (@Data, @CodProdutos, @Quantidade,@CodCliente, @CodFornecedor, @CodUsuario,@Tipo)
end
GO

create procedure spInsert_Cores
(
 @Cor varchar(max)
)
as

begin
 insert into [Cores_Produtos]
 (Cor)
 values
 (@Cor)
end
GO

create procedure spInsert_Categoria
(
 @Descricao varchar(max)
)
as
begin
 insert into [Tipos_Produtos]
 (Descricao)
 values
 (@Descricao)
end
GO

create procedure spUpdate_Tipos
(
 @CodTipo int,
 @Descricao varchar(max)
)
as
begin
 update [Tipos_Produtos] set
 Descricao =  @Descricao
 where CodTipo = @CodTipo
end
GO


--====================================================================================================================================================================
--========================================================================== TRIGGERS ================================================================================
--====================================================================================================================================================================

create trigger trg_DeleteUsuario on Usuarios
instead of delete as
begin
	set nocount on
	declare @CodUsuario int
	set @CodUsuario = (select CodUsuario FROM deleted)
	if (select count(*) from DELETED) >= 1
	begin 
		delete from Compras_Vendas where CodUsuario = @CodUsuario
		delete from Usuarios where CodUsuario = @CodUsuario
	end
	set nocount off
end

create trigger trg_DeleteCliente on Clientes
instead of delete as
begin
	set nocount on
	declare @CodCliente int
	set @CodCliente = (select CodCliente FROM deleted)
	if (select count(*) from DELETED) >= 1
	begin 
		delete from Compras_Vendas where CodCliente = @CodCliente
		delete from Clientes where CodCliente = @CodCliente
	end
	set nocount off
end

create trigger trg_DeleteFornecedores on Fornecedores
instead of delete as
begin
	set nocount on
	declare @CodFornecedor int
	set @CodFornecedor = (select CodFornecedor FROM deleted)
	if (select count(*) from DELETED) >= 1
	begin 
		delete from Compras_Vendas where CodFornecedor = @CodFornecedor
		delete from Produtos where CodFornecedor = @CodFornecedor
		delete from Fornecedores where CodFornecedor = @CodFornecedor
	end
	set nocount off
end

create trigger trg_DeleteProduto on Produtos
instead of delete as
begin
	set nocount on
	declare @CodProduto int
	set @CodProduto = (select CodProduto FROM deleted)
	if (select count(*) from DELETED) >= 1
	begin 
		delete from Compras_Vendas where CodProduto = @CodProduto
		delete from Produtos where CodProduto = @CodProduto
	end
	set nocount off
end


create trigger trg_VerificaCPF_Usuario on Usuarios
for insert as
begin

    set nocount on

    declare @CPFUsuario varchar(max)
	declare @ErroString varchar(60)

	set @CPFUsuario = (select CPFUsuario from inserted)
	
    if((select count(CPFUsuario) from Usuarios where CPFUsuario = @CPFUsuario) >= 2)
    begin
		set @ErroString = 'O CPF ' + @CPFUsuario +' já foi cadastrado!'
        raiserror(@ErroString,16,1)
        rollback tran
        return
    end

    set nocount off
end

create trigger trg_VerificaCPF_Clientes on Clientes
for insert as
begin

    set nocount on

    declare @CPFCliente varchar(max)
	declare @ErroString varchar(60)

	set @CPFCliente = (select CPFCliente from inserted)

    if((select count(CPFCliente) from Clientes where CPFCliente = @CPFCliente) >= 2)
    begin
		set @ErroString = 'O CPF ' + @CPFCliente +' já foi cadastrado!'
        raiserror(@ErroString,16,1)
        rollback tran
        return
    end

    set nocount off
end

create trigger trg_VerificaCNPJ_Clientes on Clientes
for insert as
begin

    set nocount on

    declare @CNPJCliente varchar(max)
	declare @ErroString varchar(60)

	set @CNPJCliente = (select CNPJCliente from inserted)

    if((select count(CNPJCliente) from Clientes where CNPJCliente = @CNPJCliente) >= 2)
    begin
		set @ErroString = 'O CNPJ ' + @CNPJCliente +' já foi cadastrado!'
        raiserror(@ErroString,16,1)
        rollback tran
        return
    end

    set nocount off
end


create trigger trg_VerificaCNPJ_Fornecedores on Fornecedores
for insert as
begin

    set nocount on

    declare @CNPJFornecedor varchar(max)
	declare @ErroString varchar(60)

	set @CNPJFornecedor = (select CNPJFornecedor from inserted)

    if((select count(CNPJFornecedor) from Fornecedores where CNPJFornecedor = @CNPJFornecedor) >= 2)
    begin
		set @ErroString = 'O CNPJ ' + @CNPJFornecedor +' já foi cadastrado!'
        raiserror(@ErroString,16,1)
        rollback tran
        return
    end

    set nocount off
end

create trigger trg_VerificaEmail_Usuario on Usuarios
for insert as
begin

    set nocount on

    declare @EmailUsuario varchar(max)
    declare @ErroString varchar(60)

    set @EmailUsuario = (select EmailUsuario from inserted)

    if((select count(EmailUsuario) from Usuarios where EmailUsuario = @EmailUsuario) >= 2)
    begin
        set @ErroString = 'O Email ' + @EmailUsuario +' já foi cadastrado!'
        raiserror(@ErroString,16,1)
        rollback tran
        return
    end

    set nocount off
end

Create trigger trg_SomaProduto on Compras_Vendas
after insert as
begin
	set nocount on
	declare @CodProduto int
	declare @QuantidadeNova int
	declare @QuantidadeAntiga int
	declare @QuantidadeConta int
	declare @tipo varchar

	set @CodProduto = (select CodProduto FROM inserted)
	set @QuantidadeConta = (select Quantidade FROM inserted)
	set @QuantidadeAntiga = (select QuantidadeDisponivelProduto FROM Produtos where CodProduto = @CodProduto)
	set @tipo = (select Tipo FROM inserted)

	if ((select Tipo FROM inserted) = 'Venda')
	begin
	set @QuantidadeNova = @QuantidadeAntiga - @QuantidadeConta
	end
		else
	begin
	set @QuantidadeNova = @QuantidadeAntiga + @QuantidadeConta
	end


	if(@QuantidadeNova <= 0)
	begin
	set @QuantidadeNova = 0;
	end

	begin 
		update Produtos set QuantidadeDisponivelProduto = @QuantidadeNova where CodProduto = @CodProduto		
	end
	set nocount off
end


create function fnc_ConsultaLogin (@EmailUsuario varchar(max), @SenhaUsuario varchar(max))
returns @tbl_ConsultaLogin table
(
	EmailUsuario varchar(max),
	SenhaUsuario	varchar(max)
)
as
begin	
	begin
		insert into @tbl_ConsultaLogin
		select EmailUsuario, SenhaUsuario from Usuarios where EmailUsuario = @EmailUsuario and SenhaUsuario = @SenhaUsuario
	end
	return
end