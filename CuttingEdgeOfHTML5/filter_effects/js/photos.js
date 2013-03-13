(function($) {
	var button = $('button'),
		idList = [];

	button.each(function() {
		idList.push(this.id);
	});
	
	// Add button handlers (dr31)
	
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