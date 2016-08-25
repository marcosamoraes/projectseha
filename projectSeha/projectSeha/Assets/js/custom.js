// Aqui colocamos nosso próprio js

$(document).ready(function(){

	var color = 'transparent';
	var barfill = 0;
	var ctrl = 0;

	//Select colors (Slots)
	$('#eraser').click(function(){
		color = 'transparent';

		$(this).css('opacity', '1');
		$('#available').css('opacity', '0.5');
		$('#maybe').css('opacity', '0.5');
	});

	$('#available').click(function(){
		color = '#7cfc00';
	
		$(this).css('opacity', '1');
		$('#eraser').css('opacity', '0.5');
		$('#maybe').css('opacity', '0.5');
	});

	$('#maybe').click(function(){
		color = '#ffbf00';

		$(this).css('opacity', '1');
		$('#eraser').css('opacity', '0.5');
		$('#available').css('opacity', '0.5');
	});

	$('.slots-content').click(function(){
		$(this).css('background-color', color);
		if(color != 'transparent'){
			$(this).addClass('counted');
			$(this).removeClass('removed');
		} else {
			if($(this).hasClass('counted')) {
				$(this).removeClass('counted');
				$(this).addClass('removed');
				ctrl = 0;
			}
		}

		if($(this).hasClass('counted')) {
			if(!$(this).hasClass('firstpass')) {
				barfill += 1;
				$(this).addClass('firstpass');
			}
			$(this).addClass('counted');
		} else if($(this).hasClass('removed')) {
			barfill -= 1;
			$(this).removeClass('removed');
			$(this).removeClass('firstpass');
		}

		if(barfill == 10) {
			setInterval(alert ('You have filled 10 slots, if possible fill more 5 slots'), 1000);
		}

		$('#mainNav').click(function(){
			alert('barfill: ' + barfill);
		});
	});

});