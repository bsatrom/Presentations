// JavaScript object literal to describe data *and* behavior for your UI
var viewModel = {
    firstName: ko.observable("Hugo"),
    lastName: ko.observable("Reyes")
};
viewModel.fullName = ko.dependentObservable(function() {
    return this.firstName() + " " + this.lastName();
}, viewModel);

// Tell knockout to do its thing
ko.applyBindings(viewModel);