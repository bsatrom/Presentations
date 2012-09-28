(function() {
	var _creds = "Aobl4homd3pxwBrWnQNuX25Vna6u25EHc-LJcvzIGPldJLQZdsGq6mk57Aq0ft80";
	var mapDiv = document.getElementById("map");
	var _map = new Microsoft.Maps.Map(mapDiv, { credentials: _creds });

	// Add details and position functions (dr18)
	
	function displayError(msg) {
		$('#error').text(msg);
	}
	
	// Add errorHandler and fallback form (dr17)

	// Add locate function to kick off geolocation (dr16)

	// Add handler for "Find" click (dr19)
	
})();