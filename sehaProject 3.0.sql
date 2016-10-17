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
	CursoId	int				NOT NULL primary key identity,	Titulo	varchar(100)	NULL,	Turno	int				NULL)
GO
CREATE TABLE tblDisciplina (
	DisciplinaId	int				NOT NULL primary key identity,	CodCurso		int				NOT NULL references tblCurso,	Nome			varchar(100)	NULL,	QtdAulas		int				NULL,	Semestre		int				NULL,	Sigla			varchar(3)		NULL)
GOCREATE TABLE tblDisponibilidade (
	CodProfessor	int NOT NULL references tblProfessor,	CodSlot			int			NOT NULL references tblSlot,	PRIMARY KEY (CodProfessor, CodSlot) )
GO
CREATE TABLE tblAtribuicao (
	CodProfessor	int NOT NULL references tblProfessor,	CodDisciplina	int			NOT NULL references tblDisciplina,	PRIMARY KEY (CodProfessor, CodDisciplina) 
)
GO
CREATE TABLE tblResultado (
	CodProfessor	int			NOT NULL,  	CodDisciplina	int			NOT NULL,	CodSlot			int			NOT NULL,	PRIMARY KEY (CodProfessor, CodDisciplina, CodSlot),	CONSTRAINT fkCodAtribuicaoResultado 		FOREIGN KEY					(CodProfessor, CodDisciplina) 		REFERENCES tblAtribuicao	(CodProfessor, CodDisciplina),	CONSTRAINT fkDisponibilidadeResultado 		FOREIGN KEY						(CodProfessor, CodSlot) 		REFERENCES tblDisponibilidade	(CodProfessor, CodSlot))
GO/* PROCEDURES	Modelo:		1- Procedure		2- Consulta a(as) tabelas pertencentes a procedure		3- Teste da procedure*/--Lembrete	CREATE PROCEDURE ArmazenaLembrete(
		@data		date,
		@conteudo	varchar(MAX)
	)AS
	BEGIN
		INSERT INTO tblLembrete VALUES (@data, @conteudo)
	END	EXEC ArmazenaLembrete '2016-08-04', 'conteudo teste'
	SELECT * FROM tblLembrete
--Pessoa
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
		@PessoaId int,
		@senhaNova varchar(25)
	)AS
	BEGIN
		UPDATE tblPessoa SET
			Senha = @senhaNova,
		WHERE PessoaId = @id	
	END


