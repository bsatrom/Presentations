// JavaScript object literal to describe data *and* behavior for your UI
var viewModel = {
    firstName: ko.observable("Hugo"),
    lastName: ko.observable("Reyes"),
    
    capitalizeLastName: function() {
        var currentVal = this.lastName();        // <-- Call observable values as functions
        this.lastName(currentVal.toUpperCase()); // Pass the new value in as a parameter
    }
};
viewModel.fullName = ko.dependentObservable(function() {
    return this.firstName() + " " + this.lastName();
}, viewModel);

// Tell knockout to do its thing
ko.applyBindings(viewModel);