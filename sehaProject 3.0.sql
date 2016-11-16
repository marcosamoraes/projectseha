CREATE DATABASE BDSeha

USE BDSEHA

/* CREATE DE TABELAS */
CREATE TABLE tblLembrete (
	LembreteId	int				NOT NULL primary key identity,
	Data		date			NOT NULL,
	Conteudo	varchar(MAX)	NOT NULL,
)

CREATE TABLE tblPessoa (
	PessoaId			int				NOT NULL primary key identity, 
	Nome				varchar(50)		NOT NULL,
	Email				varchar(100)	NOT NULL unique,
	Senha				varchar(25)		NOT NULL,
	Permissao_admin		bit				NOT NULL
)


CREATE TABLE tblProfessor (
	CodPessoa			int				NOT NULL primary key references tblPessoa,
	NomeGuerra			varchar(50)		NOT NULL,
	HorasAula			int				NULL,
	ProfessorExiste		bit				NOT NULL,
	ProfessorAtivo		bit				NOT NULL,
	Observacoes			varchar(MAX)	NULL
)

CREATE TABLE tblSlot (
	SlotId		int		NOT NULL primary key,
	HoraInicio	time	NOT NULL
)

CREATE TABLE tblCurso (
	CursoId		int				NOT NULL primary key identity,
	Titulo		varchar(100)	NOT NULL,
	Turno		varchar(15)		NOT NULL
)


CREATE TABLE tblDisciplina (
	DisciplinaId	int				NOT NULL primary key identity,
	CodCurso		int				NOT NULL references tblCurso,
	Nome			varchar(100)	NOT NULL,
	QtdAulas		int				NOT NULL,
	Semestre		int				NOT NULL,
	Sigla			varchar(3)		NOT NULL
)

CREATE TABLE tblDisponibilidade (
	CodProfessor	int NOT NULL references tblProfessor,
	CodSlot			int	NOT NULL references tblSlot,
	status_slot		bit NULL,

	PRIMARY KEY (CodProfessor, CodSlot) 
)

CREATE TABLE tblAtribuicao (
	CodProfessor	int NOT NULL references tblProfessor,
	CodDisciplina	int	NOT NULL unique references tblDisciplina,
	CodCurso		int NOT NULL references tblCurso,

	PRIMARY KEY (CodProfessor, CodDisciplina) 
)

CREATE TABLE tblResultado (
	CodProfessor	int			NOT NULL,  
	CodDisciplina	int			NOT NULL,
	CodSlot			int			NOT NULL,

	PRIMARY KEY (CodProfessor, CodDisciplina, CodSlot),

	CONSTRAINT fkCodAtribuicaoResultado 
		FOREIGN KEY					(CodProfessor, CodDisciplina) 
		REFERENCES tblAtribuicao	(CodProfessor, CodDisciplina),

	CONSTRAINT fkDisponibilidadeResultado 
		FOREIGN KEY						(CodProfessor, CodSlot) 
		REFERENCES tblDisponibilidade	(CodProfessor, CodSlot)
)




/*INSERTS*/

--tblPessoa
INSERT INTO tblPessoa VALUES ('Carlos Magnus', 'carlos@fatecriopreto.edu.br', 'fatecrp', 1);
INSERT INTO tblPessoa VALUES ('Lucimar Sasso', 'lucimar@fatecriopreto.edu.br', 'fatecrp', 0);
INSERT INTO tblPessoa VALUES ('Edes Costa', 'edes@fatecriopreto.edu.br', 'fatecrp', 0);
INSERT INTO tblPessoa VALUES ('Henrique Dezani', 'dezani@fatecriopreto.edu.br', 'fatecrp', 0);

--tblProfessor
INSERT INTO tblProfessor VALUES (2, 'Lucimar', 0, 1, 1, '')
INSERT INTO tblProfessor VALUES (3, 'Edes', 0, 1, 1, '')
INSERT INTO tblProfessor VALUES (4, 'Henrique', 0, 1, 1, '')

