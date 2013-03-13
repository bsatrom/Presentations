(function($) {
	var canvasTimer;
	var x = 200,
		y = 300,
		radius = 135,
		colorStops = ["rgb(255, 255, 255)", "rgb(140, 140, 140)", "rgb(55, 55, 55)"];
	
	function shift() {
	    var firstItem = colorStops.shift();
	    colorStops.push(firstItem);
	}

	// Add canvas element and context (dr05)
	var canvas = document.querySelector('#circle');
	var ctx = canvas.getContext('2d');

	// Create the drawCircle function (dr06)
	function drawCircle() {
		var gradient = ctx.createRadialGradient(x, y, 0, x, y, radius);
		gradient.addColorStop(0, colorStops[0]);
		gradient.addColorStop(.5, colorStops[1]);
		gradient.addColorStop(1, colorStops[2]);

		ctx.fillStyle = gradient;
		ctx.arc(x, y, radius, 0, Math.PI * 2);
		ctx.fill();

		ctx.strokeStyle = '#aaa';
		ctx.lineWidth = 10;
		ctx.stroke();
	}
	// Create the animateCircle function (dr07)
	function animateCircle() {
		canvasTimer = setInterval(function () {
	        drawCircle();
	        shift();
	    }, 300);
	}
	// Add button click and call to animate circle (dr08)
	$('#animate').on('click', function() {
		animateCircle();
	});

	$('#stop').on('click', function() {
		 clearInterval(canvasTimer);
	});	
	// Add call to drawCircle (dr09)
	drawCircle();
})(jQuery);