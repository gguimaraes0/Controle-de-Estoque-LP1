
USE [master]
GO
/****** Object:  Database [N2_curriculo]    Script Date: 05/04/2021 10:03:08 ******/
CREATE DATABASE [Controle_De_Estoque]
GO
USE [Controle_De_Estoque]

--drop table Fornecedores
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

INSERT INTO Tipos_Produtos values ('CALÇA');

INSERT INTO Tipos_Produtos values ('CAMISETA');

INSERT INTO Tipos_Produtos values ('JAQUETA');

INSERT INTO Tipos_Produtos values ('GORRO');

INSERT INTO Tipos_Produtos values ('MEIA');

INSERT INTO Tipos_Produtos values ('CUECA');



Create Table Produtos(
	CodProduto int IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	TipoProduto int FOREIGN KEY REFERENCES Tipos_Produtos (CodTipo) NULL,
	CorProduto varchar(255) NULL,
	TamanhoProduto varchar(255) NULL,
	DescricaoProduto varchar(255) NULL,
	QuantidadeDisponivelProduto varchar (255) NULL,
	CodFornecedor int FOREIGN KEY REFERENCES Fornecedores (CodFornecedor) Null
)
Go
--drop table [Compras_Vendas]
CREATE TABLE [Compras_Vendas](
	ID int IDENTITY(1001,1) NOT NULL PRIMARY KEY,
	Tipo varchar(255) NOT NULL,
	[Data] varchar(16) NOT NULL,
	CodProduto int FOREIGN KEY REFERENCES Produtos(CodProduto) Null,
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


INSERT INTO Cores_Produtos values ('AZUL');
INSERT INTO Cores_Produtos values ('AMARELO');
INSERT INTO Cores_Produtos values ('VERMELHO');
INSERT INTO Cores_Produtos values ('AZUL');
INSERT INTO Cores_Produtos values ('PRETO');
INSERT INTO Cores_Produtos values ('BRANCO');
INSERT INTO Cores_Produtos values ('VERDE');
INSERT INTO Cores_Produtos values ('CINZA');

 --====================================================================================================================================================================
 --========================================================FUNCTION========================================================================
 --====================================================================================================================================================================
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
go
--Function Utilizada para realizar a consulta do login, isto é, a verificação do login já existir ou não no banco
--====================================================================================================================================================================
--====================================================================== PROCEDURES =======================================================================
--====================================================================================================================================================================
create procedure spDelete
(
 @id int ,
 @codName varchar(max),
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = ' delete ' + @tabela +
 ' where '+@codName+' = ' + cast(@id as varchar(max))
 exec(@sql)
end
GO
--Sp para deletar qualquer dado do banco, sendo passado a tabela como parametro e o id que se deseja apagar naquela tabela
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
GO
--sp que executa em conjunto com a function para consultar login


create procedure spListagemCor
as
begin
 exec('select * from Cores_Produtos order by CodCores');
end
GO
--sp que lista todas as cores

create procedure spListagemTipo
as
begin
 exec('select * from Tipos_Produtos order by CodTipo');
end
GO
--sp que lista todas os tipos de produtos

create procedure spListagemCliente
as
begin
 exec('select * from Clientes');
end
GO
--sp que lista todas os clientes cadastrados
create procedure spListagemFornecedor
as
begin
 exec('select * from Fornecedores order by CodFornecedor');
end
GO
--sp que lista todas os fornecedores cadastrados
create procedure spListagemUsuario
as
begin
 exec('select * from Usuarios' )
end
GO
--sp que lista todas os usuarios cadastrados
create procedure spListagemProduto
as
begin
 exec('select * from Produtos' )
end
GO
--sp que lista todas os produtos cadastrados
create procedure spListagemCompras_Vendas
as
begin
 exec('select * from Compras_Vendas' )
end
GO
--sp que lista todas as compras ou vendas cadastrados
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
--sp que insere novas informações dos usuarios cadastrados
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
--sp que atualiza as informações dos usuarios cadastrados
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
--sp que insere novas informações dos fornecedores cadastrados
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
--sp que atualiza as informações dos fornecedores cadastrados
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
--sp que insere novas informações dos cliente cadastrados
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
--sp que atualiza as informações dos cliente cadastrados
create procedure spInsert_Produtos
(
 @CorProduto varchar(max),
 @TipoProduto int,
 @TamanhoProduto varchar(max),
 @DescricaoProduto varchar(max),
 @QuantidadeDisponivelProduto varchar(max),
 @CodFornecedor int
)
as
begin
 insert into Produtos
 values
 (@CorProduto,@TipoProduto, @TamanhoProduto, @DescricaoProduto,@QuantidadeDisponivelProduto, @CodFornecedor)
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
 @CodFornecedor int
)
as
begin
 update Produtos set
 CorProduto = @CorProduto,
 TipoProduto = @TipoProduto,
 TamanhoProduto = @TamanhoProduto,
 DescricaoProduto = @DescricaoProduto,
 QuantidadeDisponivelProduto = @QuantidadeDisponivelProduto,
 CodFornecedor = @CodFornecedor
 where CodProduto = @CodProduto
end
GO

create procedure spInsert_Compras_Vendas
(
 @Data varchar(max),
 @CodProduto int,
 @Quantidade varchar(max),
 @CodCliente int,
 @CodFornecedor int,
 @CodUsuario int,
 @Tipo varchar(max)
)
as
begin
 insert into [Compras_Vendas]
 ([Data], CodProduto,Quantidade,CodCliente,CodFornecedor,CodUsuario, Tipo)
 values
 (@Data, @CodProduto, @Quantidade,@CodCliente, @CodFornecedor, @CodUsuario,@Tipo)
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
GO

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
GO

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
GO

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
GO

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
GO

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
GO

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
GO

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
GO

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
GO