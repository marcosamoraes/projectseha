using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Controllers.assignment
{
    public class dynamicTable
    {
        public curso getCurso()
        {
            curso objCurso = new curso();
            objCurso.cursoDescricao = "Análise e Desenvolvimento de Sistemas";
            objCurso.cursoId = 1;
            objCurso.semestres = getSemestres();

            return objCurso;
        }
        
        private List<semestre> getSemestres()
        {
            List <semestre> listaSemestres = new List<semestre>();
            int i = 1;

            while(i <= 6) { 
                semestre semestre = new semestre();
                semestre.semestreId = i;
                semestre.semestreNome = getNomeSemestre(i);
                semestre.disciplinas = getDisciplinas(i);
                listaSemestres.Add(semestre);
                i++;
            }
            return listaSemestres;
        }

        private String getNomeSemestre(int semestre)
        {
            String semestreNome = "";
            switch (semestre)
            {
                case 1: semestreNome = "First";
                    break;
                case 2: semestreNome = "Second";
                    break;
                case 3: semestreNome = "Thirth";
                    break;
                case 4: semestreNome = "Fourth";
                    break;
                case 5: semestreNome = "Fifth";
                    break;
                case 6: semestreNome = "Sixth"; 
                    break;
            }
            return semestreNome;
        }
        private List<disciplina> getDisciplinas(int semestre)
        {
            List<disciplina> listaDisciplinas = new List<disciplina>();

            switch (semestre)
            {
                case 1:
                    disciplina aoc = new disciplina();
                    aoc.disciplinaId = 1;
                    aoc.siglaDescricao = "AOC";
                    aoc.status = true;

                    disciplina lh = new disciplina();
                    lh.disciplinaId = 2;
                    lh.siglaDescricao = "LH";
                    lh.status = true;

                    disciplina alg = new disciplina();
                    alg.disciplinaId = 3;
                    alg.siglaDescricao = "ALG";
                    alg.status = true;

                    disciplina adm = new disciplina();
                    adm.disciplinaId = 4;
                    adm.siglaDescricao = "ADM";
                    adm.status = false;

                    disciplina ing1 = new disciplina();
                    ing1.disciplinaId = 5;
                    ing1.siglaDescricao = "ING1";
                    ing1.status = false;

                    disciplina mat = new disciplina();
                    mat.disciplinaId = 6;
                    mat.siglaDescricao = "MAT";
                    mat.status = true;

                    disciplina prog = new disciplina();
                    prog.disciplinaId = 7;
                    prog.siglaDescricao = "PROG";
                    prog.status = false;

                    listaDisciplinas.Add(aoc);
                    listaDisciplinas.Add(lh);
                    listaDisciplinas.Add(alg);
                    listaDisciplinas.Add(adm);
                    listaDisciplinas.Add(ing1);
                    listaDisciplinas.Add(mat);
                    listaDisciplinas.Add(prog);
                    break;

                case 2 :
                    disciplina cont = new disciplina();
                    cont.disciplinaId = 1;
                    cont.siglaDescricao = "CONT";
                    cont.status = false;

                    disciplina eng1 = new disciplina();
                    eng1.disciplinaId = 2;
                    eng1.siglaDescricao = "ENG1";
                    eng1.status = true;

                    disciplina ling = new disciplina();
                    ling.disciplinaId = 3;
                    ling.siglaDescricao = "LING";
                    ling.status = true;

                    disciplina si = new disciplina();
                    si.disciplinaId = 4;
                    si.siglaDescricao = "SI";
                    si.status = false;

                    disciplina ing2 = new disciplina();
                    ing2.disciplinaId = 5;
                    ing2.siglaDescricao = "ING2";
                    ing2.status = false;

                    disciplina com = new disciplina();
                    com.disciplinaId = 6;
                    com.siglaDescricao = "COM";
                    com.status = true;

                    disciplina calc = new disciplina();
                    calc.disciplinaId = 7;
                    calc.siglaDescricao = "CALC";
                    calc.status = false;

                    listaDisciplinas.Add(cont);
                    listaDisciplinas.Add(eng1);
                    listaDisciplinas.Add(ling);
                    listaDisciplinas.Add(si);
                    listaDisciplinas.Add(ing2);
                    listaDisciplinas.Add(com);
                    listaDisciplinas.Add(calc);
                    break;

                case 3:
                    disciplina econ = new disciplina();
                    econ.disciplinaId = 1;
                    econ.siglaDescricao = "ECON";
                    econ.status = false;

                    disciplina soc = new disciplina();
                    soc.disciplinaId = 2;
                    soc.siglaDescricao = "SOC";
                    soc.status = true;

                    disciplina ed = new disciplina();
                    ed.disciplinaId = 3;
                    ed.siglaDescricao = "ED";
                    ed.status = true;

                    disciplina eng2 = new disciplina();
                    eng2.disciplinaId = 4;
                    eng2.siglaDescricao = "ENG2";
                    eng2.status = false;

                    disciplina ing3 = new disciplina();
                    ing3.disciplinaId = 5;
                    ing3.siglaDescricao = "ING3";
                    ing3.status = false;

                    disciplina ihc = new disciplina();
                    ihc.disciplinaId = 6;
                    ihc.siglaDescricao = "IHC";
                    ihc.status = true;

                    disciplina so1 = new disciplina(); ;
                    so1.disciplinaId = 7;
                    so1.siglaDescricao = "SO1";
                    so1.status = false;

                    disciplina est = new disciplina();
                    est.disciplinaId = 8;
                    est.siglaDescricao = "EST";
                    est.status = false;

                    listaDisciplinas.Add(econ);
                    listaDisciplinas.Add(soc);
                    listaDisciplinas.Add(ed);
                    listaDisciplinas.Add(eng2);
                    listaDisciplinas.Add(ing3);
                    listaDisciplinas.Add(ihc);
                    listaDisciplinas.Add(so1);
                    listaDisciplinas.Add(est);
                    break;

                case 4:
                    disciplina bd = new disciplina();
                    bd.disciplinaId = 1;
                    bd.siglaDescricao = "BD";
                    bd.status = false;

                    disciplina eng3 = new disciplina();
                    eng3.disciplinaId = 2;
                    eng3.siglaDescricao = "ENG3";
                    eng3.status = true;

                    disciplina poo = new disciplina();
                    poo.disciplinaId = 3;
                    poo.siglaDescricao = "POO";
                    poo.status = true;

                    disciplina web = new disciplina();
                    web.disciplinaId = 4;
                    web.siglaDescricao = "WEB";
                    web.status = false;

                    disciplina so2 = new disciplina();
                    so2.disciplinaId = 5;
                    so2.siglaDescricao = "SO2";
                    so2.status = false;

                    disciplina ing4 = new disciplina();
                    ing4.disciplinaId = 6;
                    ing4.siglaDescricao = "ING4";
                    ing4.status = true;

                    disciplina mpct = new disciplina();
                    mpct.disciplinaId = 7;
                    mpct.siglaDescricao = "MPCT";
                    mpct.status = false;

                    listaDisciplinas.Add(bd);
                    listaDisciplinas.Add(eng3);
                    listaDisciplinas.Add(poo);
                    listaDisciplinas.Add(web);
                    listaDisciplinas.Add(so2);
                    listaDisciplinas.Add(ing4);
                    listaDisciplinas.Add(mpct);
                    break;

                case 5:
                    disciplina lbd = new disciplina();
                    lbd.disciplinaId = 1;
                    lbd.siglaDescricao = "LBD";
                    lbd.status = false;

                    disciplina pla = new disciplina();
                    pla.disciplinaId = 2;
                    pla.siglaDescricao = "PLA";
                    pla.status = true;

                    disciplina ing5 = new disciplina();
                    ing5.disciplinaId = 3;
                    ing5.siglaDescricao = "ING5";
                    ing5.status = true;

                    disciplina red = new disciplina();
                    red.disciplinaId = 4;
                    red.siglaDescricao = "RED";
                    red.status = false;

                    disciplina mov = new disciplina();
                    mov.disciplinaId = 5;
                    mov.siglaDescricao = "MOV";
                    mov.status = false;

                    disciplina sei = new disciplina();
                    sei.disciplinaId = 6;
                    sei.siglaDescricao = "SEI";
                    sei.status = true;

                    disciplina leng = new disciplina();
                    leng.disciplinaId = 7;
                    leng.siglaDescricao = "LENG";
                    leng.status = false;

                    listaDisciplinas.Add(lbd);
                    listaDisciplinas.Add(pla);
                    listaDisciplinas.Add(ing5);
                    listaDisciplinas.Add(red);
                    listaDisciplinas.Add(mov);
                    listaDisciplinas.Add(sei);
                    listaDisciplinas.Add(leng);
                    break;

                case 6:
                    disciplina ia = new disciplina();
                    ia.disciplinaId = 1;
                    ia.siglaDescricao = "IA";
                    ia.status = false;

                    disciplina eti = new disciplina();
                    eti.disciplinaId = 2;
                    eti.siglaDescricao = "ETI";
                    eti.status = true;

                    disciplina gese = new disciplina();
                    gese.disciplinaId = 3;
                    gese.siglaDescricao = "GESE";
                    gese.status = true;

                    disciplina gesp = new disciplina();
                    gesp.disciplinaId = 4;
                    gesp.siglaDescricao = "GESP";
                    gesp.status = false;

                    disciplina lred = new disciplina();
                    lred.disciplinaId = 5;
                    lred.siglaDescricao = "LRED";
                    lred.status = false;

                    disciplina emp = new disciplina();
                    emp.disciplinaId = 6;
                    emp.siglaDescricao = "EMP";
                    emp.status = true;

                    disciplina ggov = new disciplina();
                    ggov.disciplinaId = 7;
                    ggov.siglaDescricao = "GGOV";
                    ggov.status = false;

                    disciplina ing6 = new disciplina();
                    ing6.disciplinaId = 8;
                    ing6.siglaDescricao = "ING6";
                    ing6.status = false;

                    listaDisciplinas.Add(ia);
                    listaDisciplinas.Add(gese);
                    listaDisciplinas.Add(gesp);
                    listaDisciplinas.Add(lred);
                    listaDisciplinas.Add(emp);
                    listaDisciplinas.Add(ggov);
                    listaDisciplinas.Add(ing6);
                    break;
            }
            return listaDisciplinas;
        }

    }

    sealed class curso
    {
        public int cursoId;
        public String cursoDescricao;
        public List<semestre> semestres;

    }

    sealed class semestre
    {
        public int semestreId;
        public String semestreNome;
        public List<disciplina> disciplinas;
    }
    sealed class disciplina
    {
        public int disciplinaId;
        public String siglaDescricao;
        public bool status;
    }
}