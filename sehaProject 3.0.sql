DROP DATABASE BDSehaCREATE DATABASE BDSehaUSE BDSEHAGO/* CREATE DE TABELAS */
CREATE TABLE tblLembrete (
	LembreteId	int				NOT NULL primary key identity,	Data		date			NULL,	Conteudo	varchar(MAX)	NULL,)
GOCREATE TABLE tblPessoa (
	PessoaId			int				NOT NULL primary key identity, 
	Nome				varchar(50)		NOT NULL,	Email				varchar(100)	NOT NULL unique,	Senha				varchar(25)		NOT NULL,	Permissao_admin		bit				NOT NULL
)
GO

CREATE TABLE tblProfessor (
	CodPessoa			int				NOT NULL primary key references tblPessoa,	NomeGuerra			varchar(50)		NOT NULL,	HorasAula			int				NULL,	ProfessorExiste		bit				NOT NULL,	ProfessorAtivo		bit				NOT NULL,	Observacoes			varchar(MAX)	NULL)
GOCREATE TABLE tblSlot (
	SlotId		int		NOT NULL primary key,	HoraInicio	time	NOT NULL,	Status_slot bit		NOT NULL)GO
CREATE TABLE tblCurso (
	CursoId	int				NOT NULL primary key identity,	Titulo	varchar(100)	NULL)
GO
CREATE TABLE tblDisciplina (
	DisciplinaId	int				NOT NULL primary key identity,	CodCurso		int				NOT NULL references tblCurso,	Nome			varchar(100)	NULL,	QtdAulas		int				NULL,	Semestre		int				NULL,	Sigla			varchar(3)		NULL)
GOCREATE TABLE tblDisponibilidade (
	CodProfessor	int NOT NULL references tblProfessor,	CodSlot			int	NOT NULL references tblSlot,	PRIMARY KEY (CodProfessor, CodSlot) )
GO
CREATE TABLE tblAtribuicao (
	CodProfessor	int NOT NULL references tblProfessor,	CodDisciplina	int	NOT NULL unique references tblDisciplina,	PRIMARY KEY (CodProfessor, CodDisciplina) 
)
GO
CREATE TABLE tblResultado (
	CodProfessor	int			NOT NULL,  	CodDisciplina	int			NOT NULL,	CodSlot			int			NOT NULL,	PRIMARY KEY (CodProfessor, CodDisciplina, CodSlot),	CONSTRAINT fkCodAtribuicaoResultado 		FOREIGN KEY					(CodProfessor, CodDisciplina) 		REFERENCES tblAtribuicao	(CodProfessor, CodDisciplina),	CONSTRAINT fkDisponibilidadeResultado 		FOREIGN KEY						(CodProfessor, CodSlot) 		REFERENCES tblDisponibilidade	(CodProfessor, CodSlot))
GO/* PROCEDURES	Modelo:		1- Procedure		2- Consulta a(as) tabelas pertencentes a procedure		3- Teste da procedure*/--Lembrete	--Insert	CREATE PROCEDURE ArmazenaLembrete(
		@data		date,
		@conteudo	varchar(MAX)
	)AS
	BEGIN
		INSERT INTO tblLembrete VALUES (@data, @conteudo)
	END	EXEC ArmazenaLembrete '2016-08-04', 'conteudo teste'
	SELECT * FROM tblLembrete
	--Update	CREATE PROCEDURE AlteraLembrete(		@id int,		@conteudo varchar(MAX)	)AS	BEGIN 		UPDATE tblLembrete SET			Conteudo = @conteudo		WHERE LembreteId = @id	END	EXEC AlteraLembrete 1, 'Conteudo alterado'	SELECT * FROM tblLembrete	--Delete	CREATE PROCEDURE ApagaLembrete(		@id int	)AS	BEGIN		DELETE FROM tblLembrete 
		WHERE LembreteId = @id	END	EXEC ApagaLembrete 1	SELECT * FROM tblLembrete--Pessoa
	--Insert
	CREATE PROCEDURE ArmazenaPessoa(
		@nome				varchar(50),		@email				varchar(100),		@senha				varchar(25)		--por padrão toda pessoa começa com permissao_admin = 0
	)AS
	BEGIN
		INSERT INTO tblPessoa VALUES (@nome, @email, @senha, 0)
	END

	EXEC ArmazenaPessoa 'Davi Augusto Leme dos Santos', 'davi@mail.com', '12345'
	SELECT * FROM tblPessoa

	--Outros
	CREATE PROCEDURE ValidaLogin(		@email varchar(100),		@senha varchar(25)
	)AS
	BEGIN
		SELECT * FROM tblPessoa WHERE Email = @email and Senha = @senha
	END
	EXEC ValidaLogin 'davi@mail.com', '12345'

	--Avaliar no model se haverá mudança no método UpdateSenha e alterar na proc depois
	CREATE PROCEDURE AlteraSenha(
		@id int,
		@senhaNova varchar(25)
	)AS
	BEGIN
		UPDATE tblPessoa SET
			Senha = @senhaNova
		WHERE PessoaId = @id	
	END

	EXEC AlteraSenha 1, 'senhanova'
	SELECT * FROM tblPessoa

