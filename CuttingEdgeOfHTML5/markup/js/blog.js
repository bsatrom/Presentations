(function(Modernizr) {
	// Add Modernizr Check and PIE.js (dr04)
	Modernizr.load({
	  test: Modernizr.borderradius || Modernizr.boxshadow,
	  nope: '../lib/PIE.js',
	  callback: function () {
	    $('article').each(function () {
	      PIE.attach(this);
	    });
	  }
	});	
})(Modernizr);