--Professor
	--Insert
	CREATE PROCEDURE ArmazenaProfessor(
		@nome varchar(50),		@email varchar(100),		@senha varchar(25),		@nomeGuerra	varchar(50),		--por padrão o professor começa com 0 horas aula atribuidas		@professorExiste bit		--por padrão todo professor já é ativo, inativaProfessor = 0	)AS	BEGIN		EXEC ArmazenaPessoa @nome, @email, @senha		INSERT INTO tblProfessor VALUES (@@IDENTITY, @nomeGuerra, 0, @professorExiste, 0, null)	END	EXEC ArmazenaProfessor 'Tobias', 'tobias@mail.com', '789756', 'TobiasGuerra', 1
	SELECT * FROM tblProfessor

	--Update
	CREATE PROCEDURE AlteraProfessor 
	(
		@id int,
		@nome varchar(50),		@email varchar(100),		@nomeGuerra varchar(50),
		@professorExiste bit,
		@professorAtiva bit
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
			ProfessorAtivo = @professorAtiva  
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


--Slots: Fixos e inseridos manualmente.
	CREATE PROCEDURE ArmazenaSlot(		@slotId		int,		@horaInicio time		--por padrão todo os slots começam inativos, Status_slot = 0	)AS	BEGIN		INSERT INTO tblSlot VALUES (@slotId, @horaInicio, 0)	END	CREATE PROCEDURE PopulaTblSlots	AS	BEGIN		EXEC ArmazenaSlot 1, '13:10:00'		EXEC ArmazenaSlot 2, '15:00:00'		EXEC ArmazenaSlot 3, '16:50:00'		EXEC ArmazenaSlot 4, '13:10:00'		EXEC ArmazenaSlot 5, '15:00:00'		EXEC ArmazenaSlot 6, '16:50:00'		EXEC ArmazenaSlot 7, '13:10:00'		EXEC ArmazenaSlot 8, '15:00:00'		EXEC ArmazenaSlot 9, '16:50:00'		EXEC ArmazenaSlot 10, '13:10:00'		EXEC ArmazenaSlot 11, '15:00:00'		EXEC ArmazenaSlot 12, '16:50:00'		EXEC ArmazenaSlot 13, '13:10:00'		EXEC ArmazenaSlot 14, '15:00:00'		EXEC ArmazenaSlot 15, '16:50:00'	END				EXEC PopulaTblSlots	SELECT * FROM tblSlot--Curso	CREATE PROCEDURE ArmazenaCurso(
		@titulo	varchar(100),
		@turno int
		-- 1: Diurno, 2: Vespertino, 3: Noturno
		--por definição para o inter, o turno será o da tarde
	)AS
	BEGIN
		INSERT INTO tblCurso VALUES (@titulo, @turno)
	END
	
	EXEC ArmazenaCurso 'Análise e desenvolvimento de sistemas', 1
	SELECT * FROM tblCurso
		

--Disciplina	CREATE PROCEDURE ArmazenaDisciplina(		@CodCurso		int,		@Nome			varchar(100),		@QtdAulas		int,		@Semestre		int,		@Sigla			varchar(3)	)AS	BEGIN		INSERT INTO tblDisciplina VALUES (@CodCurso, @Nome, @QtdAulas, @Semestre, @Sigla)	END	EXEC ArmazenaDisciplina 1, "Programação Orientada a Objetos", 4, 4, 'POO'	SELECT * FROM tblDisciplina/* VIEWS	Modelo:		1- Select para analisar os campos da tabela		2- View		3- Teste da View*/--Lembretes	SELECT * FROM tblLembrete	CREATE VIEW ViewLembretes	AS	SELECT LembreteId,		   Data,		   Conteudo				FROM tblLembrete	SELECT * FROM ViewLembretes--Professores	SELECT * FROM tblPessoa	SELECT * FROM tblProfessor	CREATE VIEW ViewProfessores	AS	SELECT p.PessoaId,		   p.Nome,		   p.Email,		   p.Senha,		   p.Permissao_admin     Permissão,		   prof.NomeGuerra       Nome_de_guerra,		   prof.HorasAula        Quantidade_horas_aula,		   prof.ProfessorExiste,		   prof.ProfessorAtivo,		   prof.Observacoes      Observações					FROM tblPessoa p, tblProfessor prof	Where p.PessoaId = prof.CodPessoa	SELECT * FROM ViewProfessores--Cursos	SELECT * FROM tblCurso	CREATE VIEW ViewCursos	AS	SELECT CursoId,		   Titulo,		   Turno				FROM tblCurso	SELECT * FROM ViewCursos--Disciplinas	SELECT * FROM tblCurso	SELECT * FROM tblDisciplina	CREATE VIEW ViewDisciplinas	AS	SELECT c.CursoId,		   c.Titulo,		   c.Turno,		   d.DisciplinaId,		   d.Nome,		   d.QtdAulas		Quantidade_aulas,		   d.Semestre,		   d.Sigla				FROM tblCurso c, tblDisciplina d	Where c.CursoId = d.CodCurso	SELECT * FROM ViewDisciplinas--Atribuicao	SELECT * FROM tblProfessor	SELECT * FROM tblDisciplina	SELECT * FROM tblAtribuicao	CREATE VIEW ViewAtribuicoes	AS	SELECT p.CodPessoa		PessoaId,		   p.NomeGuerra		Nome_Professor,		   d.DisciplinaId,		   d.Nome			Nome_Disciplina				FROM tblProfessor p, tblDisciplina d, tblAtribuicao a	Where a.CodProfessor = p.CodPessoa and a.CodDisciplina = d.DisciplinaId	INSERT INTO tblAtribuicao VALUES(2, 1)	SELECT * FROM ViewAtribuicoes--Disponibilidade--Resultado