--Professor
	--Insert
	CREATE PROCEDURE ArmazenaProfessor(
		@nome varchar(50),		@email varchar(100),		@senha varchar(25),		@nomeGuerra	varchar(50),		--por padrão o professor começa com 0 horas aula atribuidas		@professorExiste bit		--por padrão todo professor já é ativo, ProfessorAtivo = 1	)AS	BEGIN		EXEC ArmazenaPessoa @nome, @email, @senha		INSERT INTO tblProfessor VALUES (@@IDENTITY, @nomeGuerra, 0, @professorExiste, 1, '')	END	EXEC ArmazenaProfessor 'Tobias', 'tobias@mail.com', '789756', 'TobiasGuerra', 1
	SELECT * FROM tblProfessor

	--Update
	CREATE PROCEDURE AlteraProfessor 
	(
		@id int,
		@nome varchar(50),		@email varchar(100),		@nomeGuerra varchar(50),
		@professorExiste bit,
		@professorAtivo bit
	)
	AS
	BEGIN
		UPDATE tblPessoa SET
			Nome = @nome,
			Email = @email
		WHERE PessoaId = @id

		UPDATE tblProfessor SET 
			NomeGuerra = @nomeGuerra,
			ProfessorExiste = @professorExiste,
			ProfessorAtivo = @professorAtivo  
		WHERE CodPessoa = @id
	END

	EXEC AlteraProfessor 2, 'Tobias', 'tobias@mail.com', 'TobiasEditado', 1, 1
	SELECT * FROM tblProfessor

	--Delete
	CREATE PROCEDURE ApagaProfessor 
	(
		@id int
	)
	AS
	BEGIN
		DELETE FROM tblProfessor 
		WHERE CodPessoa = @id
		DELETE FROM tblPessoa 
		WHERE PessoaId = @id
	END

	EXEC ApagaProfessor 2
	SELECT * FROM tblProfessor
	SELECT * FROM tblPessoa


--Slots: Fixos e inseridos manualmente.
	CREATE PROCEDURE ArmazenaSlot(		@slotId		int,		@horaInicio time		--por padrão todo os slots começam inativos, Status_slot = 0	)AS	BEGIN		INSERT INTO tblSlot VALUES (@slotId, @horaInicio, 0)	END	CREATE PROCEDURE PopulaTblSlots	AS	BEGIN		EXEC ArmazenaSlot 1, '13:10:00'		EXEC ArmazenaSlot 2, '15:00:00'		EXEC ArmazenaSlot 3, '16:50:00'		EXEC ArmazenaSlot 4, '13:10:00'		EXEC ArmazenaSlot 5, '15:00:00'		EXEC ArmazenaSlot 6, '16:50:00'		EXEC ArmazenaSlot 7, '13:10:00'		EXEC ArmazenaSlot 8, '15:00:00'		EXEC ArmazenaSlot 9, '16:50:00'		EXEC ArmazenaSlot 10, '13:10:00'		EXEC ArmazenaSlot 11, '15:00:00'		EXEC ArmazenaSlot 12, '16:50:00'		EXEC ArmazenaSlot 13, '13:10:00'		EXEC ArmazenaSlot 14, '15:00:00'		EXEC ArmazenaSlot 15, '16:50:00'	END				EXEC PopulaTblSlots	SELECT * FROM tblSlot--Curso	--Insert	CREATE PROCEDURE ArmazenaCurso(
		@titulo	varchar(100),
		@turno int
		--por definição para o inter, o turno será o da tarde
	)AS
	BEGIN
		INSERT INTO tblCurso VALUES (@titulo)
		INSERT INTO tblCursoTurno VALUES (@@IDENTITY, @turno)
	END
	
	EXEC ArmazenaCurso 'Análise e desenvolvimento de sistemas', 'Tarde'
	SELECT * FROM tblCurso
	DElete from tblCurso
	
	--Update
	CREATE PROCEDURE AlteraCurso
	(
		@id int,
		@titulo varchar(100),		@turno varchar(10)	)
	AS
	BEGIN
		UPDATE tblCurso SET
			Titulo = @titulo,
			Turno = @turno
		WHERE CursoId = @id
	END

	EXEC AlteraCurso 1, 'ADS', 'Manhã'
	SELECT * FROM tblCurso

	--Delete
	CREATE PROCEDURE ApagaCurso 
	(
		@id int
	)
	AS
	BEGIN
		DELETE FROM tblCurso 
		WHERE CursoId = @id
	END

	EXEC ApagaCurso 1
	SELECT * FROM tblCurso	

