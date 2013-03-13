(function($) {

	// Add button handlers (dr24)
	$('#slide').on('click', function() {
		$("#page img:first").addClass('slide');
	});
	
	$('#stop').on('click', function() {
		$("#page img:first").removeClass('slide');
	});	

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