--tblCurso
INSERT INTO tblCurso VALUES ('Análise e Desenvolvimento de Sistemas', 'Tarde');
INSERT INTO tblCurso VALUES ('Informática para Negócios', 'Noite');
INSERT INTO tblCurso VALUES ('Agronegócio', 'Manhã');

--tblDisciplina

--Curso ADS

	--1ºSemestre
	INSERT INTO tblDisciplina VALUES (1, 'Prog. M.I', 4, 1, 'PMI');
	INSERT INTO tblDisciplina VALUES (1, 'Mat. Discreta', 4, 1, 'MAT');
	INSERT INTO tblDisciplina VALUES (1, 'Algorit. e Lóg Prog.', 4, 1, 'ALG');
	INSERT INTO tblDisciplina VALUES (1, 'Arq. Org. Comput.', 4, 1, 'AOC');
	INSERT INTO tblDisciplina VALUES (1, 'Inglês', 2, 1, 'ING');
	INSERT INTO tblDisciplina VALUES (1, 'Lab. Hardware', 2, 1, 'LH');
	
	--2ºSemestre
	INSERT INTO tblDisciplina VALUES (1, 'Ling. Prog.', 4, 2, 'LP');
	INSERT INTO tblDisciplina VALUES (1, 'Cálculo', 4, 2, 'CAL');
	INSERT INTO tblDisciplina VALUES (1, 'Sist. Inform.', 4, 2, 'SI');
	INSERT INTO tblDisciplina VALUES (1, 'Comunic. Express.', 4, 2, 'CEE');
	INSERT INTO tblDisciplina VALUES (1, 'Eng. Soft. I', 4, 2, 'ES1');
	INSERT INTO tblDisciplina VALUES (1, 'Inglês II', 2, 2, 'ING');
	INSERT INTO tblDisciplina VALUES (1, 'Contabilidade', 2, 2, 'CON');

	--3ºSemestre
	INSERT INTO tblDisciplina VALUES (1, 'Estatística Aplic. ', 4, 3, 'EST');
	INSERT INTO tblDisciplina VALUES (1, 'Estr. Dados', 4, 3, 'ED');
	INSERT INTO tblDisciplina VALUES (1, 'Sists. Operacs. I', 4, 3, 'SO1');
	INSERT INTO tblDisciplina VALUES (1, 'Interação Humano Computador', 2, 3, 'IHC');
	INSERT INTO tblDisciplina VALUES (1, 'Eng. Soft. II', 4, 3, 'ES2');
	INSERT INTO tblDisciplina VALUES (1, 'Inglês III', 2, 3, 'ING');
	INSERT INTO tblDisciplina VALUES (1, 'Econ. e Finanças', 2, 3, 'ECO');
	INSERT INTO tblDisciplina VALUES (1, 'Soc. e Tecnol', 2, 3, 'SOC');

	--4ºSemestre
	INSERT INTO tblDisciplina VALUES (1, 'M.P.C.T.', 2, 4, 'MPC');
	INSERT INTO tblDisciplina VALUES (1, 'Banco de Dados', 4, 4, 'BD');
	INSERT INTO tblDisciplina VALUES (1, 'Sists. Operacs. II', 4, 4, 'SO1');
	INSERT INTO tblDisciplina VALUES (1, 'Prog Orientada Obj.', 4, 4, 'POO');
	INSERT INTO tblDisciplina VALUES (1, 'Eng. Soft. III', 4, 4, 'ES3');
	INSERT INTO tblDisciplina VALUES (1, 'Inglês IV', 2, 4, 'ING');
	INSERT INTO tblDisciplina VALUES (1, 'Prog Web', 2, 4, 'PW');

	--5ºSemestre
	INSERT INTO tblDisciplina VALUES (1, 'Lab BD', 4, 5, 'LBD');
	INSERT INTO tblDisciplina VALUES (1, 'Segur.Inform.', 2, 5, 'SEG');
	INSERT INTO tblDisciplina VALUES (1, 'PL Aplic', 4, 5, 'PLA');
	INSERT INTO tblDisciplina VALUES (1, 'Progr.Disps.Móveis', 4, 5, 'PDM');
	INSERT INTO tblDisciplina VALUES (1, 'Lab.Eng.Soft', 4, 5, 'LES');
	INSERT INTO tblDisciplina VALUES (1, 'Inglês V', 2, 5, 'ING');
	INSERT INTO tblDisciplina VALUES (1, 'Redes Comp.', 4, 5, 'PW');

	--6ºSemestre
	INSERT INTO tblDisciplina VALUES (1, 'Ética Resp. Prof.', 2, 6, 'ERP');
	INSERT INTO tblDisciplina VALUES (1, 'G. Projetos', 4, 6, 'GP');
	INSERT INTO tblDisciplina VALUES (1, 'G. Gov. TI', 4, 6, 'GGT');
	INSERT INTO tblDisciplina VALUES (1, 'Lab. Redes', 4, 6, 'LRD');
	INSERT INTO tblDisciplina VALUES (1, 'Empreendedorismo', 2, 6, 'EMP');
	INSERT INTO tblDisciplina VALUES (1, 'Inglês VI', 2, 6, 'ING');
	INSERT INTO tblDisciplina VALUES (1, 'Gestão de Equipes', 2, 6, 'GEQ');
	INSERT INTO tblDisciplina VALUES (1, 'Int. Artificial.', 4, 6, 'IA');


