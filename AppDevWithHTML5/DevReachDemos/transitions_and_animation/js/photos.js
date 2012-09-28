(function($) {

	// Add button handlers (dr24)
	

	Modernizr.load({
		test: Modernizr.borderradius,
		nope: '../../js/PIE.js',
		callback: function() {
			$('img').each(function() {
				PIE.attach(this);
			})
		}
	});
})(jQuery);