--Disciplina 	--Insert	CREATE PROCEDURE ArmazenaDisciplina(		@codCurso		int,		@nome			varchar(100),		@qtdAulas		int,		@semestre		int,		@sigla			varchar(3)	)AS	BEGIN		INSERT INTO tblDisciplina VALUES (@CodCurso, @Nome, @QtdAulas, @Semestre, @Sigla)	END	EXEC ArmazenaCurso 'Info para negócios', 'Noite'	EXEC ArmazenaDisciplina 2, 'Programação Orientada a Objetos', 4, 4, 'POO'	SELECT * FROM tblDisciplina	--Update	CREATE PROCEDURE AlteraDisciplina(		@id				int,		@codCurso		int,		@nome			varchar(100),		@qtdAulas		int,		@semestre		int,		@sigla			varchar(3)	)AS	BEGIN		UPDATE tblDisciplina SET		CodCurso = @codCurso,		Nome = @nome,		QtdAulas = @qtdAulas,		Semestre = @semestre,		Sigla = @sigla	WHERE DisciplinaId = @id	END	EXEC AlteraDisciplina 1, 2, 'Cálculo', 2, 4, 'Cal'	SELECT * FROM tblDisciplina	--Delete	CREATE PROCEDURE ApagaDisciplina 
	(
		@id int
	)
	AS
	BEGIN
		DELETE FROM tblDisciplina 
		WHERE DisciplinaId = @id
	END

	EXEC ApagaDisciplina 1
	SELECT * FROM tblDisciplina		--Atribuicao	--Insert	CREATE PROCEDURE ArmazenaAtribuicao(		@codProfessor		int,		@codDisciplina		int	)AS	BEGIN		INSERT INTO tblAtribuicao VALUES (@codProfessor, @codDisciplina)	END	--Delete	CREATE PROCEDURE ApagaAtribuicao
	(
		@ProfessorId int,
		@DisciplinaId int
	)
	AS
	BEGIN
		DELETE FROM tblAtribuicao
		WHERE CodProfessor = @ProfessorId AND CodDisciplina = @DisciplinaId
	END/* VIEWS	Modelo:		1- Select para analisar os campos da tabela		2- View		3- Teste da View*/--Lembretes	SELECT * FROM tblLembrete	CREATE VIEW ViewLembretes	AS	SELECT LembreteId,		   Data,		   Conteudo				FROM tblLembrete	SELECT * FROM ViewLembretes--Professores	SELECT * FROM tblPessoa	SELECT * FROM tblProfessor	CREATE VIEW ViewProfessores	AS	SELECT p.PessoaId,		   p.Nome,		   p.Email,		   p.Senha,		   p.Permissao_admin,		   prof.NomeGuerra,		   prof.HorasAula,		   prof.ProfessorExiste,		   prof.ProfessorAtivo,		   prof.Observacoes					FROM tblPessoa p, tblProfessor prof	Where p.PessoaId = prof.CodPessoa	SELECT * FROM ViewProfessores--Cursos	SELECT * FROM tblCurso	CREATE VIEW ViewCursos	AS	SELECT CursoId,		   Titulo,		   Turno				FROM tblCurso	SELECT * FROM ViewCursos--Disciplinas: Possivelmente tirar os objetos "curso"	SELECT * FROM tblCurso	SELECT * FROM tblDisciplina	CREATE VIEW ViewDisciplinas	AS	SELECT d.CodCurso,		   d.DisciplinaId,		   d.Nome,		   d.QtdAulas,		   d.Semestre,		   d.Sigla				FROM tblDisciplina d	SELECT * FROM ViewDisciplinas--Atribuicao	SELECT * FROM tblProfessor	SELECT * FROM tblDisciplina	SELECT * FROM tblAtribuicao	CREATE VIEW ViewAtribuicoes	AS	SELECT a.CodProfessor,		   a.CodDisciplina	FROM tblAtribuicao a	INSERT INTO tblAtribuicao VALUES(3, 1)	SELECT * FROM ViewAtribuicoes--Disponibilidade--Resultado