--tblLembrete
INSERT INTO tblLembrete VALUES ('11/07/2016', 'Preencher todos os horários até o dia 15/07');
INSERT INTO tblLembrete VALUES ('15/07/2016', 'Verificar disponibilidade de aulas');
INSERT INTO tblLembrete VALUES ('02/12/2016', 'Caros Professores, suas atribuições foram salvas! Verifiquem se a disponoibilidade está habilitada.');


/*VIEWS*/

--tblLembrete
	CREATE VIEW ViewLembretes
	AS
	SELECT LembreteId,
		   Data,
		   Conteudo			
	FROM tblLembrete


--tblSlot
	CREATE VIEW ViewSlots
	AS
	SELECT SlotId,
		   HoraInicio		
	FROM tblSlot


--tblPessoa
	CREATE VIEW ViewPessoas
	AS
	SELECT PessoaId,
		   Nome,
		   Email,
		   Senha,
		   Permissao_admin			
	FROM tblPessoa


--tblProfessor (JOIN)
	CREATE VIEW ViewProfessores
	AS
	SELECT p.PessoaId,
		   p.Nome,
		   p.Email,
		   p.Senha,
		   p.Permissao_admin,
		   prof.NomeGuerra,
		   prof.HorasAula,
		   prof.ProfessorExiste,
		   prof.ProfessorAtivo,
		   prof.Observacoes				
	FROM tblPessoa p, tblProfessor prof
	Where p.PessoaId = prof.CodPessoa


--tblCurso
	CREATE VIEW ViewCursos
	AS
	SELECT CursoId,
		   Titulo,
		   Turno			
	FROM tblCurso


--tblDisciplina
	CREATE VIEW ViewDisciplinas
	AS
	SELECT d.CodCurso,
		   d.DisciplinaId,
		   d.Nome,
		   d.QtdAulas,
		   d.Semestre,
		   d.Sigla			
	FROM tblDisciplina d


--tblAtribuicao
	CREATE VIEW ViewAtribuicoes
	AS
	SELECT a.CodProfessor,
		   a.CodDisciplina,
		   a.CodCurso
	FROM tblAtribuicao a


--tblDisponibilidade
	CREATE VIEW ViewDisponibilidades
	AS
	SELECT d.CodProfessor,
		   d.CodSlot,
		   d.status_slot
	FROM tblDisponibilidade d
	

--tblResultado
	CREATE VIEW ViewResultados
	AS
	SELECT r.CodProfessor,
		   r.CodSlot
	FROM tblDisponibilidade r

--(JOINS)

