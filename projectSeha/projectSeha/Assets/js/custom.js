// Aqui colocamos nosso próprio js

$(document).ready(function () {

    var color = '#7cfc00';
    var barfill = 0;
    var ctrl = 0;
    var ctrl2 = 0;
    var bar_width = $('.slots-bar').width();
    $('.bar-fill').css('max-width', bar_width);
    bar_width = bar_width / 15;

    //Select colors (Slots)
    $('#eraser').click(function () {
        color = 'transparent';

        $(this).css('opacity', '1');
        $('#available').css('opacity', '0.5');
        $('#maybe').css('opacity', '0.5');
    });

    $('#available').click(function () {
        color = '#7cfc00';

        $(this).css('opacity', '1');
        $('#eraser').css('opacity', '0.5');
        $('#maybe').css('opacity', '0.5');
    });

    $('#maybe').click(function () {
        color = '#ffbf00';

        $(this).css('opacity', '1');
        $('#eraser').css('opacity', '0.5');
        $('#available').css('opacity', '0.5');
    });

    $('.slots-content').click(function () {
        if (!$(this).hasClass('desactive')) {
            $(this).css('background-color', color);
            if (color != 'transparent') {
                $(this).css('border', 'solid');
                $(this).css('border-width', '1px');
                $(this).css('border-color', 'white');
                $(this).addClass('counted');
                $(this).removeClass('removed');
            } else {
                if ($(this).hasClass('counted')) {
                    $(this).removeClass('counted');
                    $(this).addClass('removed');
                    $(this).css('border', '0');
                    ctrl = 0;
                }
            }

            if ($(this).hasClass('counted')) {
                if (!$(this).hasClass('firstpass')) {
                    barfill += 1;
                    $(this).addClass('firstpass');
                }
                $(this).addClass('counted');
            } else if ($(this).hasClass('removed')) {
                barfill -= 1;
                $(this).removeClass('removed');
                $(this).removeClass('firstpass');
            }
            /*
            if (barfill == 10 && ctrl2 == 0) {
                alert('You have filled 10 slots, if possible fill more 5 slots');
                ctrl2 = 1;
            }
    
            if (barfill == 15 && ctrl2 == 1) {
                alert('You have filled 15 slots, congratz!');
                ctrl2 = 2;
            }
            */
            result = barfill * bar_width;
            $('.bar-fill').animate({ width: result }, 200);
        }
    });

    $('.slots-clear').click(function () {
        $('.slots-content').css('background-color', 'transparent');
        $('.slots-content').css('border', '0');
        $('.slots-content').removeClass('counted');
        $('.slots-content').removeClass('firstpass');
        $('.slots-content').removeClass('removed');
        barfill = 0;
        $('.bar-fill').animate({ width: 0 }, 500);
        ctrl2 = 0;
    });

    /*View - Semesters*/
    $('#btn-show-semester').click(function () {
        $('#history-semester').css('visibility', 'visible');
    });

    /*View - Steps*/
    $('#step1').show();
    $('#btn-step1').click(function () {
        $("#steps-content .step").hide();
        $("#step1").show();
    });
    $('#btn-step2').click(function () {
        $("#steps-content .step").hide();
        $("#step2").show();
    });
    $('#btn-step3').click(function () {
        $("#steps-content .step").hide();
        $("#step3").show();
    });

    $("#txt-step1-prof").keyup(function () {
        $('#select-step1-status').val("");
        var colunaprof = '#tb-step1 td:nth-child(1)';
        var valor = $(this).val().toUpperCase();
        $("#tb-step1 tbody tr").show();
        $(colunaprof).each(function (){
            if($(this).text().toUpperCase().indexOf(valor) < 0){
                $(this).parent().hide();
            }
        });
    });
    $("#select-step1-status").click(function () {
        $('#txt-step1-prof').val("");
        var colunastatus = '#tb-step1 td:nth-child(2)';
        var valor = $(this).val().toUpperCase();
        $("#tb-step1 tbody tr").show();
        if(valor!= "ALL") {
            $(colunastatus).each(function () {
                if ($(this).text().toUpperCase().indexOf(valor) < 0) {
                    $(this).parent().hide();
                }
            });
        }   
    });
    /*Reaproveitar código do filtro da step1*/
    $("#txt-step2-prof").keyup(function () { 
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
    });
   
});