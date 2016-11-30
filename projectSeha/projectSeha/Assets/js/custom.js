$(document).ready(function () {
    var ctrl = 0;
    var ctrl2 = 0;
    var ctrl_f4b = 0;
    var modal_lemb = 0;
    

    /*View Availability*/

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

    /*--- Botões de controle --------------------------------------------- */
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
        //verifica se o slot clicado está 'bloqueado'
        if (!$(this).hasClass('bloqueado')) {
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
        }

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
       
    /*--- Botão para salvar avaiability e obs -----------------------------*/
    var slotDisponivel = [];
    var slotTalvez = [];
    $(document).on('click', '.slots-save', function () {
        //Limpar lista de slots talvez e disponível para iniciar nova contagem
        slotDisponivel = [];
        slotTalvez = [];

        var disponivel = $('.slots-content.disponivel input');
        var talvez = $('.slots-content.talvez input');
        //Varre inputs disponíveis e talvez adicionando nas listas slotDisponivel e slotTalvez
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

        //Pega o ID passado pelo input disabled hidden para salvar avaiability no professor
        var ProfessorId = $("#PessoaId").val();
        $.ajax({
            url: '/default/Create/?ProfessorId=' + ProfessorId + '&slotDisponivel=' + slotDisponivel + '&slotTalvez=' + slotTalvez,
            method: 'get',
            success: function (data) {

            },
            error: function () {

            }
        });

        /*Observation - Availability -----------------------------------------------------------------*/
        var obs = $('#txt-obs').val();
        if (obs != null) {
            $.ajax({
                url: '/user/UpdateObservation/?ProfessorId=' + ProfessorId + '&observacoes=' + obs,
                success: function () {

                }
            });
        }
        /*--------------------------------------------------------------------------------------------*/

        //Exibe msg de sucesso
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

    //Carregamento da view Avaiability para a Session["professor"]
    if ($("#_Availability".length > 0)) {
        var ProfessorId = $("#ProfessorId").val();
        if (ProfessorId != undefined){
            $.ajax({
                url: '/user/_Availability/?ProfessorId=' + ProfessorId,
                method: 'get',
                dataType: 'html',
                success: function (data) {
                    $('#_Availability').html(data);

                    selecao = 1;
                    horas = parseInt($('#horas').html());
                    totalBarra = horas + (horas / 2);
                    maxDisp = horas / 2; //max quadros disponiveis
                    maxTalvez = parseInt(maxDisp / 2); //max quadros talvez
                    if (totalBarra % 2 != 0) { totalBarra++; maxTalvez++ } //caso seja impar, aumenta 1

                    if ($("#existeDisponibilidade").length > 0) { //verifica se elemento existe e inicia valores ja preenchidos
                        contTalvez = maxTalvez;
                        atualBarra = totalBarra;
                        contDisp = maxDisp;
                    }
                    else {
                        contTalvez = 0;
                        atualBarra = 0;
                        contDisp = 0;
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
        }
    };
     
  
    /*View - Steps*/

    //Carregamento da view Avaiability para a Session["admin"]
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
                totalBarra = horas + (horas / 2);
                maxDisp = horas / 2; //max quadros disponiveis
                maxTalvez = parseInt(maxDisp / 2); //max quadros talvez
                if (totalBarra % 2 != 0) { totalBarra++; maxTalvez++ } //caso seja impar, aumenta 1

                if ($("#existeDisponibilidade").length > 0) { //verifica se elemento existe e inicia valores ja preenchidos
                    contTalvez = maxTalvez;
                    atualBarra = totalBarra;
                    contDisp = maxDisp;
                }
                else {
                    contTalvez = 0;//contTalvez = maxTalvez;
                    atualBarra = 0;//atualBarra = totalBarra;
                    contDisp = 0; //contDisp = maxDisp;
                }

                widthBarra = 0;
                color = '#4DB6AC';
                $(".progress-bar").attr('aria-valuemax', totalBarra); //Inicializador do total barra
                $(".slots-save").attr('disabled', 'disabled');

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
    

    /*View Professors*/
    $("#professor tr").click(function () {
        //pega o id pela tabela para redirecionar ao update do professor
        location.href = '/professors/update/' + $(this).find('td:nth-child(1)').html();
    });

    /*View Courses*/
    var contadorDisciplinas = 0;
    $(document).on('click', '#course tr', function () {
        location.href = '/courses/Update/' + $(this).find('td:nth-child(1)').html();
    });
    $(document).on('click', '#disciplinas tr', function () {
        location.href = '/courses/UpdateDisciplina/' + $(this).find('td:nth-child(1)').html();
    });
    $(document).on('click', '#disciplinasCreate tr', function () {
        location.href = '/courses/AtualizarDisciplina/' + $(this).find('td:nth-child(1)').html();
    });

    $(".table tbody>tr").each(function () {
        contadorDisciplinas++;
    });
    
    if (contadorDisciplinas > 0) {
        $("#btnSaveCourse").removeClass('disabled')
    }

    /*View Assignment*/

    var disciplinas = [];
    var QtdAulas = 0;

    // Método carrega horas aula do professor selecionado e abre select-curso
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

    //Método carrega assignments do professor de acordo com o curso selecionado
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

    //Método pega hora aula ao clicar no input checkbox de cada disciplina
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
            $('#msg-limite').show();
            //cria alert mensagem de sucesso
            if ($('#msg-limite').val() == null) {
                $("#assignment-hours").after(
                  '<div id="msg-limite" style="float:left; width:100%; text-align:center" class="alert alert-danger alert-dismissable">' +
                      '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">' +
                           '&times;' +
                      '</button>' +
                      'Assignment hours exceeded limit' +
                  '</div>');
            }
            //desabilitar btnsave
            $("#btn-save-assignment").attr('disabled', 'disabled');
        }
        else {
            $("#btn-save-assignment").removeAttr('disabled');
            $('#msg-limite').hide();
            $('#msg-limite').val(null);
        }
    });

    //Método salva atribuição
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

    /*Método search*/

    $(function () {
        $(".search").keyup(function () {
            var texto = $(this).val().toUpperCase();
            if (texto != "") {
                $(".table tbody>tr").each(function () {
                    if ($("td:nth-child(2)", this).html().toUpperCase().indexOf(texto) > -1) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }
                });          
            }
            else {
                $("table tbody>tr").show()
            }
           
        });
    });

    //results
    $(document).on('change', '#select-step3-period', function () {
        var turno = $("option:selected",this).html();
        $.ajax({
            url: '/admin/_ResultsTurno/?turno=' + turno,
            method: 'get',
            dataType: 'html',
            success: function (data) {
                $('#_ResultsTurno').html(data);
            },
            error: function () {

            }
        });
    });

    //pdf
    $(document).on("click", "#btn-step3-export", function (e) {
        $("#tb-step3").btechco_excelexport({
            containerid: "tb-step3"
            , datatype: $datatype.Table
            , filename: 'results'
        });
    });

    /*Dashboards*/
    $('.tb-profcolor').click(function () {
        var next = $(this).next();
        while (next.hasClass('dash-slots')) {
            $(next).fadeToggle();
            next = next.next();
        }
    });

});