--Professor - Disciplina - Curso
	CREATE VIEW ViewProf_Disciplina_Curso
	AS
	SELECT p.CodPessoa,
		   p.NomeGuerra,
		   d.DisciplinaId,
		   d.Nome,
		   d.Semestre,
		   d.QtdAulas,
		   c.CursoId,	
		   c.Titulo	
	FROM tblProfessor p, tblDisciplina d, tblCurso c, tblAtribuicao a
	Where p.CodPessoa = a.CodProfessor and d.DisciplinaId = a.CodDisciplina and c.CursoId = a.CodCurso



--Professor - Disciplina
	CREATE VIEW ViewProf_Disciplina
	AS
	SELECT p.CodPessoa,
		   p.NomeGuerra,
		   d.DisciplinaId,
		   d.Nome,
		   d.Sigla,
		   d.QtdAulas
	FROM tblProfessor p, tblDisciplina d, tblAtribuicao a
	Where p.CodPessoa = a.CodProfessor and d.DisciplinaId = a.CodDisciplina



--Professor - Curso
	CREATE VIEW ViewProf_Curso
	AS
	SELECT p.CodPessoa,
		   p.NomeGuerra,
		   c.CursoId,	
		   c.Titulo,
		   c.Turno
	FROM tblProfessor p, tblCurso c, tblAtribuicao a
	Where p.CodPessoa = a.CodProfessor and c.CursoId = a.CodCurso


--Professor - Disponibilidade - Slot
	CREATE VIEW ViewProf_Disponibilidade_Slot
	AS
	SELECT p.CodPessoa,
		   p.NomeGuerra,
		   s.SlotId,
		   s.HoraInicio		
	FROM tblProfessor p, tblSlot s, tblDisponibilidade d
	Where p.CodPessoa = d.CodProfessor and  s.SlotId = d.CodSlot



--Disciplina - Curso
	CREATE VIEW ViewDisciplina_Curso
	AS
	SELECT d.DisciplinaId,
		   d.Nome,
		   d.Semestre,
		   c.CursoId,
		   c.Titulo,
		   c.Turno
	FROM tblDisciplina d, tblCurso c
	Where d.CodCurso = c.CursoId



--Disciplina - Prof - Slot
	CREATE VIEW ViewDisciplina_Prof_Slot
	AS
	SELECT d.DisciplinaId,
		   d.Nome,
		   p.CodPessoa,
		   p.NomeGuerra,
		   s.SlotId,
		   s.HoraInicio
	FROM tblDisciplina d, tblProfessor p, tblSlot s, tblResultado r
	Where d.DisciplinaId = r.CodDisciplina and p.CodPessoa = r.CodProfessor and s.SlotId = r.CodSlot




/*PROCEDURES*/
	
	
--Lembrete
	--Insert
	CREATE PROCEDURE ArmazenaLembrete(
		@data		date,
		@conteudo	varchar(MAX)
	)AS
	BEGIN
		INSERT INTO tblLembrete VALUES (@data, @conteudo)
	END
	
	--Update
	CREATE PROCEDURE AlteraLembrete(
		@id int,
		@conteudo varchar(MAX)
	)AS
	BEGIN 
		UPDATE tblLembrete SET
			Conteudo = @conteudo
		WHERE LembreteId = @id
	END
	
	--Delete
	CREATE PROCEDURE ApagaLembrete(
		@id int
	)AS
	BEGIN
		DELETE FROM tblLembrete 
		WHERE LembreteId = @id
	END


--Pessoa
	--Insert
	CREATE PROCEDURE ArmazenaPessoa(
		@nome				varchar(50),
		@email				varchar(100)
	)AS
	BEGIN
		INSERT INTO tblPessoa VALUES (@nome, @email, 'fatecrp', 0)
	END
	
	--Update
	CREATE PROCEDURE AlteraPessoa 
	(
		@id int,
		@nome varchar(50),
		@email varchar(100)
	)
	AS
	BEGIN
		UPDATE tblPessoa SET
			Nome = @nome,
			Email = @email
		WHERE PessoaId = @id
	END
	

	--ValidaLogin
	CREATE PROCEDURE ValidaLogin(
		@email varchar(100),
		@senha varchar(25)
	)AS
	BEGIN
		SELECT * FROM tblPessoa WHERE Email = @email and Senha = @senha
	END
	

	--UpdateSenha
	CREATE PROCEDURE AlteraSenha(
		@id int,
		@senhaNova varchar(25)
	)AS
	BEGIN
		UPDATE tblPessoa SET
			Senha = @senhaNova
		WHERE PessoaId = @id	
	END
	


