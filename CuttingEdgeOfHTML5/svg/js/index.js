(function() {
	var timer;

	function getColors(stops) {
	    var i, length;
	    var colors = [];
	    
	    for (i = 0, length = stops.length; i < length; i++) {
	        colors.push(stops[i].attributes['stop-color'].value);
	    }

	    return colors;
	}

	// Add Shimmer function (dr12)
	function shimmer(stops) {
	  var i, length;
	  var stopColors = getColors(stops);

	  for (i = 0, length = stops.length; i < length; i++) {
	    stops[i].attributes['stop-color'].value = stopColors[i === 2 ? 0 : i + 1];
	  }
	}	

	// Add button click event handlers (dr13)
	$('#animate').on('click', function() {
		var stops = document.querySelectorAll('#circleGrad stop');

		timer = setInterval(function () { 
			shimmer(stops); 
		}, 300);
	});

	$('#stop').on('click', function() {
		clearInterval(timer);
	});
})();