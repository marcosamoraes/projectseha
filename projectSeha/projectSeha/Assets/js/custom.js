// Aqui colocamos nosso próprio js

$(document).ready(function(){

	var color = 'transparent';

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
	});

});