--Professor
	--Insert
	CREATE PROCEDURE ArmazenaProfessor(
		@nome varchar(50),
		@email varchar(100),

		@nomeGuerra	varchar(50),
		@professorExiste bit
	)AS
	BEGIN
		EXEC ArmazenaPessoa @nome, @email
		INSERT INTO tblProfessor VALUES (@@IDENTITY, @nomeGuerra, 0, @professorExiste, 1, '')
	END
	
	
	--Update
	CREATE PROCEDURE AlteraProfessor 
	(
		@id int,
		@nome varchar(50),
		@email varchar(100),
		@nomeGuerra varchar(50),
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

	
--Slots
	--Insert
	CREATE PROCEDURE ArmazenaSlot(
		@slotId		int,
		@horaInicio time
	
	)AS
	BEGIN
		INSERT INTO tblSlot VALUES (@slotId, @horaInicio)
	END
	CREATE PROCEDURE PopulaTblSlots
	AS
	BEGIN
		EXEC ArmazenaSlot 1, '07:40:00'
		EXEC ArmazenaSlot 2, '09:30:00'
		EXEC ArmazenaSlot 3, '11:20:00'
		EXEC ArmazenaSlot 4, '13:10:00'
		EXEC ArmazenaSlot 5, '15:00:00'
		EXEC ArmazenaSlot 6, '16:50:00'
		EXEC ArmazenaSlot 7, '19:00:00'
		EXEC ArmazenaSlot 8, '20:50:00'

		EXEC ArmazenaSlot 9, '07:40:00'
		EXEC ArmazenaSlot 10, '09:30:00'
		EXEC ArmazenaSlot 11, '11:20:00'
		EXEC ArmazenaSlot 12, '13:10:00'
		EXEC ArmazenaSlot 13, '15:00:00'
		EXEC ArmazenaSlot 14, '16:50:00'
		EXEC ArmazenaSlot 15, '19:00:00'
		EXEC ArmazenaSlot 16, '20:50:00'

		EXEC ArmazenaSlot 17, '07:40:00'
		EXEC ArmazenaSlot 18, '09:30:00'
		EXEC ArmazenaSlot 19, '11:20:00'
		EXEC ArmazenaSlot 20, '13:10:00'
		EXEC ArmazenaSlot 21, '15:00:00'
		EXEC ArmazenaSlot 22, '16:50:00'
		EXEC ArmazenaSlot 23, '19:00:00'
		EXEC ArmazenaSlot 24, '20:50:00'

		EXEC ArmazenaSlot 25, '07:40:00'
		EXEC ArmazenaSlot 26, '09:30:00'
		EXEC ArmazenaSlot 27, '11:20:00'
		EXEC ArmazenaSlot 28, '13:10:00'
		EXEC ArmazenaSlot 29, '15:00:00'
		EXEC ArmazenaSlot 30, '16:50:00'
		EXEC ArmazenaSlot 31, '19:00:00'
		EXEC ArmazenaSlot 32, '20:50:00'

		EXEC ArmazenaSlot 33, '07:40:00'
		EXEC ArmazenaSlot 34, '09:30:00'
		EXEC ArmazenaSlot 35, '11:20:00'
		EXEC ArmazenaSlot 36, '13:10:00'
		EXEC ArmazenaSlot 37, '15:00:00'
		EXEC ArmazenaSlot 38, '16:50:00'
		EXEC ArmazenaSlot 39, '19:00:00'
		EXEC ArmazenaSlot 40, '20:50:00'

		EXEC ArmazenaSlot 41, '07:40:00'
		EXEC ArmazenaSlot 42, '09:30:00'
		EXEC ArmazenaSlot 43, '11:20:00'
		EXEC ArmazenaSlot 44, '13:10:00'
		EXEC ArmazenaSlot 45, '15:00:00'
		EXEC ArmazenaSlot 46, '16:50:00'
		EXEC ArmazenaSlot 47, '19:00:00'
		EXEC ArmazenaSlot 48, '20:50:00'
	END
	select*from tblSlot
	PopulaTblSlots
		
			
--Curso
	--Insert
	CREATE PROCEDURE ArmazenaCurso(
		@titulo	varchar(100),
		@turno varchar(15)
	)AS
	BEGIN
		INSERT INTO tblCurso VALUES (@titulo, @turno)
	END
	
	
	--Update
	CREATE PROCEDURE AlteraCurso
	(
		@id int,
		@titulo varchar(100),
		@turno varchar(15)
	)
	AS
	BEGIN
		UPDATE tblCurso SET
			Titulo = @titulo,
			Turno = @turno
		WHERE CursoId = @id
	END


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


--Disciplina 
	--Insert
	CREATE PROCEDURE ArmazenaDisciplina(
		@codCurso		int,
		@nome			varchar(100),
		@qtdAulas		int,
		@semestre		int,
		@sigla			varchar(3)
	)AS
	BEGIN
		INSERT INTO tblDisciplina VALUES (@CodCurso, @Nome, @QtdAulas, @Semestre, @Sigla)
	END
	

	--Update
	CREATE PROCEDURE AlteraDisciplina(
		@id				int,
		@codCurso		int,
		@nome			varchar(100),
		@qtdAulas		int,
		@semestre		int,
		@sigla			varchar(3)
	)AS
	BEGIN
		UPDATE tblDisciplina SET
		CodCurso = @codCurso,
		Nome = @nome,
		QtdAulas = @qtdAulas,
		Semestre = @semestre,
		Sigla = @sigla
	WHERE DisciplinaId = @id
	END
	

	--Delete
	CREATE PROCEDURE ApagaDisciplina 
	(
		@id int
	)
	AS
	BEGIN
		DELETE FROM tblDisciplina 
		WHERE DisciplinaId = @id
	END


--Disponibilidade
	--Insert
	CREATE PROCEDURE ArmazenaDisponibilidade(
		@codProfessor		int,
		@codSlot			int,
		@status_slot		bit
	)AS
	BEGIN
		INSERT INTO tblDisponibilidade VALUES (@codProfessor, @codSlot, @status_slot)
	END
	

	--Delete
	CREATE PROCEDURE ApagaDisponibilidade
	(
		@ProfessorId int
	)
	AS
	BEGIN
		DELETE FROM tblDisponibilidade
		WHERE CodProfessor = @ProfessorId
	END

--Atribuicao
	--Insert
	CREATE PROCEDURE ArmazenaAtribuicao(
		@codProfessor		int,
		@codDisciplina		int,
		@codCurso			int
	)AS
	BEGIN
		INSERT INTO tblAtribuicao VALUES (@codProfessor, @codDisciplina, @codCurso)
	END
	

	--Delete
	CREATE PROCEDURE ApagaAtribuicao
	(
		@ProfessorId int,
		@CursoId int
	)
	AS
	BEGIN
		DELETE FROM tblAtribuicao
		WHERE CodProfessor = @ProfessorId AND CodCurso = @CursoId
	END


--Resultado
	--Insert
	CREATE PROCEDURE ArmazenaResultado(
		@codProfessor		int,
		@codDisciplina		int,
		@codSlot			int
	)AS
	BEGIN
		INSERT INTO tblResultado VALUES (@codProfessor, @codDisciplina, @codSlot)
	END
	

	--Delete
	CREATE PROCEDURE ApagaResultado
	(
		@codProfessor		int,
		@codDisciplina		int,
		@codSlot			int
	)
	AS
	BEGIN
		DELETE FROM tblResultado
		WHERE CodProfessor = @codProfessor AND CodDisciplina = @codDisciplina and CodSlot= @codSlot
	END
