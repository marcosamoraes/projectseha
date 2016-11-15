﻿$(document).ready(function () {
    var ctrl = 0;
    var ctrl2 = 0;
    var ctrl_f4b = 0;
    var modal_lemb = 0;

    /*View Availability*/

    //var selecao = 1; //define cor verde(1) ou laranja(2)

    /*Carregamento inicial ----------------------------------------------------------------------------
    var horas = parseInt($('#horas').html());
    console.log(horas);
    var totalBarra = horas + (horas / 2);
    var maxDisp = horas / 2; //max quadros disponiveis
    var maxTalvez = parseInt(maxDisp / 2); //max quadros talvez
    if (totalBarra % 2 != 0) { totalBarra++; maxTalvez++ } //caso seja impar, aumenta 1
  
    if ($("#existeDisponibilidade").length>0) { //verifica se elemento existe e inicia valores ja preenchidos
        contTalvez = maxTalvez;
        atualBarra = totalBarra;
        contDisp = maxDisp;
        console.log(contTalvez);
        console.log(contDisp);
        console.log(atualBarra);
    }
    else {
        var contTalvez = 0;//contTalvez = maxTalvez;
        var atualBarra = 0;//atualBarra = totalBarra;
        var contDisp = 0; //contDisp = maxDisp;
        console.log(contTalvez);
        console.log(contDisp);
        console.log(atualBarra);
    }

    var widthBarra = 0;
    var color = '#4DB6AC';
    $(".progress-bar").attr('aria-valuemax', totalBarra); //Inicializador do total barra
    $(".slots-save").attr('disabled', 'disabled');
    console.log("total barra é:", totalBarra);

    //checa disabled do bt save
    if (atualBarra == totalBarra) {
        $(".slots-save").removeAttr('disabled', 'disabled');
    }
    
    //prache!
    $(".progress-bar").attr('aria-valuenow', atualBarra); //atualiza o valuenow da progress-bar
    widthBarra = (atualBarra / totalBarra) * 100; //atualiza o width da progress-bar
    $(".progress-bar").css('width', widthBarra + '%'); //preenche bar com o width
    $(".progress-bar label").html(parseInt(widthBarra) + '%'); //atualiza a label

    /*-----------------------------------------------------------------------------------------------*/

    var atualBarra;
    var totalBarra;
    var contDisp;
    var contTalvez;
    var talvez;
    var disp;
    var selecao;
    var horas;
    var maxDisp;
    var maxTalvez;
    var widhtBarra;
    var color;


    /*Botões de controle (cores)*/
    $(document).on('click', '#available', function () {
        color = '#4DB6AC';
        selecao = 1;
        $(this).css('opacity', '1');
        $('#maybe').css('opacity', '0.5');
    });

    $(document).on('click', '#maybe', function () {
        color = '#FFCC80';
        selecao = 2;
        $(this).css('opacity', '1');
        $('#available').css('opacity', '0.5');
    });

    $(document).on('click', '.slots-content', function () {

        if ($(this).hasClass('disponivel')) { //se já existir preenchimento
            $(this).removeClass('disponivel');
            atualBarra -= 2;
            contDisp--;

            console.log("id: ", $("input", this).val());
            console.log("talvez: ", contTalvez);
            console.log("disp: ", contDisp);
            console.log("atualBarra ", atualBarra);
            console.log("total barra eh ", totalBarra);
            console.log($("#existeDisponibilidade").length > 0);
        }
        else {
            if ($(this).hasClass('talvez')) {
                $(this).removeClass('talvez');
                atualBarra -= 2;
                contTalvez--;

                console.log("id: ", $("input", this).val());
                console.log("talvez: ", contTalvez);
                console.log("disp: ", contDisp);
                console.log("atualBarra ", atualBarra);
                console.log($("#existeDisponibilidade").length > 0);
            }
            else {
                if (selecao == 1 && contDisp < maxDisp) { //se for available
                    $(this).addClass('disponivel');
                    atualBarra += 2;
                    contDisp++;
                }
                else if (selecao == 2 && contTalvez < maxTalvez) {
                    $(this).addClass('talvez');
                    atualBarra += 2;
                    contTalvez++;
                }
            }
        }

        //função para desabilitar save
        if (atualBarra == totalBarra) {
            $(".slots-save").removeAttr('disabled');
        }
        else {
            $(".slots-save").attr('disabled', 'disabled');
        }


        //prache!
        $(".progress-bar").attr('aria-valuenow', atualBarra); //atualiza o valuenow da progress-bar
        widthBarra = (atualBarra / totalBarra) * 100; //atualiza o width da progress-bar
        $(".progress-bar").css('width', widthBarra + '%'); //preenche bar com o width
        $(".progress-bar label").html(parseInt(widthBarra) + '%')//atualiza a label
    });

    $(document).on('click', '.slots-clear', function () {
        $('.slots-content').removeClass('disponivel');
        $('.slots-content').removeClass('talvez');
        contTalvez = 0;
        contDisp = 0;
        atualBarra = 0;

        $(".slots-save").attr('disabled', 'disabled');

        //prache!
        $(".progress-bar").attr('aria-valuenow', atualBarra); //atualiza o valuenow da progress-bar
        widthBarra = (atualBarra / totalBarra) * 100; //atualiza o width da progress-bar
        $(".progress-bar").css('width', widthBarra + '%'); //preenche bar com o width
        $(".progress-bar label").html(parseInt(widthBarra) + '%')//atualiza a label

        slotTalvez = [];
        slotDisponivel = [];
    });

    var slotDisponivel = [];
    var slotTalvez = [];

    $(document).on('click', '.slots-save', function () {
        slotDisponivel = []; //limpo
        slotTalvez = []; //limpo

        var disponivel = $('.slots-content.disponivel input');
        var talvez = $('.slots-content.talvez input');

        if (disponivel.length > 0) {
            disponivel.each(function () {
                slotDisponivel.push($(this).val());
            });
        }
        if (talvez.length > 0) {
            talvez.each(function () {
                slotTalvez.push($(this).val());
            });
        }

        var ProfessorId = $("#PessoaId").val();

        $.ajax({ //talvez n precisamos usar o ajax aqui
            url: '/default/Create/?ProfessorId=' + ProfessorId + '&slotDisponivel=' + slotDisponivel + '&slotTalvez=' + slotTalvez,
            method: 'get',
            success: function (data) {

            },
            error: function () {

            }
        });

        var obs = $('#txt-obs').val();
        if (obs != null) {
            $.ajax({
                url: '/user/UpdateObservation/?ProfessorId=' + ProfessorId + '&observacoes=' + obs,
                success: function () {

                }
            });
        }

        if ($('#msg-sucesso').val() == null) {
            $(".slots-tools").before(
              '<div id="msg-sucesso" style="margin-top: 20px; float:left; width:100%; text-align:center" class="alert alert-success alert-dismissable">' +
                  '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">' +
                       '&times;' +
                  '</button>' +
                  'Availability saved successfully' +
              '</div>');
        }
    });


    //Carregamento da view Avaiability
    if ($("#_Availability".length > 0)) {

        var ProfessorId = $("#ProfessorId").val();

        $.ajax({
            url: '/user/_Availability/?ProfessorId=' + ProfessorId,
            method: 'get',
            dataType: 'html',
            success: function (data) {
                $('#_Availability').html(data);

                selecao = 1;
                horas = parseInt($('#horas').html());
                console.log(horas);
                totalBarra = horas + (horas / 2);
                maxDisp = horas / 2; //max quadros disponiveis
                maxTalvez = parseInt(maxDisp / 2); //max quadros talvez
                if (totalBarra % 2 != 0) { totalBarra++; maxTalvez++ } //caso seja impar, aumenta 1

                if ($("#existeDisponibilidade").length > 0) { //verifica se elemento existe e inicia valores ja preenchidos
                    contTalvez = maxTalvez;
                    atualBarra = totalBarra;
                    contDisp = maxDisp;
                    console.log(contTalvez);
                    console.log(contDisp);
                    console.log(atualBarra);
                }
                else {
                    contTalvez = 0;//contTalvez = maxTalvez;
                    atualBarra = 0;//atualBarra = totalBarra;
                    contDisp = 0; //contDisp = maxDisp;
                    console.log(contTalvez);
                    console.log(contDisp);
                    console.log(atualBarra);
                }

                widthBarra = 0;
                color = '#4DB6AC';
                $(".progress-bar").attr('aria-valuemax', totalBarra); //Inicializador do total barra
                $(".slots-save").attr('disabled', 'disabled');
                console.log("total barra é:", totalBarra);

                //checa disabled do bt save
                if (atualBarra == totalBarra) {
                    $(".slots-save").removeAttr('disabled', 'disabled');
                }

                //prache!
                $(".progress-bar").attr('aria-valuenow', atualBarra); //atualiza o valuenow da progress-bar
                widthBarra = (atualBarra / totalBarra) * 100; //atualiza o width da progress-bar
                $(".progress-bar").css('width', widthBarra + '%'); //preenche bar com o width
                $(".progress-bar label").html(parseInt(widthBarra) + '%'); //atualiza a label
            },
            error: function () {

            }
        });
    };





    /*View - Semesters*/
    $('#btn-show-semester').click(function () {
        $('#history-semester').css('visibility', 'visible');
    });

    /*View - Steps*/

    $(document).on('change', '#select-steps-professor', function () {

        var ProfessorId = $("option:selected", this).val();

        $.ajax({
            url: '/admin/_StepsAvailability/?ProfessorId=' + ProfessorId,
            method: 'get',
            dataType: 'html',
            success: function (data) {
                $('#_StepsAvailability').html(data);

                selecao = 1;
                horas = parseInt($('#horas').html());
                console.log(horas);
                totalBarra = horas + (horas / 2);
                maxDisp = horas / 2; //max quadros disponiveis
                maxTalvez = parseInt(maxDisp / 2); //max quadros talvez
                if (totalBarra % 2 != 0) { totalBarra++; maxTalvez++ } //caso seja impar, aumenta 1

                if ($("#existeDisponibilidade").length > 0) { //verifica se elemento existe e inicia valores ja preenchidos
                    contTalvez = maxTalvez;
                    atualBarra = totalBarra;
                    contDisp = maxDisp;
                    console.log(contTalvez);
                    console.log(contDisp);
                    console.log(atualBarra);
                }
                else {
                    contTalvez = 0;//contTalvez = maxTalvez;
                    atualBarra = 0;//atualBarra = totalBarra;
                    contDisp = 0; //contDisp = maxDisp;
                    console.log(contTalvez);
                    console.log(contDisp);
                    console.log(atualBarra);
                }

                widthBarra = 0;
                color = '#4DB6AC';
                $(".progress-bar").attr('aria-valuemax', totalBarra); //Inicializador do total barra
                $(".slots-save").attr('disabled', 'disabled');
                console.log("total barra é:", totalBarra);

                //checa disabled do bt save
                if (atualBarra == totalBarra) {
                    $(".slots-save").removeAttr('disabled', 'disabled');
                }

                //prache!
                $(".progress-bar").attr('aria-valuenow', atualBarra); //atualiza o valuenow da progress-bar
                widthBarra = (atualBarra / totalBarra) * 100; //atualiza o width da progress-bar
                $(".progress-bar").css('width', widthBarra + '%'); //preenche bar com o width
                $(".progress-bar label").html(parseInt(widthBarra) + '%'); //atualiza a label


            },
            error: function () {

            }
        });
    });











    $('#step1').show();
    $('#btn-step1').click(function () {
        $("#steps-content .step").hide();
        $("#step3").hide(); //solução não tão boa para esconder a step3
        $("#step1").show();
        $(this).css('background-color', '#FF8A65');
        $('#btn-step2').css('background-color', '#607D8B');
        $('#btn-step3').css('background-color', '#607D8B');

    });
    $('#btn-step2').click(function () {
        $("#steps-content .step").hide();
        $("#step3").hide(); //solução não tão boa para esconder a step3
        $("#step2").show();
        $(this).css('background-color', '#FF8A65');
        $('#btn-step1').css('background-color', '#607D8B');
        $('#btn-step3').css('background-color', '#607D8B');
    });
    $('#btn-step3').click(function () {
        $("#steps-content .step").hide();
        $("#step3").show();
        $(this).css('background-color', '#FF8A65');
        $('#btn-step1').css('background-color', '#607D8B');
        $('#btn-step2').css('background-color', '#607D8B');
    });

    $("#txt-step1-prof").keyup(function () {
        $('#select-step1-status').val("");
        var colunaprof = '#tb-step1 td:nth-child(1)';
        var valor = $(this).val().toUpperCase();
        $("#tb-step1 tbody tr").show();
        $(colunaprof).each(function () {
            if ($(this).text().toUpperCase().indexOf(valor) < 0) {
                $(this).parent().hide();
            }
        });
    });
    $("#select-step1-status").click(function () {
        $('#txt-step1-prof').val("");
        var colunastatus = '#tb-step1 td:nth-child(2)';
        var valor = $(this).val().toUpperCase();
        $("#tb-step1 tbody tr").show();
        if (valor != "ALL") {
            $(colunastatus).each(function () {
                if ($(this).text().toUpperCase().indexOf(valor) < 0) {
                    $(this).parent().hide();
                }
            });
        }
    });
    /*Reaproveitar código do filtro da step1*/
    $("#txt-step2-prof").keyup(function () {
        $(".disp-step").hide();
        $(".finish-step").show();
        $('#select-step2-status').val("");
        var colunaprof = '#tb-step2 td:nth-child(1)';
        var valor = $(this).val().toUpperCase();
        $("#tb-step2 tbody tr").show();
        $(colunaprof).each(function () {
            if ($(this).text().toUpperCase().indexOf(valor) < 0) {
                $(this).parent().hide();
            }
        });
    });
    /*Reaproveitar código do filtro da step1*/
    $("#select-step2-status").click(function () {
        $(".disp-step").hide();
        $(".finish-step").show();
        $('#txt-step2-prof').val("");
        var colunastatus = '#tb-step2 td:nth-child(2)';
        var valor = $(this).val().toUpperCase();
        $("#tb-step2 tbody tr").show();
        if (valor != "ALL") {
            $(colunastatus).each(function () {
                if ($(this).text().toUpperCase().indexOf(valor) < 0) {
                    $(this).parent().hide();
                }
            });
        }
    });

    /*Pula pra step 2 com nome de professor filtrado*/
    $("#tb-step1 tr td:nth-child(1)").click(function () {
        $('#btn-step2').click();
        $("#p-disp-step2").html("" + $(this).text());
        $('#txt-step2-prof').val("" + $(this).text());
        $("#txt-step2-prof").keyup();
    });

    /*Carrega tela de disponibilidade de professor selecionado*/
    $("#tb-step2 tr td:nth-child(1)").click(function () {
        $("#p-disp-step2").html("" + $(this).text());
        $('#txt-step2-prof').val("" + $(this).text());
        $("#txt-step2-prof").keyup();
        $(".disp-step").slideDown();
        $(".finish-step").hide();
    });
    $(".clear-filter").click(function () {
        $(".disp-step").hide();
        $(".finish-step").show();
        $(".step table tbody tr").show();
        $(".step input").val("");
        $("#step1 select").val("");
        $("#step2 select").val("");
    });

    /*View Professors*/
    $("#professor tr").click(function () {
        location.href = '/professors/update/' + $(this).find('td:nth-child(1)').html();
    });

    /*View Courses*/
    $(document).on('click', '#course tr', function () {
        location.href = '/courses/update/' + $(this).find('td:nth-child(1)').html();
    });

    $('#_TabDisciplinas').load('/courses/_TabDisciplinas/');

    $(document).on('click', '#btn-create-discipline', function () {
        $.ajax({
            url: '/courses/_createdisciplina/',
            method: 'get',
            dataType: 'html',
            success: function (data) {
                $('#_CreateDisciplina').html(data);
            },
            error: function () {

            }
        });
    });

    /*View Assignment*/
    var disciplinas = [];
    var QtdAulas = 0;

    $(document).on('change', '#select-assignment-professor', function () {

        var ProfessorId = $("option:selected", this).val();

        $.ajax({
            url: '/assignment/_AssignmentProfessor/?ProfessorId=' + ProfessorId,
            method: 'get',
            dataType: 'html',
            success: function (data) {
                $('#_AssignmentProfessor').html(data);
            },
            error: function () {

            }
        });
        disciplinas = [];
    });

    $(document).on('change', '#select-assignment-curso', function () {
        QtdAulas = $('#QtdAulas').val(); //quando seleciona o curso atribui a qtd aulas;
        $('#lbl-aulas').html(QtdAulas);

        var CursoId = $("option:selected", this).val();
        var ProfessorId = $("#select-assignment-professor option:selected").val();

        $.ajax({
            url: '/assignment/_AssignmentCurso/?CursoId=' + CursoId + '&ProfessorId=' + ProfessorId,
            method: 'get',
            dataType: 'html',
            success: function (data) {
                $('#_AssignmentCurso').html(data);
            },
            error: function () {

            }
        });
        disciplinas = [];
    });

    //pega hora aula ao clicar no input checkbox
    $(document).on('click', '#tb-assignment tr td input', function () {
        if ($(this).is(':checked')) {
            //add hrs aula
            QtdAulas = parseInt(QtdAulas) + parseInt($(this).val().split(" ")[1]);
            $('#lbl-aulas').html(QtdAulas);
        }
        else {
            //remove hrs aula
            QtdAulas -= parseInt($(this).val().split(" ")[1]);
            $('#lbl-aulas').html(QtdAulas);
        }

        //checa se ultrapassou limite
        if (parseInt($("#lbl-aulas").html()) > 28) {
            //cria alert mensagem de sucesso
            if ($('#msg-limite').val() == null) {
                $("#assignment-hours").after(
                  '<div id="msg-limite" style="float:left; width:100%; text-align:center" class="alert alert-danger alert-dismissable">' +
                      '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">' +
                           '&times;' +
                      '</button>' +
                      'Assignment hours limit exceeded' +
                  '</div>');
            }
            //desabilitar btnsave
            $("#btn-save-assignment").attr('disabled', 'disabled');
        }
        else {
            $("#btn-save-assignment").removeAttr('disabled');
        }
    });

    //salva atribuição
    $(document).on('click', '#btn-save-assignment', function () {

        //atualiza o value do input #QtdAulas e lbl-aulas
        $("#QtdAulas").attr('value', QtdAulas);

        //varre checkbox selected
        var checkbox = $('#tb-assignment tr td input:checkbox[name^=cbDisciplina]:checked');
        if (checkbox.length > 0) {
            checkbox.each(function () {
                disciplinas.push($(this).val().split(" ")[0]);
            });
        }

        //cria alert mensagem de sucesso
        if ($('#msg-sucesso').val() == null) {
            $("#tb-assignment").after(
              '<div id="msg-sucesso" style="float:left; width:100%; text-align:center" class="alert alert-success alert-dismissable">' +
                  '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">' +
                       '&times;' +
                  '</button>' +
                  'Assignment saved successfully' +
              '</div>');
        }

        var ProfessorId = $("#select-assignment-professor option:selected").val();
        var CursoId = $("#select-assignment-curso option:selected").val();

        $.ajax({ //talvez n precisamos usar o ajax aqui
            url: '/assignment/Create/?ProfessorId=' + ProfessorId + '&CursoId=' + CursoId + '&QtdAulas=' + QtdAulas
                + '&disciplinas=' + disciplinas,
            method: 'get',
            success: function (data) {

            },
            error: function () {

            }
        });
        disciplinas = [];
    });


    /*Easter Egg*/
    $("#f4b").click(function () {
        if (ctrl_f4b == 5) {
            $("#f4b").slideUp();
            $("#f6b").slideDown();
            $("#int5").slideDown();
            $("#int6").slideDown();
        }
        ctrl_f4b++;
    });

    /*Modal Lembrete*/
    $(document).on('click', '#lista-lembretes.table-seha table tr', function () {
        $('.modal-lembrete').fadeIn();
        $('body').css('overflow', 'hidden');
        var dateLembrete = $(this).find('td').html();
        var contentLembrete = $(this).find('td:nth-child(2)').html();
        $('.dateLemb').html(dateLembrete);
        $('.contentLemb').html(contentLembrete);
    });

    $(document).on('click', '#fecharLemb', function () {
        $('.modal-lembrete').fadeOut();
        $('body').css('overflow', 'auto');
    });

});
